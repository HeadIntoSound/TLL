using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This state is triggered when the player jumps
// This can be improved to avoid moving faster when jumping
// I did that because we wanted a more horizontal jump
public class playerJumpState : playerBaseState
{
    float startTime = 0;
    public override void EnterState(playerController player)
    {
        ropeInteraction.letGo(player.Hj, player.Colliders);
        player.atributes.speed = player.atributes.maxSpeed;
        player.Rb.isKinematic = false;
        startTime = Time.time;
        changeMaterial.change(player.gameObject, 0, 0);

        if (player.atributes.currentJumpCharges > 0)
        {
            player.atributes.xMoveMul = 2;
            float usedForce = player.atributes.maxJumpCharges ==
                    player.atributes.currentJumpCharges ? player.atributes.jumpForce : player.atributes.doubleJumpForce;
            player.Rb.velocity = Vector2.zero;
            player.Rb.AddForce(Vector2.up * usedForce, ForceMode2D.Impulse);
            player.animationController.setAnimation("Jump");
            player.atributes.currentJumpCharges--;
        }
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls") && Vector2.Distance(player.hitPosition, player.transform.position) > 1)
            player.states.transitionToState(player.states.wallState);
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
        return "jump";
    }

    public override void Update(playerController player)
    {
        if (Time.time > startTime + 0.25f)
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Rope"), false);
        if (player.atributes.xMoveMul > 1)
            player.atributes.xMoveMul -= Time.deltaTime / 2;
        if (player.Rb.velocity.y < -.75f)
            player.states.transitionToState(player.states.fallingState);
        if (player.Rb.velocity.y < 0.1f && raycastCheck.line(player.StartCastPos, Vector2.down, .5f, LayerMask.NameToLayer("Walkable")))
            player.states.transitionToState(player.states.landState);
    }
}
