using System.Linq;
using UnityEngine;

// This state is triggered when the player isn't moving
// I used this state as a transition state between some of the others
// For example, from landing to walk, first the player enters idle
// Idle can only be triggered if the player is standing on something
public class playerIdleState : playerBaseState
{
    public override void EnterState(playerController player)
    {
        if (raycastCheck.line(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable")))
        {
            player.animationController.setAnimation("Idle");

            player.atributes.xMoveMul = 1;
            player.atributes.speed = player.atributes.maxSpeed;
            player.atributes.currentJumpCharges = 2;

            player.Rb.velocity = Vector2Int.RoundToInt(player.Rb.velocity);
            player.Rb.gravityScale = player.atributes.initialGravity;
            player.Rb.isKinematic = false;

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Rope"), false);
            changeMaterial.change(player.gameObject, 0, 0);
        }
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "idle";
    }

    public override void Update(playerController player)
    {
        if (!raycastCheck.line(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable")))
        {
            if (player.Rb.velocity.y < -.75f)
                player.states.transitionToState(player.states.fallingState);
        }
        else if (Mathf.Abs(player.Rb.velocity.x) > 0.05f)
            player.states.transitionToState(player.states.walkState);

    }
}
