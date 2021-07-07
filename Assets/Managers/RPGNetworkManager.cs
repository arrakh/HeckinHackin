using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class RPGNetworkManager : NetworkManager
{
    public List<Transform> spawnPositions;
    public ChatManager chatManager;

    public PlayerData clientData;
    public Leaderboard leaderboard;
    public Action OnConnected;
    public Action OnDisconnected;

    public List<MonsterSpawner> monsterSpawner;

    //public override void OnServerAddPlayer(NetworkConnection conn)
    //{
    //    int rand = Random.Range(0, spawnPositions.Count - 1);
    //    GameObject player = Instantiate(playerPrefab, spawnPositions[rand].position, spawnPositions[rand].rotation);
    //    NetworkServer.AddPlayerForConnection(conn, player);
    //}

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnConnected?.Invoke();

        conn.Send<PlayerData>(clientData);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        Debug.Log("Client disconnected");

        OnDisconnected?.Invoke();

        //SceneManager.LoadScene("Main");
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        //Run OnCreatePlayer on the server when a message is sent from client
        NetworkServer.RegisterHandler<PlayerData>(OnCreatePlayer);

        Debug.Log("Server Started");

        StartCoroutine(SpawnCoroutine());
    }

    private void OnCreatePlayer(NetworkConnection conn, PlayerData data)
    {
        //Get index for random spawn pos
        int rand = UnityEngine.Random.Range(0, spawnPositions.Count - 1);

        //Instantiate player
        GameObject playerGO = Instantiate(playerPrefab, spawnPositions[rand].position, spawnPositions[rand].rotation);

        //Init Player Data
        playerGO.GetComponent<PlayerScript>().Initialize(data, this);

        //Add player to connection
        NetworkServer.AddPlayerForConnection(conn, playerGO);

        chatManager.data = data;
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);
            foreach (var spawner in monsterSpawner)
            {
                spawner.SpawnCheck();
            }
        }
    }

    public Vector3 GetRandomSpawnPos()
    {
        int rand = UnityEngine.Random.Range(0, spawnPositions.Count - 1);
        return spawnPositions[rand].position;
    }
}

//Hotfix
[System.Serializable]
public struct PlayerData : NetworkMessage
{
    public string playerName;
}
