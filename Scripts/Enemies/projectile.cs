using UnityEngine;

// This controls the projectile properties and has a collision event
public class projectile : MonoBehaviour
{
    [SerializeField][Range(15,150)] float force;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Walkable"))
        {
            Destroy(gameObject,0.1f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            return;
        }
        if(other.transform.CompareTag("Player"))
        {
            float dir = (other.transform.position.x - transform.position.x)/Mathf.Abs(other.transform.position.x - transform.position.x);
            print(dir);
            Rigidbody2D rb = other.transform.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(dir*force,.3f),ForceMode2D.Impulse);
            Destroy(gameObject,0.25f);
        }
            
    }
}
