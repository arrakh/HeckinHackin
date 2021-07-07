using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RPGNetworkManager networkManager;
    [SerializeField] private GameObject menuGroup;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip ambience;

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

    private void Start()
    {

        AudioManager.Instance.PlayBGM(menuMusic);
    }

    private void OnConnected()
    {
        AudioManager.Instance.PlayBGM(ambience);
        menuGroup.SetActive(false);
    }

    private void OnDisconnected()
    {
        menuGroup.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
