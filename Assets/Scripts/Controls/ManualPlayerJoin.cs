using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualPlayerJoin : MonoBehaviour
{
    public static event Action<InputDevice> onPlayerRequestedJoin;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.UI.Join.performed += HandleJoinRequest;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.UI.Join.performed -= HandleJoinRequest;
    }

    private void HandleJoinRequest(InputAction.CallbackContext context)
    {
        // This will retrieve the device that triggered the action.
        InputDevice device = context.control.device;

        // Signal that this device wants to join.
        onPlayerRequestedJoin?.Invoke(device);
    }
}
