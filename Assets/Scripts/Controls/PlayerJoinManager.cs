using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    // Using a List to keep track of the devices that have already joined.
    private List<InputDevice> joinedDevices = new List<InputDevice>();

    private void OnEnable()
    {
        ManualPlayerJoin.onPlayerRequestedJoin += OnPlayerJoinRequested;
    }

    private void OnDisable()
    {
        ManualPlayerJoin.onPlayerRequestedJoin -= OnPlayerJoinRequested;
    }

    private void OnPlayerJoinRequested(InputDevice device)
    {
        if (joinedDevices.Contains(device)) return; // Prevent duplicate joins

        GameObject instantiatedPlayer = null;

        if (!joinedDevices.Contains(device) && PlayerInput.all.Count == 0)
        {
            instantiatedPlayer = InstantiateAndSetup(player1Prefab);
        }
        else if (!joinedDevices.Contains(device) && PlayerInput.all.Count == 1)
        {
            instantiatedPlayer = InstantiateAndSetup(player2Prefab);
        }

        if (instantiatedPlayer != null)
        {
            PlayerInput playerInput = instantiatedPlayer.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.camera = Camera.main;
            }

            joinedDevices.Add(device);
            if (joinedDevices.Count == 2)
            {
                EventBus.Publish<StartGameEvent>(new StartGameEvent());
            }
        }
    }

    private GameObject InstantiateAndSetup(GameObject playerPrefab)
    {
        Vector3 spawnPosition;

        if (playerPrefab == player1Prefab)
        {
            Debug.Log("Here");
            spawnPosition = new Vector3(1f, 1.5999f, -12.61f); 
        }
        else if (playerPrefab == player2Prefab)
        {
            spawnPosition = new Vector3(3.56f, 1.59f, -8.51f);
        }
        else
        {
            return null; // Return null if the prefab does not match any known prefabs.
        }

        GameObject instantiatedPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        return instantiatedPlayer;
    }

}