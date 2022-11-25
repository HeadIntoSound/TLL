using UnityEngine;

// The game uses a finite state machine to control the properties and animations of the player
// This is the class from which all states will inherit their methods
public abstract class playerBaseState 
{
   public abstract void EnterState(playerController player);   // This triggers when the player enters a new state
   public abstract void Update(playerController player); // Can be called either in Update, FixedUpdate or LateUpdate
   public abstract void OnCollisionEnter(playerController player,Collision2D other);   // Same as unity's built in method
   public abstract string stateName(playerController player);     // This is for debugging, returns the name of the current state
   public abstract void OnCollisionExit(playerController player, Collision2D other);   // Same as unity's built in method
}
