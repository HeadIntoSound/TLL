using UnityEngine;

// For the first presentation, the ropes were disabled
// This controls the interaction between player and rope
// More can be built uppon, for example going up and down the nodes of the rope
public class playerSwingState : playerBaseState
{
    public override void EnterState(playerController player)
    {
        player.Hj.enabled = true;
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
            
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "swing";
    }

    public override void Update(playerController player)
    {
        player.Rb.AddForce(new Vector2(player.LastDir,0),ForceMode2D.Impulse);
    }
}
