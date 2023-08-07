using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace EricGames.Runtime.Equipment
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float timeToShow = 0.5f;
        [SerializeField] private float timeToHide = 0.5f;
        [SerializeField] private Color color;

        private Material material;
        private CancellationTokenSource cts = new();

        // Start is called before the first frame update
        void Awake()
        {
            material = GetComponent<SpriteRenderer>().material;
        }

        void OnEnable()
        {
            material.SetFloat("_Alpha", 0.0f);
        }

        public void Show()
        {
            cts.Cancel(true);
            cts.Dispose();
            cts = new CancellationTokenSource();
            ShowAsync(cts.Token).Forget();
        }

        public void Hide()
        {
            cts.Cancel(true);
            cts.Dispose();
            cts = new CancellationTokenSource();
            HideAsync(cts.Token).Forget();
        }

        public async UniTask ShowAsync(CancellationToken token)
        {
            float timer = timeToShow;
            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                material.SetFloat("_Alpha", 1.0f - timer / timeToHide);
                token.ThrowIfCancellationRequested();
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        public async UniTask HideAsync(CancellationToken token)
        {
            float timer = timeToHide;
            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                material.SetFloat("_Alpha", timer / timeToHide);
                token.ThrowIfCancellationRequested();
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
    }
}
