using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerComp))]
public class PlayerInteract : MonoBehaviour {

    public SphereCollider sphereCollider = null;
    private PlayerComp playerComp = null;

    // Bad for now, will make better later
    private const short E_PRESS = 0x0001;
    private short keyFlags      = 0x0000;

	// Use this for initialization
	protected void Start ()
    {
        playerComp = GetComponent<PlayerComp>();
	}
	
	// Update is called once per frame
	protected void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            keyFlags |= E_PRESS;
            InteractWithInteractable();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            keyFlags &= ~E_PRESS;
        }

    }

    private void InteractWithInteractable()
    {
        // if we did not press the E key
        if ((keyFlags & E_PRESS) == 0)
        {
            return;
        }

        // First check if we're mounted
        if ((playerComp.playerFlags & PlayerComp.c_MOUNTED) > 0)
        {
            UnMountVehicle();
        }
        else
        {
            Collider nearestCollider = CalculateNearestCollider();

            // nothing to do if no nearby collider
            if (nearestCollider == null)
                return;

            GameObject interactableObject = nearestCollider.gameObject;
            Debug.Log("Nearest GameObject: " + interactableObject.name);

            IInteractableComp interactableComp = interactableObject.GetComponent<IInteractableComp>();
            if (interactableComp != null)
            {
                Debug.Log("GameObject: " + interactableObject.name + " is Interactable");
                interactableComp.Interact(this.gameObject);
            }
        }

        // We processed the E Press
        keyFlags &= ~E_PRESS;
    }

    private Collider CalculateNearestCollider()
    {
        if (sphereCollider == null)
            return null;

        Collider[] colliders = Physics.OverlapSphere(sphereCollider.transform.position, sphereCollider.radius, LayerMask.GetMask("Interactable"));
        if (colliders.Length == 0)
            return null;

        // Iterate through the returned colliders and save the closest one
        Collider nearestCollider = null;
        Vector3 nearestPosDelta = new Vector3();
        foreach (Collider col in colliders)
        {
            if (nearestCollider == null)
            {
                nearestCollider = col;
                nearestPosDelta = nearestCollider.transform.position - transform.position;
            }
            else
            {
                Vector3 colPosDelta = col.transform.position - transform.position;

                if (colPosDelta.sqrMagnitude < nearestPosDelta.sqrMagnitude)
                {
                    nearestCollider = col;
                    nearestPosDelta = colPosDelta;
                }
            }
        }

        return nearestCollider;
    }

    // Called from InteractableComponent
    public void DriveVehicle(DrivingComponent drivingComp)
    {
        MountVehicle(drivingComp);

        // Probably set up the driveable vehicle so it can move
    }

    private void MountVehicle(DrivingComponent drivingComp)
    {
        Debug.Log("Mounting: " + drivingComp.gameObject.name);

        // Parent player to mount location
        gameObject.transform.SetParent(drivingComp.mountLocation, false);
        gameObject.transform.SetPositionAndRotation(drivingComp.mountLocation.position, drivingComp.mountLocation.rotation);
        

        playerComp.playerFlags |= PlayerComp.c_MOUNTED;
    }

    private void UnMountVehicle()
    {
        // Parent player to mount location
        gameObject.transform.parent = null;

        playerComp.playerFlags &= ~PlayerComp.c_MOUNTED;
    }
}
