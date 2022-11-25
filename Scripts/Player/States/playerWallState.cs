using UnityEngine;

// This state is triggered when the player is hanging to the wall
public class playerWallState : playerBaseState
{
    float t = 0;
    public override void EnterState(playerController player)
    {
        // For some reason, the animation wasn't made, here is the line that would trigger it
        //player.animationController.setAnimation( * clip's name* );
        t = 0;
        changeMaterial.change(player.gameObject, 1, 0);
        player.atributes.currentJumpCharges = 1;
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Walkable"))
            player.states.transitionToState(player.states.idleState);
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
        changeMaterial.change(player.gameObject, 0, 0);
        player.states.transitionToState(player.states.fallingState);
    }

    public override string stateName(playerController player)
    {
        return "wall";
    }

    public override void Update(playerController player)
    {
        if(t<=1)
        {
            changeMaterial.change(player.gameObject, Mathfx.Ease.CircleInOut(1,0,t), 0);
            t+= Time.deltaTime*0.25f;
        }
            
        
    }
}
