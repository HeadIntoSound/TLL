using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeathState : playerBaseState
{
    float startTime;
    float waitTime;
    public override void EnterState(playerController player)
    {
        startTime = Time.time;
        waitTime = animationInfo.animLength(player.dust.GetComponent<Animator>(), "Dust") + .2f + player.animationController.animLength("Death-DUST");
        player.animationController.setAnimation("Death-DUST");
    }

    public override void OnCollisionEnter(playerController player, Collision2D other)
    {
    }

    public override void OnCollisionExit(playerController player, Collision2D other)
    {
    }

    public override string stateName(playerController player)
    {
        return "death";
    }

    public override void Update(playerController player)
    {
        if (Time.time < startTime + waitTime)
        {
            player.Rb.velocity = Vector2.zero;
            if (Time.time > startTime + player.animationController.animLength("Death-DUST"))
            {
                player.dust.SetActive(true);
            }

        }
        else
        {
            Debug.Log("respawn damn fucker");
            player.dust.SetActive(false);
            gameEvents.current.passBool("death", true);
            gameEvents.current.passFloat("fade", 1);
            player.moveController.t = 0;
            player.states.transitionToState(player.states.idleState);
        }
    }
}
