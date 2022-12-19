using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCircle : MonoBehaviour
{
    private float fade; // fading period in second
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        fade = 1.0f;
        material.SetFloat("_Fade", fade);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade > 0)
            fade -= Time.deltaTime;
        else
            Destroy(gameObject);

        if (fade < 0)
            fade = 0;

        material.SetFloat("_Fade", fade);
    }
}
