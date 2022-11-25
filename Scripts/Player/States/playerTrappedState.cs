using UnityEngine;

// This state is triggered when the player falls into a trap and can't move unless mashing the movement left and right
// It can be used for other situations and can be built uppon
public class playerTrappedState : playerBaseState
{
    public override void EnterState(playerController player)
    {
        player.atributes.speed = 0;
        player.animationController.setAnimation("Idle");
        gameEvents.current.passBool("cam",true);
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "trapped";
    }

    public override void Update(playerController player)
    {
        player.Rb.velocity = Vector2.zero;
    }
}
