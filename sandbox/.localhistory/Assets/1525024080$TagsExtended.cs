using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsExtended : MonoBehaviour {
    [Flags]
    public enum Tags : short
    {
        FLOOR = 0X01,
        PLAYER = 0X02,
        WALL = 0X04,
        PLAYERKILLABLE = 0X08,
        ENEMYKILLABLE = 0X16
    };

    public Tags[] tags;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
