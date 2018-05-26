using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerBattle : MonoBehaviour
{
    private CharacterController controller;

    public Collider[] attackHitBoxes;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void LaunchAttack(Collider col)
    {

    }
}
