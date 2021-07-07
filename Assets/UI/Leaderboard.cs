using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class Leaderboard : NetworkBehaviour
{
    public PlayerScript[] playerList;

    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private GameObject entryContainer;

    
    private void Start()
    {
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);

        if (!isServer) return;
        StartCoroutine(LeaderboardUpdate());
    }

    IEnumerator LeaderboardUpdate()
    {
        while (true)
        {
            playerList = FindObjectsOfType<PlayerScript>();
            playerList.OrderByDescending(x => x.coin);

            UpdateBoard(playerList);

            yield return new WaitForSeconds(0.5f);
        }
    }

    [ClientRpc]
    private void UpdateBoard(PlayerScript[] list)
    {
        entryContainer.transform.Clear();

        foreach (var p in list)
        {
            var entry = Instantiate(entryPrefab, entryContainer.transform, false).GetComponent<LeaderboardEntry>();
            entry.Initialize(p.data.playerName, p.GetCoin().ToString());
        }

    }
}
