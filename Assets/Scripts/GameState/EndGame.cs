using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public static int index = 0;
    [SerializeField] private GameObject text;

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

        public void restartInitiated(InputAction.CallbackContext ctx)
    {
        RestartGame();
        inputActions.UI.NewGame.performed -= restartInitiated;
    }
    void RestartGame()
    {

        inputActions.Player.Enable();
        inputActions.Ghost.Enable();

        if (PlayerController.num_lives <= 0) {
            PlayerController.num_lives = 3;
            SceneManager.LoadScene("Main Menu");
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       
        
    }
    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
