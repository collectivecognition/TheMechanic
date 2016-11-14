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

        // Generate vertices based on intersections between path and a grid of the specified interval.
        // Also, generate another set of vertices slightly higher up to give height to our object.

        for(float ii = 0; ii < 1f; ii += step) {
            Vector3 currentPoint = iTween.PointOnPath(path, ii);

            float currentXNormalized = Mathf.Floor(currentPoint.x / gridInterval) * gridInterval;
            float currentYNormalized = Mathf.Floor(currentPoint.y / gridInterval) * gridInterval;
            float previousXNormalized = Mathf.Floor(previousPoint.x / gridInterval) * gridInterval;
            float previousYNormalized = Mathf.Floor(previousPoint.y / gridInterval) * gridInterval;

            if(currentXNormalized != previousXNormalized) {
                float averageY = (currentYNormalized + previousYNormalized) / 2f;
                float xNormalized = currentXNormalized > previousXNormalized ? currentXNormalized : previousXNormalized;
                vertices.Add(new Vector3(xNormalized, averageY, currentPoint.z)); // We can ignore the z axis, shouldn't really need this
                vertices.Add(new Vector3(xNormalized, averageY + gridInterval, currentPoint.z));
            }

            if(currentYNormalized != previousYNormalized) {
                float averageX = (currentXNormalized + previousXNormalized) / 2f;
                float yNormalized = currentYNormalized > previousYNormalized ? currentYNormalized : previousYNormalized;
                vertices.Add(new Vector3(averageX, currentYNormalized, currentPoint.z));
                vertices.Add(new Vector3(averageX, currentYNormalized + gridInterval, currentPoint.z));
            }

            previousPoint = currentPoint;
        }

        int[] indexes = new int[vertices.Count * 3 - 3];

        // Create lines for each set of vertices

        int jj = 0, kk = 0;
        while(kk < vertices.Count - 2) {
            indexes[jj++] = kk;
            indexes[jj++] = kk + 2;
            indexes[jj++] = kk + 1;
            indexes[jj++] = kk + 3;
            indexes[jj++] = kk;
            indexes[jj++] = kk + 1;

            kk += 2;
        }

        // Add final quad manually

        //indexes[jj++] = kk;
        //indexes[jj++] = 0;
        //indexes[jj++] = kk + 1;
        //indexes[jj++] = 1;
        //indexes[jj++] = kk;
        //indexes[jj++] = kk + 1;

        //indexes[jj++] = kk - 50;   // Bottom right
        //indexes[jj++] = 0;                    // Bottom left
        //indexes[jj++] = 1;                    // Top left
        //indexes[jj++] = kk - 51;   // Top right

        Mesh mesh = new Mesh();
        mesh.name = "Generated Terrain";
        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indexes, MeshTopology.Lines, 0);

        AssetDatabase.CreateAsset(mesh, "Assets/Resources/Meshes/" + selected.name + "Terrain.asset");
    }
}