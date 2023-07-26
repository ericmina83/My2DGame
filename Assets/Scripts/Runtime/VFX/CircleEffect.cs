using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh = new Mesh();

        int degreeGrid = 40;
        int degreeStep = 360 / degreeGrid;
        float ustep = 1.0f / degreeGrid;

        float radius = 2.0f;
        int radiusGrid = 8;
        float radiusStep = radius / radiusGrid;
        float vstep = 1.0f / radiusGrid;

        int verticesCount = (degreeGrid + 1) * (radiusGrid + 1);
        vertices = new Vector3[verticesCount];
        uvs = new Vector2[verticesCount];
        triangles = new int[degreeGrid * radiusGrid * 2 * 3];


        for (int r = 0; r <= radiusGrid; r++)
        {
            for (int d = 0; d <= degreeGrid; d++)
            {
                var vertexIndex = r * (degreeGrid + 1) + d;
                var vertex = vertices[vertexIndex] = Quaternion.Euler(0, 0, degreeStep * d) * new Vector3(0, radiusStep * r, 0);
                // uvs[vertexIndex] = new Vector2(vertex.x / (2.0f * radius) + 0.5f, vertex.y / (2.0f * radius) + 0.5f);
                uvs[vertexIndex] = new Vector2(ustep * (d - 1), vstep * (r - 1));

                if (r == 0 || d == 0)
                    continue;

                triangles[((r - 1) * degreeGrid + d - 1) * 6] = r * (degreeGrid + 1) + d - 1;
                triangles[((r - 1) * degreeGrid + d - 1) * 6 + 1] = (r - 1) * (degreeGrid + 1) + d - 1;
                triangles[((r - 1) * degreeGrid + d - 1) * 6 + 2] = (r - 1) * (degreeGrid + 1) + d;

                triangles[((r - 1) * degreeGrid + d - 1) * 6 + 3] = r * (degreeGrid + 1) + d;
                triangles[((r - 1) * degreeGrid + d - 1) * 6 + 4] = r * (degreeGrid + 1) + d - 1;
                triangles[((r - 1) * degreeGrid + d - 1) * 6 + 5] = (r - 1) * (degreeGrid + 1) + d;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
