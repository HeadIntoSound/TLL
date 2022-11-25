using System;
using UnityEngine;

// Lots of things in the project communicate thru events
// There's a key parameter and a value of each type for lots of situations
// Can totally be improved or deprecated if considered
public class gameEvents : MonoBehaviour
{
    public static gameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<string, Rigidbody2D> onPassRb;
    public void passRb(string parameter, Rigidbody2D rb)
    {
        if (onPassRb != null)
            onPassRb(parameter, rb);
    }

    public event Action<string, bool> onPassBool;
    public void passBool(string parameter, bool value)
    {
        if (onPassBool != null)
        {
            onPassBool(parameter, value);
        }
    }

    // This is kinda useful but never used, investigate for future applications
    public event Action<string, Action<bool>> onCallback;
    public void callback(string parameter, Action<bool> response)
    {
        if (onCallback != null)
        {
            onCallback(parameter, response);
        }
    }

    public event Action<string, string> onPassValue;
    public void passValue(string parameter, string value)
    {
        if (onPassValue != null)
        {
            onPassValue(parameter, value);
        }
    }

    public event Action<string, float> onPassFloat;
    public void passFloat(string parameter, float value)
    {
        if (onPassFloat != null)
        {
            onPassFloat(parameter, value);
        }
    }

    public event Action<string,Vector3> onPassPosition;
    public void passPosition(string parameter, Vector3 value)
    {
        if (onPassPosition != null)
        {
            onPassPosition(parameter,value);
        }
    }

    // public event Action<float,float,Vector3> onCollisionDamage;
    // public void collisionDamage(float dmg, float knockBack,Vector3 direction)
    // {
    //     if(onCollisionDamage != null)
    //     {
    //         onCollisionDamage(dmg,knockBack,direction);
    //     }
    // }
}

