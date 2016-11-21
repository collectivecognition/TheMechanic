
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class Wireframe {
    private static Color color = new Color(0f, 1f, 0f, 1f);
    private static float intensity = 4f;

    [MenuItem("Tools/Generate Wireframe For GameObject")]
    private static void GenerateMesh() {
        GameObject selected = Selection.activeGameObject;

        // Grab the first mesh we find and make a copy of it

        Transform parent = selected.GetComponentInChildren<MeshFilter>().transform;
        Mesh oldMesh = selected.GetComponentInChildren<MeshFilter>().mesh;

        // Create a new mesh with the vertices / triangles from the original one

        Mesh newMesh = new Mesh();
        newMesh.vertices = oldMesh.vertices;
        int[] tris = oldMesh.triangles;

        // Scale the vertices of the mesh based on their normals,
        // which produces a scaled "shell" of the mesh so that our
        // lines will sit nicely on the outside of the existing geometry 
        // without overlapping

        float scale = .01f;

        Vector3[] vertices = newMesh.vertices;

        for (int ii = 1; ii < vertices.Length; ii++) {
            for (int jj = 0; jj < vertices.Length; jj++) {
                if (newMesh.vertices[ii] == newMesh.vertices[jj]) {
                    oldMesh.vertices[ii] += new Vector3(
                        scale * oldMesh.normals[jj].x / parent.localScale.x,
                        scale * oldMesh.normals[jj].y / parent.localScale.y,
                        scale * oldMesh.normals[jj].z / parent.localScale.z
                    );
                }
            }
        }

        newMesh.vertices = vertices;

        // Create a list of shared sides

        // FIXME: I don't understand why this algorithm doesn't require normal comparisions
        // in order to not cull the whole mesh?

        Dictionary<string, bool> sharedSides = new Dictionary<string, bool>();

        for (int ii = 0; ii < tris.Length; ii += 3) {
            for (int jj = 0; jj < tris.Length; jj += 3) {

                // No need to compare a triangle to itself

                if (ii != jj) {
                    int[] sharedSide = new int[2];
                    int vertexCount = 0;

                    if (tris[ii] == tris[jj] || tris[ii] == tris[jj + 1] || tris[ii] == tris[jj + 2]) {
                        sharedSide[vertexCount] = tris[ii];
                        vertexCount++;
                    }

                    if (tris[ii + 1] == tris[jj] || tris[ii + 1] == tris[jj + 1] || tris[ii + 1] == tris[jj + 2]) {
                        sharedSide[vertexCount] = tris[ii + 1];
                        vertexCount++;
                    }

                    if (tris[ii + 2] == tris[jj] || tris[ii + 2] == tris[jj + 1] || tris[ii + 2] == tris[jj + 2]) {
                        sharedSide[vertexCount] = tris[ii + 2];
                        vertexCount++;
                    }

                    string key = sharedSide[0] + "," + sharedSide[1];
                    string backwardsKey = sharedSide[1] + "," + sharedSide[0];

                    if (vertexCount == 2 && !sharedSides.ContainsKey(key)) { // No duplicates!
                        sharedSides.Add(key, true);
                        sharedSides.Add(backwardsKey, true);
                    }
                }
            }
        }
        
        int[] indexes = new int[tris.Length * 2];

        for (int i = 0, a = 0; i < tris.Length; i += 3) {
            if(!sharedSides.ContainsKey(tris[i].ToString() + "," + tris[i + 1].ToString())) {
                indexes[a++] = tris[i];
                indexes[a++] = tris[i + 1];
            }

            if (!sharedSides.ContainsKey(tris[i + 1].ToString() + "," + tris[i + 2].ToString())) {
                indexes[a++] = tris[i + 1];
                indexes[a++] = tris[i + 2];
            }

            if (!sharedSides.ContainsKey(tris[i + 2].ToString() + "," + tris[i].ToString())) {
                indexes[a++] = tris[i + 2];
                indexes[a++] = tris[i];
            }
        }

        newMesh.SetIndices(indexes, MeshTopology.Lines, 0);

        GameObject newGo = new GameObject();
        newGo.name = "Wireframe";
        newGo.transform.parent = parent;
        newGo.transform.position = parent.position;
        newGo.transform.localRotation = Quaternion.identity;
        newGo.transform.localScale = Vector3.one;

        MeshFilter newMeshFilter = newGo.AddComponent<MeshFilter>();
        newMeshFilter.mesh = newMesh;
        newGo.AddComponent<MeshRenderer>();

        newGo.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color * intensity);

        AssetDatabase.CreateAsset(newMesh, "Assets/Resources/Meshes/" + selected.name + "Wireframe.asset");
    }
}