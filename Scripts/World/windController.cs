using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wind is kinda hard, this is an attempt at making it, not used now
public class windController : MonoBehaviour
{
    PointEffector2D effector;
    [SerializeField] float angle;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PointEffector2D>();
    }

    //turns off the effector once the player has been pushed;
    private void OnTriggerStay2D(Collider2D other)
    {
        angle = Vector2.Angle(transform.position, other.transform.position);
        if (other.CompareTag("Player") && angle <= 3)
            effector.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        effector.enabled = true;
    }

}
