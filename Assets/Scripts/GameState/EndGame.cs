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

    bool GameEnd = false;

    Subscription<EndGameEvent> gameEndSubscription;
    // Reference to your input actions
    private PlayerInputActions inputActions;
    string current_winner = "";
    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void Start()
    {
        gameEndSubscription = EventBus.Subscribe<EndGameEvent>(_OnGameEnd);
        GameEnd = false;
    }

    void _OnGameEnd(EndGameEvent e)
    {
        if (!GameEnd) {
            GameEnd = true;
            current_winner = e.playerWinnerName;
            
            /*
            inputActions.Player.Disable();
            inputActions.Ghost.Disable();

            inputActions.UI.NewGame.Enable();
            */
            StartCoroutine(transition_scene());
            // Bind the restart game action to be triggered by any key or button press
        }
        
    }

    public void restartInitiated(InputAction.CallbackContext ctx)
    {
    RestartGame();
    inputActions.UI.NewGame.performed -= restartInitiated;
    }
    void RestartGame()
    {
        Debug.Log("If it goes in here we need to restart application");
        inputActions.Player.Enable();
        inputActions.Ghost.Enable();

        if (current_winner == "Ghost")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            SceneManager.LoadScene(0);
        }

       
        
    }

    public IEnumerator transition_scene() {
        EventBus.Publish<fadeOut>(new fadeOut(true));
        PlayerController.player_lock = true;
        GhostController.ghost_lock = true;
        yield return new WaitForSeconds(3.0f);
        GameEnd = false;
        EventBus.Publish<fadeOut>(new fadeOut(false));
        EventBus.Publish<Reset>(new Reset());
        //inputActions.UI.NewGame.performed += restartInitiated;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
