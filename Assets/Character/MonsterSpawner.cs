using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mirror;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterToSpawn;
    public float spawnRadius;
    public int spawnMax = 5;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public void Spawn()
    {
        var go = Instantiate(monsterToSpawn, transform.position.RandomCircle(spawnRadius) , Quaternion.identity);
        spawnedEnemies.Add(go);
        go.GetComponent<Enemy>().OnDeath += delegate { spawnedEnemies.Remove(go); /*MoveObj(go);*/ };
        NetworkServer.Spawn(go);
        //Debug.Log("Enemy Spawned!");
    }

    public void SpawnCheck()
    {
        if (spawnedEnemies.Count < spawnMax) Spawn();
    }

    void MoveObj (GameObject obj)
    {
        obj.transform.position = obj.transform.position.RandomCircle(spawnRadius);
    }

#if UNITY_EDITOR
    //GIZMO
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, spawnRadius);
    }
#endif
}
