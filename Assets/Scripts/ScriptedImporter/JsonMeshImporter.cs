using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;
using System;
using EricGames.Core.Triangulations;

[ScriptedImporter(1, "jsonmesh")]
public class JsonMeshImporter : ScriptedImporter
{
    [SerializeField]
    public Color color;

    [Range(.01f, 10.0f)]
    public float uniformScale = 1.0f;

    [Serializable]
    struct JsonMesh
    {
        [SerializeField]
        public Vector2[] points;
    }

    public override void OnImportAsset(AssetImportContext ctx)
    {
        var jsonContent = File.ReadAllText(ctx.assetPath);
        var jsonMesh = JsonUtility.FromJson<JsonMesh>(jsonContent);

        var gameObject = new GameObject();
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        var nameWithoutExtension = Path.GetFileNameWithoutExtension(ctx.assetPath);

        material.name = nameWithoutExtension;
        material.color = color;
        meshRenderer.sharedMaterial = material;

        var mesh = Delaunay.Triangulate(jsonMesh.points);
        mesh.name = nameWithoutExtension;
        meshFilter.sharedMesh = mesh;

        gameObject.transform.localScale = Vector3.one * uniformScale;

        ctx.AddObjectToAsset("mesh", mesh);
        ctx.AddObjectToAsset("material", material);
        ctx.AddObjectToAsset("gameObject", gameObject);
        ctx.SetMainObject(gameObject);
    }
}
