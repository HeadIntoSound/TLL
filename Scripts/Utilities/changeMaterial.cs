using UnityEngine;

// Changes the physicsMaterial of a collider
public static class changeMaterial
{
    static PhysicsMaterial2D phy = new PhysicsMaterial2D();
    public static void change(GameObject toChange, float friction, float bounciness)
    {
        phy.friction = friction;
        phy.bounciness = bounciness;
        foreach (Collider2D col in toChange.GetComponents<Collider2D>())
            col.sharedMaterial = phy;
    }
}
