using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreen : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private RPGNetworkManager networkManager;
    [SerializeField] private string ipToConnectTo;
    [SerializeField] private Toggle debugLocalhost;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject loadingCircle;

    private void Start()
    {
        loginButton.gameObject.SetActive(false);
        errorText.text = "";
    }

    //Will be called from Input Field
    public void InputFieldFunction(string input)
    {
        if(!loginButton.gameObject.activeSelf) loginButton.gameObject.SetActive(true);

        if (string.IsNullOrWhiteSpace(input))
        {
            errorText.text = "Name cannot be empty or contains only space!";
            loginButton.interactable = false;
        } 
        else if(input.Length > 20)
        {
            errorText.text = "Name cannot be longer than 20 characters!";
            loginButton.interactable = false;
        }
        else
        {
            errorText.text = "";
            loginButton.interactable = true;
            networkManager.clientData.playerName = input;
        }
    }

    //Will be called from Login Button
    public void LoginButton()
    {
        networkManager.networkAddress = debugLocalhost.isOn ? "localhost" : ipToConnectTo;
        networkManager.StartClient();
        //this.gameObject.SetActive(false);
        panel.SetActive(false);
        loadingCircle.SetActive(true);
    }

    public void DEBUG_CLIENTANDSERVER()
    {
        networkManager.networkAddress = debugLocalhost.isOn ? "localhost" : ipToConnectTo;
        networkManager.StartHost();
    }

    
}
