using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// Given that we use the new Input System, this class controls every event called by the playerInput component 
public class playerInputController : MonoBehaviour
{
    public playerController player;
    bool beingHeld;
    [SerializeField] GameObject pauseImage;

    public void move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.Movement = context.ReadValue<Vector2>();
            if (player.Movement.x != 0 && player.Movement.x != player.LastDir)
            {
                player.LastDir = (int)(player.Movement.x / Mathf.Abs(player.Movement.x));
            }
        }
        if (context.canceled)
            player.Movement = Vector2.zero;
    }

    public void crouch(InputAction.CallbackContext context)
    {
        if (player.states.CurrentState == player.states.deathState || player.states.CurrentState == player.states.trappedState)
            return;
        if (raycastCheck.line(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable")))
        {
            if (context.canceled)
            {
                player.states.transitionToState(player.states.idleState);
                return;
            }
            if (context.performed)
                player.states.transitionToState(player.states.crouchState);
        }
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.canceled || player.states.CurrentState == player.states.deathState || player.states.CurrentState == player.states.trappedState)
            return;
        if (context.performed && player.states.CurrentState != player.states.landState)
        {
            if (player.Movement.y <= -.9
                && raycastCheck.lineTag(player.StartCastPos, Vector2.down, .33f, LayerMask.NameToLayer("Walkable"), "Platform"))
                player.states.transitionToState(player.states.fallingState);
            else
                player.states.transitionToState(player.states.jumpState);
        }
    }

    public void glide(InputAction.CallbackContext context)
    {
        if (!raycastCheck.line(player.StartCastPos, Vector2.down, .05f, LayerMask.NameToLayer("Walkable")))
        {
            if (context.performed)
            {
                StopAllCoroutines();
                StartCoroutine(startGlide(player));
                beingHeld = true;
                return;
            }
            else
            {
                if ((context.started || context.canceled) && beingHeld)
                {
                    StopAllCoroutines();
                    player.states.transitionToState(player.states.fallingState);
                    beingHeld = false;
                    return;
                }
            }
        }

        if (context.canceled)
            beingHeld = false;
    }

    public void dash(InputAction.CallbackContext context)
    {
        if (player.states.CurrentState == player.states.trappedState || player.states.CurrentState == player.states.deathState)
            return;
        if (context.performed && player.atributes.currentDashCharges > 0)
            player.states.transitionToState(player.states.dashState);
    }

    public void mash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (player.states.CurrentState == player.states.trappedState)
                gameEvents.current.passBool("trap", false);
        }

    }

    public void interaction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            foreach (RaycastHit2D r in Physics2D.RaycastAll(player.transform.position, Vector2.right * player.LastDir, 2).ToList().FindAll(item => item.transform.gameObject != null))
            {
                if (r.transform.CompareTag("NPC"))
                    player.states.transitionToState(player.states.talkState);

            }
        }

    }

    public void pause(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
            pauseImage.SetActive(!pauseImage.activeSelf);
        }
    }

    // Waits a second before gliding, this should be improved
    IEnumerator startGlide(playerController player)
    {
        yield return new WaitForSeconds(.55f);
        if (!raycastCheck.line(player.StartCastPos, Vector2.down, .5f, LayerMask.NameToLayer("Walkable")))
            player.states.transitionToState(player.states.glideState);
    }

}
