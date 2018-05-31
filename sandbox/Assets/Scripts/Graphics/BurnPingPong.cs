using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnPingPong : MonoBehaviour {

    public AnimationCurve curve;

    private float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        GetComponent<Renderer>().material.SetFloat("_DissolveThreshold", curve.Evaluate(time));
	}
}
