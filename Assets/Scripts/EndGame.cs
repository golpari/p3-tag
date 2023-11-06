using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    Subscription<EndGameEvent> gameEndSubscription;
    // Reference to your input actions
    private PlayerInputActions inputActions;
    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void Start()
    {
        gameEndSubscription = EventBus.Subscribe<EndGameEvent>(_OnGameEnd);
    }

    void _OnGameEnd(EndGameEvent e)
    {
        inputActions.Player.Disable();
        inputActions.Ghost.Disable();
        inputActions.UI.NewGame.Enable();
        // Bind the restart game action to be triggered by any key or button press
        inputActions.UI.NewGame.performed += restartInitiated;
    }
    private void restartInitiated(InputAction.CallbackContext ctx)
    {
        RestartGame();
    }
    void RestartGame()
    { 
        inputActions.Player.Enable();
        inputActions.Ghost.Enable();
        inputActions.UI.NewGame.performed -= restartInitiated;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
