using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Well, teleports the player to the last checkpoint
public class respawn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameEvents.current.passBool("death", true);
        }

    }
}
