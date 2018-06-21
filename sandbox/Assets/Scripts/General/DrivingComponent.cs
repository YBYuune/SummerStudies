using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingComponent : MonoBehaviour, IInteractableComp {

    public Transform mountLocation = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Interact(GameObject interactingObject)
    {
        PlayerInteract playerInteractComp = interactingObject.GetComponent<PlayerInteract>();

        if (playerInteractComp)
        {
            playerInteractComp.DriveVehicle(this);
        }
    }
}
