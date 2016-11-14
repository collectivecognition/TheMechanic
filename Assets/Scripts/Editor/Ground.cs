using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Ground {
    [MenuItem("Tools/Generate Ground In Scene")]
    private static void GenerateTerrain() {
        List<Vector3> vertices = new List<Vector3>();
        List<int> indexes = new List<int>();

        float gridInterval = 5f;
        float gridMinX = -500f;
        float gridMaxX = 500f;
        float gridMinZ = -500f;
        float gridMaxZ = 500f;

        float xx = 0, zz = 0;
        int ii = 0;
        int colsPerRow = (int)(Mathf.Abs(gridMinZ - gridMaxZ) / gridInterval);
        for(xx = gridMinX; xx < gridMaxX; xx += gridInterval) {
            for(zz = gridMinZ; zz < gridMaxZ; zz += gridInterval) {
                vertices.Add(new Vector3(xx, 0f, zz));

                if(zz < gridMaxZ - gridInterval) {
                    indexes.Add(ii);
                    indexes.Add(ii + 1);
                }

                if(xx < gridMaxX - gridInterval ) {
                    indexes.Add(ii);
                    indexes.Add(ii + colsPerRow);
                }

                ii++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.name = "Generated Ground";
        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indexes.ToArray(), MeshTopology.Lines, 0);

        AssetDatabase.CreateAsset(mesh, "Assets/Resources/Meshes/Ground_" + Random.Range(0, 10000) + ".asset");
    }
}