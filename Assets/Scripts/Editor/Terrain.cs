using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Terrain {
    private static float gridInterval = 1f;

    [MenuItem("Tools/Generate Terrain For Path")]
    private static void GenerateTerrain() {
        GameObject selected = Selection.activeGameObject;
        List<Vector3> nodes = selected.GetComponent<iTweenPath>().nodes;

        Vector3[] path = nodes.ToArray();
        List<Vector3> vertices = new List<Vector3>();

        float pathLength = iTween.PathLength(path);
        float step = 0.1f * 1f / pathLength;
        float gridInterval = 5f;
        Vector3 previousPoint = path[0];

        for(float ii = 0; ii < 1f; ii += step) {
            Vector3 currentPoint = iTween.PointOnPath(path, ii);

            float currentXNormalized = Mathf.Floor(currentPoint.x / gridInterval) * gridInterval;
            float currentZNormalized = Mathf.Floor(currentPoint.z / gridInterval) * gridInterval;
            float previousXNormalized = Mathf.Floor(previousPoint.x / gridInterval) * gridInterval;
            float previousZNormalized = Mathf.Floor(previousPoint.z / gridInterval) * gridInterval;

            if(currentXNormalized != previousXNormalized) {
                float averageZ = (currentZNormalized + previousZNormalized) / 2f;
                float xNormalized = currentXNormalized > previousXNormalized ? currentXNormalized : previousXNormalized;
                vertices.Add(new Vector3(xNormalized, currentPoint.y, averageZ)); // Largely ignores the y axis, shouldn't really need this
            }

            if(currentZNormalized != previousZNormalized) {
                float averageX = (currentXNormalized + previousXNormalized) / 2f;
                float zNormalized = currentZNormalized > previousZNormalized ? currentZNormalized : previousZNormalized;
                vertices.Add(new Vector3(averageX, currentPoint.y, currentZNormalized));
            }

            previousPoint = currentPoint;
        }

        int[] indexes = new int[vertices.Count * 2];

        for(int ii = 0, jj = 0; ii < vertices.Count; ii++) {
            indexes[jj++] = ii;
            indexes[jj++] = ii + 1;
        }

        indexes[indexes.Length - 2] = vertices.Count - 1;
        indexes[indexes.Length - 1] = 0;

        Mesh mesh = new Mesh();
        mesh.name = "Generated Terrain";
        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indexes, MeshTopology.Lines, 0);

        AssetDatabase.CreateAsset(mesh, "Assets/Resources/Meshes/" + selected.name + "Terrain.asset");
    }
}