using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupDamage : MonoBehaviour
{
    int damage;
    float disappearTimer = 1.0f;
    private TextMeshPro textMesh;
    private Color textColor;

    public static PopupDamage Create(Vector2 position, int damageAmount)
    {
        var popupDamageTransform = Instantiate(GameAssets.instance.popupDamage, position, Quaternion.identity);
        var popupDamage = popupDamageTransform.GetComponent<PopupDamage>();

        popupDamage.Setup(damageAmount);
        return popupDamage;
    }

    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    void Setup(int damageAmount)
    {
        this.damage = damageAmount;
        textMesh.SetText(damageAmount.ToString());
        disappearTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 1.0f;
        transform.position += Vector3.up * moveYSpeed * Time.deltaTime;

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
