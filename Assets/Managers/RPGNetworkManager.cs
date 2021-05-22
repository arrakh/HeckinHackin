using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RPGNetworkManager : NetworkManager
{
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private GameObject loginScreen;

    public PlayerData clientData;

    //public override void OnServerAddPlayer(NetworkConnection conn)
    //{
    //    int rand = Random.Range(0, spawnPositions.Count - 1);
    //    GameObject player = Instantiate(playerPrefab, spawnPositions[rand].position, spawnPositions[rand].rotation);
    //    NetworkServer.AddPlayerForConnection(conn, player);
    //}

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        //loginScreen.SetActive(false);

        conn.Send<PlayerData>(clientData);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        Debug.Log("Client disconnected");

        //loginScreen.SetActive(true);

        //SceneManager.LoadScene("Main");
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        //Run OnCreatePlayer on the server when a message is sent from client
        NetworkServer.RegisterHandler<PlayerData>(OnCreatePlayer);
    }

    private void OnCreatePlayer(NetworkConnection conn, PlayerData data)
    {
        //Get index for random spawn pos
        int rand = Random.Range(0, spawnPositions.Count - 1);

        //Instantiate player
        GameObject playerGO = Instantiate(playerPrefab, spawnPositions[rand].position, spawnPositions[rand].rotation);

        //Init Player Data
        //playerGO.GetComponent<Player>().Initialize(data);

        //Add player to connection
        NetworkServer.AddPlayerForConnection(conn, playerGO);
    }
}

//Hotfix
[System.Serializable]
public struct PlayerData : NetworkMessage
{
    public string playerName;
}
