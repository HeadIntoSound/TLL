using System.Linq;
using UnityEngine;

// After 1 second of any jump, if the player is holding down the jump key, they enter this state
// Gliding is achieved by making gravity 0 and gradualy approach the initial value
// That's why you can see another state setting gravity to 0 again when entering
public class playerGlideState : playerBaseState
{
    float startTime;
    float t = 0;
    public override void EnterState(playerController player)
    {
        player.animationController.setAnimation("Glide");
        t = 0;
        startTime = Time.time;
        player.Rb.velocity = new Vector2(player.Rb.velocity.x, 0);
        player.Rb.gravityScale = 0;
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Walkable") && raycastCheck.line(player.StartCastPos,Vector2.down,.33f,LayerMask.NameToLayer("Walkable")))
            player.states.transitionToState(player.states.idleState);
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls") && Vector2.Distance(player.hitPosition, player.transform.position) > 1)
            player.states.transitionToState(player.states.wallState);
        if (other.transform.CompareTag("Ladder"))
            player.states.transitionToState(player.states.climbState);
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "glide";
    }

    public override void Update(playerController player)
    {
        if (t <= 1 || Time.time > startTime + player.atributes.glideTime)
        {
            player.Rb.gravityScale = Mathfx.Ease.CircleInOut(0, player.atributes.initialGravity, t);
            t += Time.deltaTime * .5f;

        }
        else
            player.states.transitionToState(player.states.fallingState);

    }
}
