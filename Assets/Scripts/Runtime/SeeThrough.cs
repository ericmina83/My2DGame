using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<TilemapRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        var position = mainCamera.WorldToViewportPoint(player.position);
        material.SetVector("_PlayerPosition", new Vector4(position.x, position.y, 0, 0));
    }
}
