using UnityEngine;

// Cointains every state the player should have and a method to transition between them
public class playerStatesController : MonoBehaviour
{

    playerBaseState currentState;
    public playerController player;
    public playerBaseState CurrentState
    {
        get { return currentState; }
    }
    public readonly playerIdleState idleState = new playerIdleState();
    public readonly playerWalkState walkState = new playerWalkState();
    public readonly playerJumpState jumpState = new playerJumpState();
    public readonly playerDashState dashState = new playerDashState();
    public readonly playerCrouchState crouchState = new playerCrouchState();
    public readonly playerGlideState glideState = new playerGlideState();
    public readonly playerFallingState fallingState = new playerFallingState();
    public readonly playerWallState wallState = new playerWallState();
    public readonly playerSwingState swingState = new playerSwingState();
    public readonly playerClimbState climbState = new playerClimbState();
    public readonly playerTalkState talkState = new playerTalkState();
    public readonly playerTrappedState trappedState = new playerTrappedState();
    public readonly playerLandState landState = new playerLandState();
    public readonly playerDeathState deathState = new playerDeathState();

    public void transitionToState(playerBaseState state)
    {
        currentState = state;
        currentState.EnterState(player);
    }
}
