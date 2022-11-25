using UnityEngine;

// Used by the player to grab or let go the rope
public static class ropeInteraction
{
    public static void letGo(HingeJoint2D hj,Collider2D[] colliders)
    {
        if (hj.connectedBody != null)
        {
            hj.connectedBody.GetComponentInParent<ropeController>().ignoreCollisionFromMiddle(colliders,false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Rope"), true);
            hj.connectedBody = null;
            hj.enabled = false;
        }
    }
    public static void grab(HingeJoint2D hj, ropeController rc,Collider2D[] colliders)
    {
        hj.connectedBody = rc.midRb();
        rc.ignoreCollisionFromMiddle(colliders,true);
    }
}
