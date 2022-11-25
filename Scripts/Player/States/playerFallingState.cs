using UnityEngine;

// This state is triggered after the highest point of the jump or when falling/dropping off a platform
public class playerFallingState : playerBaseState
{
    float startTime;
    Collider2D platform;
    public override void EnterState(playerController player)
    {
        player.animationController.setAnimation("Fall");
        player.Rb.gravityScale = player.atributes.initialGravity;

        // Used to drop off a platform
        if (player.Movement.y < -.8f)
        {
            startTime = Time.time;

            // searchs for a platform 
            RaycastHit2D hit = raycastCheck.line(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable"), "Platform");
            if (hit.collider != null)
            {
                platform = hit.collider;
                foreach (Collider2D col in player.Colliders)
                    Physics2D.IgnoreCollision(col, platform, true); // Disables collision between bodies to drop off
            }
        }
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Walkable") && raycastCheck.line(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable")))
        {
            gameEvents.current.passBool("cam",true);
            player.states.transitionToState(player.states.landState);
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Walkable") && raycastCheck.line(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable"), "Trampoline"))
        {
            player.atributes.currentJumpCharges = 2;
            player.states.transitionToState(player.states.jumpState);
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls") && Vector2.Distance(player.hitPosition, player.transform.position) > 1)
        {
            player.states.transitionToState(player.states.wallState);
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Rope"))
        {
            ropeInteraction.grab(player.Hj, other.transform.GetComponentInParent<ropeController>(), player.Colliders);
            player.states.transitionToState(player.states.swingState);
            return;
        }
        if (other.transform.CompareTag("Ladder"))
            player.states.transitionToState(player.states.climbState);

    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "falling";
    }

    public override void Update(playerController player)
    {
        if (!player.CamOffset.isActiveAndEnabled && player.Rb.velocity.y < 0
            && !raycastCheck.line(player.transform.position, Vector2.down, 8, LayerMask.NameToLayer("Walkable")))
            player.CamOffset.enabled = true;
        // activates the collision again
        if (Time.time > startTime + .5f && platform != null)
        {
            foreach (Collider2D col in player.Colliders)
                Physics2D.IgnoreCollision(col, platform, false);
        }
        if (player.Rb.velocity.y < 0.1f && raycastCheck.line(player.StartCastPos, Vector2.down, .5f, LayerMask.NameToLayer("Walkable")))
        {
            player.states.transitionToState(player.states.landState);
        }

    }
}
