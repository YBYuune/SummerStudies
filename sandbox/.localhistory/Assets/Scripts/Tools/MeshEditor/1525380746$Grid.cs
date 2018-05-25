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
    }
}
