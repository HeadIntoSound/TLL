using UnityEngine;

// This state is triggered when the player is walking over a "Walkable" object
// Movement isn't controlled here, only animation, properties and transitions
public class playerWalkState : playerBaseState
{
    public override void EnterState(playerController player)
    {
        player.atributes.xMoveMul = 1;
        player.atributes.speed = player.atributes.maxSpeed;
        player.Rb.isKinematic = false;
        player.animationController.setAnimation("Walk");
        player.atributes.currentJumpCharges = player.atributes.maxJumpCharges;
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
        if(other.transform.CompareTag("Ladder"))
            player.states.transitionToState(player.states.climbState);
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "walk";
    }

    public override void Update(playerController player)
    {
        if (!player.CooldownSystem.isOnCooldown(1) && player.atributes.currentDashCharges < 1)
            player.atributes.currentDashCharges = player.atributes.maxDashCharges;
        if (player.Rb.velocity.y < -.75f && !raycastCheck.line(player.StartCastPos,Vector2.down,.33f,LayerMask.NameToLayer("Walkable")))
            player.states.transitionToState(player.states.fallingState);
        if (player.Rb.velocity.magnitude < 0.01f)
            player.states.transitionToState(player.states.idleState);
    }
}
