using UnityEngine;

// This controls the venom's behaviour
// As of now, it's disabled, it lacks the screen shader controller and some other features
public class venomEnemyContoller : MonoBehaviour, IHasCooldown
{
    public int id => gameObject.GetInstanceID();
    public float cooldownDuration => 10;
    cooldownSystem cooldownSystem;

    [SerializeField] float slowDuration = 2;
    Animator anim;


    void Start()
    {
        cooldownSystem = GetComponent<cooldownSystem>();
        anim = GetComponentInChildren<Animator>();
    }

    // Slows the player
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !cooldownSystem.isOnCooldown(id))
        {
            anim.Play("Venom");
            StartCoroutine(other.GetComponent<playerMoveController>().slow(.5f,slowDuration));
            cooldownSystem.putOnCooldown(this);
        }
    }

    // Enters cooldown
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") && cooldownSystem.isOnCooldown(id))
        {
            anim.Play("Venom-IDLE");
        }
    }
}
