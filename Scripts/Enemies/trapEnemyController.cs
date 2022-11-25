using System.Collections;
using UnityEngine;

// This controls the trap's behaviour
public class trapEnemyController : MonoBehaviour, IHasCooldown
{
    [SerializeField][Range(0.25f, 3)] float rearmCooldown = .25f;
    [SerializeField] GameObject effector;
    Collider2D col;
    [SerializeField] Animator anim0;
    [SerializeField] Animator anim1;
    public int id => gameObject.GetInstanceID();
    public float cooldownDuration => rearmCooldown;
    cooldownSystem cooldownSystem;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        cooldownSystem = GetComponent<cooldownSystem>();
        gameEvents.current.onPassBool += delegate (string parameter, bool value)
        {
            if (parameter.Contains("trap") && !value)
            {
                print("done");
                playAnim(true);
            }
                
        };
    }

    // This should be optimized, animation names are really a mess
    // Through the project the animations are controlled in different ways, that can be optimized
    void playAnim(bool reverse)
    {
        if (!reverse)
        {
            anim0.Play("TRAP-BACK");
            anim1.Play("TRAP");
        }
        else
        {
            anim0.Play("TRAP-BACK-inverse");
            anim1.Play("TRAP-inverse");
        }

    }

    // Traps the player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cooldownSystem.isOnCooldown(id))
        {
            effector.SetActive(false);
            playAnim(false);
            gameEvents.current.passBool("trap", true);
            print("hit");
        }

    }

    // Starts the rearm process
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cooldownSystem.isOnCooldown(id))
        {
            cooldownSystem.putOnCooldown(this);
            col.enabled = false;
            anim0.Play("TRAP-BACK-idle");
            anim1.Play("TRAP-idle");
            StartCoroutine(turnOnEffector());
        }

    }

    IEnumerator turnOnEffector()
    {
        yield return new WaitForSeconds(rearmCooldown);
        effector.SetActive(true);
        col.enabled = true;
    }
}
