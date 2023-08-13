using UnityEngine;
using TMPro;

public class PopupDamage : MonoBehaviour
{
    private int damage;
    private float disappearTimer = 1.0f;
    private TextMeshPro textMesh;
    private Color textColor;

    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    public void Setup(int damageAmount, Vector3 position)
    {
        transform.position = position;
        damage = damageAmount;
        textMesh.SetText(damageAmount.ToString());
        disappearTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 1.0f;
        transform.position += moveYSpeed * Time.deltaTime * Vector3.up;

        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            disappearTimer -= Time.deltaTime;
        }
    }
}
