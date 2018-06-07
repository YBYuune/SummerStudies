using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define delegate signature of event
public delegate void MoveStateEndEventHandler(object sender, MoveStateEndArgs e);

public class PlayerBattle : MonoBehaviour
{
    public Collider[] attackHitBoxes;
    private List<AttackNode> attackNodes = new List<AttackNode>();

    private AttackNode currentNode = null;

    public enum CharState : short
    {
        Idle,
        Attack,
        Dodge
    }

    private CharState myState;

	// Use this for initialization
	void Start ()
    {
        myState = CharState.Idle;

        AttackNode.MoveStateEvent += new MoveStateEndEventHandler(OnMoveStateEndEvent);

        LinkCollidersToNodes();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.G))
        {
            LaunchAttack();
        }

        // Always update the current node so that the move timer runs out
        UpdateAttack();
    }

    private void LaunchAttack()
    {
        AttackNode nextNode = null;

        if (currentNode == null)
            nextNode = attackNodes[0];
        else
        {
            nextNode = currentNode.Next();
            if (nextNode == null)
            {
                // Do nothing, let attack time out
            }
            else
            {
                nextNode = currentNode.Next();
            }
        }

        if (nextNode != null)
        {
            // Set current state to attacking
            myState = CharState.Attack;

            nextNode.StartAttack(1.0f);

            // This delay in setting the current node is so that 
            // If the current attack was activated previously, 
            // it won't re-start itself
            currentNode = nextNode;
        }


        // TODO: Do a hitstun on hit opponents
        // TODO: Apply damage
    }

    private void UpdateAttack()
    {
        if (currentNode != null)
        {
            // Get colliders overlapping with collider of currentAttackNode
            Collider[] cols = Physics.OverlapBox(currentNode.AttackCollider.bounds.center, currentNode.AttackCollider.bounds.extents, currentNode.AttackCollider.transform.rotation, LayerMask.GetMask("Hurtbox"));
            foreach (Collider c in cols)
            {
                Debug.Log(currentNode.ID + " collided with: " + c.name);
            }

            currentNode.Update(Time.deltaTime);
        }
    }

    private void OnMoveStateEndEvent(object sender, MoveStateEndArgs e)
    {
        if (e.GetType == typeof(AttackNode))
        {
            // Check which attack it was
            Debug.Log("Attack that ended is: " + ((AttackNode)sender).ID);
        }

        myState = CharState.Idle;
        currentNode = null;
    }

    private void LinkCollidersToNodes()
    {
        // Manual creation of 3 basic attacks
        attackNodes.Add(new AttackNode(AttackNode.AttackID.BASIC1));
        attackNodes.Add(new AttackNode(AttackNode.AttackID.BASIC2));
        attackNodes.Add(new AttackNode(AttackNode.AttackID.BASIC3));

        // Manual linking of attacks for combo routes
        attackNodes[0].AddAttack(attackNodes[1]);
        attackNodes[1].AddAttack(attackNodes[2]);

        // Do a 1 to 1 setup of collider to node for now
        for (int i = 0; i < attackHitBoxes.Length; ++i)
        {
            attackNodes[i].AttackCollider = attackHitBoxes[i];
        }
    }
}

public class AttackNode
{
    // Create handler to consume MoveStateEndEvent
    public static event MoveStateEndEventHandler MoveStateEvent;

    public enum AttackID : short
    {
        BASIC1 = 0,
        BASIC2 = 1,
        BASIC3 = 2
    }

    // ID denotes which attack
    public AttackID ID { get; set; }

    // nextAttacks lead to other attacks in the chain
    private List<AttackNode> nextAttacks;

    // HitBox
    public Collider AttackCollider { get; set; }

    // Timers
    public bool IsActive { get; set; }
    public float AttackTimer { get; set; }


    public AttackNode(AttackID id)
    {
        this.ID = id;
        this.AttackTimer = -1.0f;
        this.IsActive = false;

        this.nextAttacks = new List<AttackNode>();
    }

    public void Update(float deltaTime)
    {
        // TODO: Add startup timer
        // TODO: Add active timer
        // TODO: Add recovery timer
        // TODO: Add chainstartup timer
        // TODO: Add chainActive timer


        // This is before the active state check so that the 
        // timer will run
        if (AttackTimer > 0.0f)
        {
            AttackTimer -= deltaTime;
        }

        // DEBUG
        // Show collider if timer is running
        DEBUGShowCollider(AttackTimer > 0.0f);

        // Quit if we're inactive
        if (!IsActive)
        {
            return;
        }

        // If the attack has ended, raise MoveStateEnd event
        if (AttackTimer <= 0.0f)
        {
            OnMoveStateEvent(new MoveStateEndArgs(typeof(AttackNode)));
        }
    }

    private void DEBUGShowCollider(bool enabled)
    {
        // DEBUG
        // Show collider if timer is running
        MeshRenderer[] mRenders = AttackCollider.GetComponentsInParent<MeshRenderer>();
        if (mRenders.Length > 0)
        {
            mRenders[0].enabled = enabled;
        }
    }

    // For now, set timer in method
    // In the future, each attack will have their own time
    public void StartAttack(float timer)
    {
        IsActive = true;
        AttackTimer = timer;
    }

    // Used for setting members to their
    // deactivated states
    // Can be used to force the attack
    // to an deactivated state
    public void EndAttack()
    {
        IsActive = false;
        AttackTimer = -1.0f;
        DEBUGShowCollider(false);
    }

    public bool AddAttack(AttackNode node)
    {
        // don't add if the node has same id as us
        // or if we already lead to that attack
        if (node.ID == ID || nextAttacks.Contains(node))
        {
            return false;
        }

        // Add node as possible chain route
        nextAttacks.Add(node);
        return true;
    }

    public AttackNode Next()
    {
        if (nextAttacks.Count > 0)
        {
            // Stop current attack
            EndAttack();

            // Return next attack in chain
            return nextAttacks[0];
        }
        else
        {
            return null;
        }
    }

    // Protected OnMoveStateEvent raises the event by invoking
    // the delegates that have subscribed to the class's event
    protected virtual void OnMoveStateEvent(MoveStateEndArgs e)
    {
        MoveStateEndEventHandler handler = MoveStateEvent;
        if (handler != null)
        {
            handler(this, e);
        }

        // Deactivate the node because the timer has run out
        EndAttack();
    }
}

public class MoveStateEndArgs : EventArgs
{
    private Type type;

    public MoveStateEndArgs(Type type)
    {
        this.type = type;
    }

    // Property getter
    public new Type GetType
    {
        get { return type; }
    }
}