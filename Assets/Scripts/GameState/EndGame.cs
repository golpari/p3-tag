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
    string current_winner = "";
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
        current_winner = e.playerWinnerName;
        inputActions.Player.Disable();
        inputActions.Ghost.Disable();

        inputActions.UI.NewGame.Enable();
        // Bind the restart game action to be triggered by any key or button press
        StartCoroutine(transition_scene());
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
        yield return new WaitForSeconds(0.75f);
        inputActions.UI.NewGame.performed += restartInitiated;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
