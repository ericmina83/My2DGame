using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [Range(0, 1)] public float fade = 1.0f;
    [Range(0, 1)] public float size = 0.5f;
    public Color color;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    public void SetSize(float size)
    {
        var localScale = transform.localScale;
        localScale.z = size;
        transform.localScale = localScale;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_Fade", fade);
    }
}
