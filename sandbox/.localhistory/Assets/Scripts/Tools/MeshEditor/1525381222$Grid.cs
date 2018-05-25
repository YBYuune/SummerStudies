using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int xSize, ySize;

    private Vector3[] vertices;

	// Use this for initialization
	void Awake () {
        StartCoroutine(Generate());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Generate()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return WaitForSeconds(.05f);
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
