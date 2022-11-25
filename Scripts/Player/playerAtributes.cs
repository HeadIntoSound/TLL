using UnityEngine;

// All of the player properties and stats
[System.Serializable]
public class playerAtributes
{
    public float speed = 15;                                            // Player's speed at a given time.
    public readonly float maxSpeed = 15;                                // Player's max speed.
    public readonly float crouchSpeed = 5;                              // Player's speed while crouching.
    public readonly float maxY_Velocity = 15;                           // Velocity limit on the Y axis.
    public readonly float minY_Velocity = -15;                          // Velocity floor on the Y axis.
    [Range(0, .3f)]public readonly float m_MovementSmoothing = .05f;    // How much to smooth out the movement.
    [HideInInspector]public Vector3 currentVelocity = Vector3.zero;     // Velocity of the player at a given time.
    [HideInInspector]public Vector2 currentVelocity2 = Vector2.zero;    // Velocity of the player at a given time.
    public float jumpForce = 10;                                        // Amount of force added when the player jumps.
    public float doubleJumpForce = 7;                                   // Amount of force added when the player jumps in the air.
    public int currentJumpCharges = 2;                                  // Amount off jumps available.
    public int maxJumpCharges = 2;                                      // Max amount of jumps.
    [Range(40, 100)] public float dashForce;                            // Amount of force added when the player dashes.
    public int maxDashCharges = 2;                                      // Max amount of dashes.
    public int currentDashCharges = 2;                                  // Amount off dashes available.
    public float glideTime = 5;                                         // Time in seconds that the player can glide for.
    public float xMoveMul = 1;                                          // Variation in X axis speed
    public float shieldDuration = 1.5f;                                 // Shield duration in seconds
    public float initialGravity = 2;                                    // Base gravity of the player
}
