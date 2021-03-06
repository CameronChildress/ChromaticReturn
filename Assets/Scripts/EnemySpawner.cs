using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnableEnemies;
    public GameObject spawnFX;

    public void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, spawnableEnemies.Length);
        GameObject enemy = Instantiate(spawnableEnemies[randomEnemy], transform.position, transform.rotation);
        enemy.GetComponent<EnemyManager>().currentTarget = FindObjectOfType<PlayerManager>().GetComponent<PlayerStats>();
    }
}
