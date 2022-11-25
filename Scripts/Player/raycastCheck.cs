using System.Linq;
using UnityEngine;

// Used to search for objects, mainly the ground when falling
public static class raycastCheck
{
    public static bool line(Vector3 startPos, Vector2 direction, float distance, int layer)
    {
        foreach (RaycastHit2D r in Physics2D.RaycastAll(startPos, direction, distance).ToList().FindAll(item => item.transform.gameObject != null))
        {
            if (r.transform.gameObject.layer == layer)
            {
                return true;
            }

        }
        return false;
    }

    // this overload can be used to check with tag and return a bool instead of a raycastHit
    //
    public static bool lineTag(Vector3 startPos, Vector2 direction, float distance, int layer, string tag)
    {
        foreach (RaycastHit2D r in Physics2D.RaycastAll(startPos, direction, distance,layer).ToList().FindAll(item => item.transform.gameObject != null))
        {
            Debug.Log(r.transform.tag);
            if (r.transform.CompareTag(tag))
                return true;
        }
        return false;
    }

    public static RaycastHit2D line(Vector3 startPos, Vector2 direction, float distance, int layer, string tag)
    {
        foreach (RaycastHit2D r in Physics2D.RaycastAll(startPos, direction, 1).ToList().FindAll(item => item.transform.gameObject != null))
        {
            if (r.transform.gameObject.layer == layer && r.transform.CompareTag(tag))
                return r;
        }
        return new RaycastHit2D();
    }
}
