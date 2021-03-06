﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int xSize, ySize;

    private Vector3[] vertices;

    private Mesh mesh;

	// Use this for initialization
	void Awake () {
        StartCoroutine(Generate());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Editable Mesh";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return new WaitForSeconds(.05f);
            }
        }

        int[] triangles = new int[xSize * 6];
        for (int ti = 0, vi = 0, x = 0; x < xSize; x++, ti += 6, vi++)
        {
            triangles[ti] = vi;
            triangles[ti + 3] = triangles[ti + 2] = vi + 1;
            triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
            triangles[ti + 5] = vi + xSize + 2;
        }

        mesh.vertices = vertices;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        // render verts
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
