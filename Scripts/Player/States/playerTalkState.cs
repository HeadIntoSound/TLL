using UnityEngine;

// This state is triggered when the player presses the interaction button near an NPC
public class playerTalkState : playerBaseState
{
    public override void EnterState(playerController player)
    {
        gameEvents.current.passBool("talkResponse", true);
        player.animationController.setAnimation("Idle");
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "talk";
    }

    public override void Update(playerController player)
    {
        player.Rb.velocity = Vector2.zero;
    }
}
