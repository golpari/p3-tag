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
        Debug.Log("On Game End#");

        // Bind the restart game action to be triggered by any key or button press
        inputActions.Player.NewGame.performed += restartInitiated;
        inputActions.Ghost.NewGame.performed += restartInitiated;
        inputActions.Player.NewGame.Enable();
        inputActions.Ghost.NewGame.Enable();
    }
    private void restartInitiated(InputAction.CallbackContext ctx)
    {
        RestartGame();
    }
    void RestartGame()
    { 
        inputActions.Player.NewGame.Disable();
        inputActions.Ghost.NewGame.Disable();
        inputActions.Player.NewGame.performed -= restartInitiated;
        inputActions.Ghost.NewGame.performed -= restartInitiated;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
