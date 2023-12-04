using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    Subscription<EndGameEvent> gameEndSubscription;
    public Sprite player;
    public Sprite ghost;
    public GameObject text;

    bool GameEnd = false;

    void Start()
    {
        gameEndSubscription = EventBus.Subscribe<EndGameEvent>(_OnPlayerWin);
        GameEnd = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameEnd)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void _OnPlayerWin(EndGameEvent e)
    {
        if (!GameEnd) {
            if (e.playerWinnerName == "Ghost")
            {
                GameEnd = true;
                GetComponent<Image>().sprite = player;
                GetComponent<Image>().color = Color.red;

                text.SetActive(true);
            }
            else
            {
                GameEnd = true;
                GetComponent<Image>().sprite = ghost;
                GetComponent<Image>().color = Color.blue;

                text.SetActive(true);
            }
        }

    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
