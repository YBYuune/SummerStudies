using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int xSize, ySize;

    private Vector3[] vertices;

	// Use this for initialization
	void Awake () {
        Generate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Generate()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {

            }
        }
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
