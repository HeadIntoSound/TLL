using UnityEngine;

// This state has 2 parts
// First it triggers the animation
// Then the player enters a "shield" in which is immovable
public class playerCrouchState : playerBaseState, IHasCooldown
{
    float startTime;
    float animationLength;

    public int id => 2;

    public float cooldownDuration => 3;

    public override void EnterState(playerController player)
    {
        player.atributes.speed = player.atributes.crouchSpeed;
        player.animationController.setAnimation("Shield");
        startTime = Time.time;
        animationLength=player.animationController.animLength("Shield");
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "crouch";
    }

    public override void Update(playerController player)
    {
        if(player.Rb.velocity.y<-.75f)
            player.states.transitionToState(player.states.fallingState);
        if(Time.time>startTime+animationLength)
            player.Rb.isKinematic = true;
        if(Time.time > startTime+animationLength+player.atributes.shieldDuration)
        {
            player.Rb.isKinematic = false;
            player.CooldownSystem.putOnCooldown(this);
        }
    }
}
