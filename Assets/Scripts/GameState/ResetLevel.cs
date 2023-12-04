using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Place this script on a gameObject named GameManager in the first level.
 * In every scene, if the player dies, then the scene will resetload. 
 * The GameManager object should be preserved between scenes 
 * so that this condition can be checked in every level/environment.
 * */

public class ResetLevel : MonoBehaviour
{
    private Subscription<ThiefDiedEvent> thiefDiedSubscription;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<ThiefDiedEvent>(_OnThiefDied);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }*/

    void _OnThiefDied(ThiefDiedEvent e)
    {
        // when the thief dies, load the scene that you are in again
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(thiefDiedSubscription);
    }

}
