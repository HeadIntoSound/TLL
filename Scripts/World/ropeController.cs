using System.Collections.Generic;
using UnityEngine;

// Creates the rope at the start of the game, does some other things
public class ropeController : MonoBehaviour
{
    [SerializeField] GameObject node;
    [SerializeField] int amountOfNodes;
    public List<Rigidbody2D> nodesRigidbodies;
    Vector3 initialPos;
    [SerializeField] List<GameObject> rope;
    int midIndex;

    public void setInitialPos()
    {
        initialPos = transform.position + Vector3.down * transform.localScale.y / 2 + Vector3.down * node.transform.localScale.y / 2;
    }

    public void makeRope()
    {
        setInitialPos();
        for (int i = 0; i < amountOfNodes; i++)
        {
            Vector3 targetPos = initialPos + Vector3.down * (node.transform.localScale.y) * i;
            rope.Add(Instantiate(node, targetPos, Quaternion.identity, transform));
            rope[i].name = "Node " + i.ToString();
            nodesRigidbodies.Add(rope[i].GetComponent<Rigidbody2D>());
            if (rope.Count == 1)
            {
                rope[i].GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
                gameObject.GetComponent<HingeJoint2D>().connectedBody = nodesRigidbodies[i];
            }

            else
                rope[i].GetComponent<HingeJoint2D>().connectedBody = rope[i - 1].GetComponent<Rigidbody2D>();
        }

    }

    public void deleteRope()
    {
        for (int i = rope.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(rope[i]);
        }
        rope.Clear();
        nodesRigidbodies.Clear();
    }

    public Rigidbody2D midRb()
    {
        return nodesRigidbodies[midIndex];
    }

    // In order to avoid moving the nodes below the joint point, the collision is disabled between the player and the node
    public void ignoreCollisionFromMiddle(Collider2D[] colliders, bool ignore)
    {
        foreach (Rigidbody2D rb in nodesRigidbodies)
        {
            foreach (Collider2D col in colliders)
                Physics2D.IgnoreCollision(col, rb.GetComponent<Collider2D>(), ignore);
        }
    }

    private void Awake()
    {
        deleteRope();
        makeRope();
        midIndex = nodesRigidbodies.Count - 3;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
