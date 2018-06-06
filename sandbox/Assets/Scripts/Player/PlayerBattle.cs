using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public Collider[] attackHitBoxes;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.G))
        {
            LaunchAttack(attackHitBoxes[0]);
        }
        
    }

    private void LaunchAttack(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            Debug.Log(c.name);
        }
    }
}

public class AttackNode
{
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



    public AttackNode(AttackID id)
    {
        this.ID = id;
    }

    public void Update(float deltaTime)
    {

    }

    bool AddAttack(AttackNode node)
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
}