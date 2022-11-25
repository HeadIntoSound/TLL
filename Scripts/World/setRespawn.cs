using UnityEngine;

// Used when the player passes near a checkpoint
public class setRespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            gameEvents.current.passPosition("setRespawn", other.transform.position);
    }
}
