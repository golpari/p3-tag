using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManagerTemmp : MonoBehaviour
{
    /*private PlayerInputManager playerInputManager;

    private InputActionAsset inputAsset;
    private InputActionMap joinMap;
    private InputAction joinAction;

    private void Awake()
    {
        playerInputManager = PlayerInputManager.instance;

        // Set up the input to recieve from both action maps
        inputAsset = GetComponent<PlayerInput>().actions;
        joinMap = inputAsset.FindActionMap("Join");
        joinAction = joinMap.FindAction("Join");
    }

    private void OnEnable()
    {   
        joinMap.Enable();
        joinAction.performed += OnJoinActionPerformed;
    }

    private void OnDisable()
    {
        joinMap.Disable();
        joinAction.performed -= OnJoinActionPerformed;
    }

    private void OnJoinActionPerformed(InputAction.CallbackContext context)
    {
        // Ensure the PlayerInputManager is not null
        if (playerInputManager == null)
        {
            Debug.LogError("PlayerInputManager instance is null.");
            return; // Exit the method if playerInputManager is null
        }
        if (playerInputManager.playerCount < 2)
        {
            // Ensure the device is not null
            if (context.control.device != null)
            {
                // Join the player with the device that triggered the Join action
                var playerInput = playerInputManager.JoinPlayer(pairWithDevice: context.control.device);

                // Check if the playerInput is not null after joining
                if (playerInput != null)
                {
                    // Assign the action map depending on the player index
                    if (playerInput.playerIndex == 0)
                    {
                        playerInput.SwitchCurrentActionMap("Player");
                        Debug.Log("Player joined as Player");
                    }
                    else if (playerInput.playerIndex == 1)
                    {
                        playerInput.SwitchCurrentActionMap("Ghost");
                        Debug.Log("Player joined as Ghost");
                    }
                }
                else
                {
                    Debug.LogError("Failed to join player.");
                }
            }
            else
            {
                Debug.LogError("Device is null.");
            }
        }
    }*/
    [SerializeField] List<GameObject> players = new List<GameObject>();
    private int index = 0;
    private PlayerInputManager playerInputManager;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = players[index];
    }

    public void SwitchPlayerPrefab(PlayerInput playerInput)
    {
        index++;
        playerInputManager.playerPrefab = players[index];
    }

    

}
