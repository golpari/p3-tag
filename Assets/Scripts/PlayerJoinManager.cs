using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    public PlayerInput[] playerObjects;

    /*private void OnEnable()
    {
        // Subscribe to the event
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    }*/

    // This method is called when the onPlayerJoined event is triggered
    public void OnJoin(PlayerInput playerInput)
    {
        if (playerInput.playerIndex == 0)
        {
            playerInput.SwitchCurrentActionMap("Player");
            Debug.Log("Player joined");
        }
        else if (playerInput.playerIndex == 1)
        {
            playerInput.SwitchCurrentActionMap("Ghost");
            Debug.Log("Ghost joined");
        }

        // Here you would typically enable the object associated with the player,
        // but since they are already in the scene and just being enabled/disabled, 
        // you may not need to do anything else here.
    }

/*    public void JoinPlayer()
    {
        // This method would be called by your UI or by an InputAction callback when you want to join a player.
        PlayerInputManager.instance.JoinPlayer();
    }

    // Bind this to the UI button or the InputAction that should trigger the join
    public void OnJoinAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JoinPlayer();
        }
    }*/
}
