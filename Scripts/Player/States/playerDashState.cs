using UnityEngine;

// This state makes sure the player exit other states and then pushes the player in the direction they're facing
public class playerDashState : playerBaseState, IHasCooldown
{
    public int id => 1;

    public float cooldownDuration => 2;

    float animationLength;
    float startTime;

    public override void EnterState(playerController player)
    {
        // Lets go of the rope
        ropeInteraction.letGo(player.Hj, player.Colliders);
        player.atributes.speed = player.atributes.maxSpeed;
        player.Rb.isKinematic = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Rope"), false);

        // Checks if the player can dash, if so, launches the rb
        if (player.atributes.currentDashCharges > 0 && !player.CooldownSystem.isOnCooldown(id))
        {
            player.animationController.setAnimation("Dash");
            startTime = Time.time;
            animationLength = player.animationController.animLength("Dash");
            player.Rb.AddForce(Vector2.right * player.LastDir * player.atributes.dashForce, ForceMode2D.Impulse);
            player.atributes.currentDashCharges--;
        }

    }

    // Makes sure to stop the velocity before entering another state
    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
        // Might be a bug with this, the player sometimes gets launched upwards when colliding with a platform at a certain angle
        if (other.gameObject.layer == LayerMask.NameToLayer("Walkable") || other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            player.Rb.velocity = Vector2.zero;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Rope"))
        {
            ropeInteraction.grab(player.Hj, other.transform.GetComponentInParent<ropeController>(), player.Colliders);
            player.states.transitionToState(player.states.swingState);
        }
        if (other.transform.CompareTag("Ladder"))
            player.states.transitionToState(player.states.climbState);
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "dash";
    }

    public override void Update(playerController player)
    {
        if (Time.time > startTime + animationLength * 0.85f)
        {
            if (player.atributes.currentDashCharges == 0)
                player.CooldownSystem.putOnCooldown(this);
            if (raycastCheck.line(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable")))
                player.states.transitionToState(player.states.landState);
            else
                player.states.transitionToState(player.states.fallingState);
        }

    }


}
