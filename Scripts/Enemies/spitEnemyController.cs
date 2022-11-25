using UnityEngine;

// This controls the spit's behaviour
// They call this enemy missile and maybe other names, it's not defined as of today
public class spitEnemyController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField][Range(.5f,10)] float fireRate;
    [SerializeField] Transform initialPos;              // Position from which the projectile will spawn
    Collider2D collidedObj;
    bool active;
    Animator anim;
    [SerializeField] float force;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void spit()
    {
        // Looks for the target
        float target = (collidedObj.transform.position - transform.position).normalized.x;

        anim.Play("Missile");
        // WARNING: apply object pooling <- it's really important to fix this
        GameObject proj = Instantiate(projectile, initialPos.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().AddForce(new Vector2(target, .3f) * force);
        Destroy(proj, 1.5f);
        // END OF WARNING
    }

    // When in range, starts spitting
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")&&!active)
        {
            collidedObj = other;
            active = true;
            InvokeRepeating("spit",0,fireRate);
        }
    }

    // When not in range, it stops
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            CancelInvoke();
            active = false;
            anim.Play("Missile-IDLE");
        }
            
    }

}
