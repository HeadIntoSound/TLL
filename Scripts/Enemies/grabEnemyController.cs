using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This controls the grab's behaviour
public class grabEnemyController : MonoBehaviour, IHasCooldown
{
    PointEffector2D effector;
    [SerializeField][Range(0.25f, 3)] float rearmTime = .25f;
    cooldownSystem cooldownSystem;
    [SerializeField][Range(0.1f,1.5f)] float releaseDistance;       // When the player is close to the center, stops the grab
    [SerializeField][Range(0.5f,1.5f)] float paralyzeDistance;      // When the player is at a certain distance, they can no longer move
    [SerializeField][Range(50,200)] float downwardPush;             // On release the player gets pushed down
    Animator anim;

    public int id => gameObject.GetInstanceID();

    public float cooldownDuration => rearmTime;

    void Awake()
    {
        effector = GetComponent<PointEffector2D>();
        cooldownSystem = GetComponent<cooldownSystem>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cooldownSystem.isOnCooldown(id))
        {
            effector.enabled = true;
            anim.Play("Grab");
            StartCoroutine(other.GetComponent<playerMoveController>().slow(.66f,2));
        }
            
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(Vector3.Distance(transform.position, other.transform.position) < paralyzeDistance)
                gameEvents.current.passBool("paralyze",true);
            if(Vector3.Distance(transform.position, other.transform.position) < releaseDistance)
            {
                // here we can put a timer to release
                other.attachedRigidbody.velocity = Vector2.zero;
                other.attachedRigidbody.AddForce(Vector2.down*downwardPush);
                effector.enabled = false;
            }
                
            cooldownSystem.putOnCooldown(this);
        }
            
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameEvents.current.passBool("paralyze",false);
            anim.Play("Grab-IDLE");
        }
    }
}
