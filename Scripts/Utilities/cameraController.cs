using UnityEngine;

// Totally deprecated, maybe something can be useful, tho it's a total mess
public class cameraController : MonoBehaviour
{
    // [SerializeField] GameObject player;
    // Vector3 target;
    // [SerializeField] float yOffset;
    // [SerializeField] float smoothTime;
    // Vector3 velocity = Vector3.zero;

    // private void Start()
    // {
    //     transform.position = player.transform.position + Vector3.up * yOffset - Vector3.forward * 10;
    //     gameEvents.current.onPassBool += warp;
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    //     target = new Vector3(player.transform.position.x, transform.position.y, -10);
    //     if (player.transform.position.y >= transform.position.y + yOffset * .25f)
    //         target = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, -10);
    //     else if (player.transform.position.y <= transform.position.y - yOffset)
    //         target = new Vector3(player.transform.position.x, player.transform.position.y - yOffset, -10);
    //     transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * smoothTime);
    // }

    // void warp(string parameter,bool value)
    // {
    //     if(parameter.Contains("moveCamera"))
    //         transform.position = player.transform.position + Vector3.up * yOffset - Vector3.forward * 10;
    // }
}
