using UnityEngine;

// The player enters this state when collides with an object with the "Ladder" tag
public class playerClimbState : playerBaseState
{
    public override void EnterState(playerController player)
    {
        player.animationController.setAnimation("Climb");
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Walkable"))
            player.states.transitionToState(player.states.idleState);
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
        if(other.transform.CompareTag("Ladder"))
            player.states.transitionToState(player.states.fallingState);
    }

    public override string stateName(playerController player)
    {
        return "climb";
    }

    public override void Update(playerController player)
    {
        player.moveController.Climb(player.Movement.y);
    }
}
