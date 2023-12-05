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
        Debug.Log("Goes in here");
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
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(0.50f);
        EventBus.Publish<Reset>(new Reset());
        GameEnd = false;
        EventBus.Publish<fadeOut>(new fadeOut(false));
        inputActions.UI.NewGame.performed += restartInitiated;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
