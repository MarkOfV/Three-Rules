using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemySpawner : MonoBehaviour
{
    public static event Action<GameObject> OnEnemySpawned;

    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public List<GameObject> SpawnAll()
    {
        var list = new List<GameObject>();
        foreach (var p in spawnPoints)
        {
            var e = Instantiate(enemyPrefab, p.position, Quaternion.identity);
            list.Add(e);
            OnEnemySpawned?.Invoke(e);
        }
        return list;
    }

}
