using UnityEngine;

// When the player is falling and about to reach the floor, this state is triggered
// It's an interesting state to built uppon
// For exalmple: ignoring collision with platforms if the player is holding down the move down button  
public class playerLandState : playerBaseState
{
    float startTime;
    float animTime;
    public override void EnterState(playerController player)
    {
        player.CamOffset.enabled = false;
        if (raycastCheck.line(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable"), "Trampoline"))
        {
            player.atributes.currentJumpCharges = 2;
            player.states.transitionToState(player.states.jumpState);
            return;
        }
        
        player.Rb.velocity *= Vector2.right;
        startTime = Time.time;
        player.animationController.setAnimation("Land");
        animTime = player.animationController.animLength("Land");
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "land";
    }

    public override void Update(playerController player)
    {
        if (startTime + (animTime *.6f) < Time.time)
            player.states.transitionToState(player.states.idleState);
    }
}
