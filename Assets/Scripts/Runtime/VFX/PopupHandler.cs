using Cysharp.Threading.Tasks;
using EricGames.Runtime.Mechanics;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PopupHandler : MonoBehaviour
{
    // Prefab assets
    public AssetReferenceGameObject popupDamageAddressable;

    private PopupDamage popupDamageSource = null;

    [SerializeField]
    private MechanicsDamageHandler damageHandler = null;

    private void Awake()
    {
        _ = LoadAddressable();
    }

    private void OnEnable()
    {
        damageHandler.DamageEvent += ShowPopupDamage;
    }

    private void OnDisable()
    {
        damageHandler.DamageEvent -= ShowPopupDamage;
    }

    private async UniTask LoadAddressable()
    {
        var go = await popupDamageAddressable.LoadAssetAsync();

        if (go.TryGetComponent(out popupDamageSource))
        {
            popupDamageAddressable.ReleaseAsset();
        }
    }

    private void ShowPopupDamage(Skill skill)
    {
        if (popupDamageSource != null)
        {
            var popupDamage = Instantiate(popupDamageSource);
            popupDamage.Setup(skill.damage, transform.position + transform.up * 1.5f);
        }
    }
}