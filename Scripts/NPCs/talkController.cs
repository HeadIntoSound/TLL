using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameEvents.current.onPassBool += talk;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void talk(string parameter, bool value)
    {
        if(parameter.Contains("talkResponse") && value)
            gameEvents.current.passBool("UI",true);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         gameEvents.current.passBool("talkCall", true);
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         gameEvents.current.passBool("talkCall", false);
    //     }
    // }


}
