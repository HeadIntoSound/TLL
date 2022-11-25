using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This controls the bomb's behaviour
// Using a collider effector I created an object that pushes the player in a direction given by the angle between
//      player and bomb
public class bombEnemyController : MonoBehaviour, IHasCooldown
{
    
    [SerializeField][Range(1000, 3000)] int knockBackForce;
    [SerializeField][Range(.1f, 1)] float preparationTime;
    [SerializeField][Range(3, 20)] float rearmTime = 3;
    //PointEffector2D effector;
    AreaEffector2D effector;
    [SerializeField]Animator anim;

    public int id => gameObject.GetInstanceID();
    public float cooldownDuration => rearmTime;
    cooldownSystem cooldownSystem;

    void Awake()
    {
        cooldownSystem = GetComponent<cooldownSystem>();
        effector = GetComponent<AreaEffector2D>();
        effector.enabled = false;
        effector.forceMagnitude = knockBackForce;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!cooldownSystem.isOnCooldown(id) && other.CompareTag("Player"))
            StartCoroutine(explode(other));
    }

    IEnumerator explode(Collider2D other)
    {
        // Waits for a little
        anim.Play("Bomb");
        yield return new WaitForSeconds(preparationTime);
        // Angle calculation
        float angle = Vector2.SignedAngle(transform.position, other.transform.position);
        if (angle < 0)
            effector.forceAngle = angle + 180;
        else
            effector.forceAngle = angle;
        // Explosion
        effector.enabled = true;
        yield return new WaitForSeconds(0.2f);
        effector.enabled = false;
        // Rearm and cooldown
        cooldownSystem.putOnCooldown(this);
        anim.Play("Bomb-IDLE");
        yield break;
    }
}
