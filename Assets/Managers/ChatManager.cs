using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class ChatManager : NetworkBehaviour
{
    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private TMP_Text chatPanel;
    public PlayerData data;

    private void Start()
    {
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);
    }

    [Client]
    public void SendButton()
    {
        Debug.Log(chatInput.text);

        if (string.IsNullOrWhiteSpace(chatInput.text)) return;

        Debug.Log("LOLOS");

        CMD_Chat(data.playerName + ": " + chatInput.text);
        chatInput.text = string.Empty;
    }

    [Command]
    void CMD_Chat(string content)
    {
        //Check Chat here
        Debug.Log(content);
        RPC_Chat(content);
    }

    [ClientRpc(includeOwner = true)]
    void RPC_Chat(string content)
    {
        chatPanel.text += "\n" + content;
    }
}
