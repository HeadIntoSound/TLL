using UnityEngine;
using System.Collections;
using System;

// Controls the movement of the player, some
public class playerMoveController : MonoBehaviour
{
    [Range(0, .3f)][SerializeField] float m_MovementSmoothing = .05f;    // How much to smooth out the movement
    [SerializeField] float hauntTime = 5;                                // Idle time needed for the kill screen to start
    bool m_FacingRight = true;                                           // For determining which way the player is currently facing.
    Vector3 m_Velocity = Vector3.zero;                                   // Character's velocity
    Rigidbody2D rb;                                                      // Character's attached rigidbody
    float initialTime;                                                   // Used to know when the haunt screen should start counting to trigger
    public float t;
    float speedModifier = 1;                                             // Moves the player faster when jumping, can be used for other situations obviously
    bool dying;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void clampVelocity(float min, float max)
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, min, max));
    }

    public void Move(float move, float xMoveMul)
    {
        if (rb.velocity.magnitude < 0.05f)
            rb.velocity = Vector2.zero;
        Vector2 targetVel = new Vector2(move * 10 * xMoveMul * speedModifier, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    // If the player is haning from a "Ladder" controls the movement
    public void Climb(float move)
    {
        Vector2 targetVel = new Vector2(rb.velocity.x, move * 2.5f);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref m_Velocity, m_MovementSmoothing);
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // kill screen trigger
    public void haunt()
    {
        if (rb.velocity.magnitude < 0.01f)
        {
            if (initialTime == 0)
            {
                initialTime = Time.time;
                dying = false;
            }
        }
        else
        {
            initialTime = 0;
            if (t > 0)
            {
                gameEvents.current.passFloat("fade", Mathfx.Ease.CubicIn(1, 5, t));
                t -= Time.deltaTime * 0.1f;
            }
        }
        if (initialTime + hauntTime < Time.time && initialTime != 0)
        {
            gameEvents.current.passFloat("fade", Mathfx.Ease.CubicIn(1, 5, t));
            if (t <= 1)
                t += Time.deltaTime * 0.1f;
            if (t >= 1 && !dying)
            {
                dying = true;
                gameEvents.current.passBool("hauntDeath", true);
            }

        }
    }

    // slows movement
    public IEnumerator slow(float modifier, float time)
    {
        speedModifier = modifier;
        yield return new WaitForSeconds(time);
        speedModifier = 1;
        yield break;
    }
}
