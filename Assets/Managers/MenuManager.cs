using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RPGNetworkManager networkManager;
    [SerializeField] private GameObject menuGroup;

    private void OnEnable()
    {
        networkManager.OnConnected += OnConnected;
        networkManager.OnDisconnected += OnDisconnected;
    }

    private void OnDisable()
    {
        networkManager.OnConnected -= OnConnected;
        networkManager.OnDisconnected -= OnDisconnected;
    }

    private void OnConnected()
    {
        menuGroup.SetActive(false);
    }

    private void OnDisconnected()
    {
        menuGroup.SetActive(false);
    }
}
