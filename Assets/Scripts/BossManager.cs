using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public EnemySpawner[] spawns;
    List<EnemySpawner> usedSpawns = new List<EnemySpawner>();
    bool finishedSpawning;

    int randomSpawn = 0;
    int enemiesSpawned = 0;
    float spawnTimer = 5f;
    public float enemySpawnRate = 3f;

    public string bossName;
    BossHealthBar bossHealthBar;
    EnemyStats enemyStats;
    EnemyAnimatorManager enemyAnimatorManager;
    EnemyManager enemyManager;

    AttackState attackState;

    public bool firstDeath = false;
    public float firstDeathTimer = 5f;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<BossHealthBar>();
        enemyStats = GetComponent<EnemyStats>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyManager = GetComponent<EnemyManager>();

        attackState = GetComponentInChildren<AttackState>();
    }

    private void Start()
    {
        bossHealthBar.SetBossName(bossName);
        bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
    }

    private void Update()
    {
        //spawnTimer -= Time.deltaTime;

        if (enemyStats.currentHealth <= 0)
        {
            enemyManager.enabled = false;
            firstDeathTimer -= Time.deltaTime;

            if (firstDeath == false && firstDeathTimer <= 0)
            {
                enemyManager.enabled = true;
                firstDeath = true;
                enemyStats.currentHealth = enemyStats.maxHealth;
                bossHealthBar.SetBossCurrentHealth(enemyStats.maxHealth);

                firstDeathTimer = 5f;

                enemyAnimatorManager.animator.SetTrigger("isRevived");
            }
            else if (firstDeath == true)
            {
                WorldColorManager.Instance.OnChangeWorldProfile();
                Destroy(gameObject, 5f);
            }
        }

        if (attackState.currentAttack != null)
        {
            if (attackState.currentAttack.actionAnimation == "cast start")
            {
                Debug.Log(attackState.currentAttack);
                enemyAnimatorManager.animator.SetTrigger("SpawnLoop");
                //gameObject.GetComponentInChildren<EnemySpawner>().SpawnEnemy();

                //if (spawnTimer <= 0 && enemiesSpawned < 4)
                if (enemiesSpawned < 4)
                {
                    Debug.Log("spawn");
                    SpawnEnemy();
                    spawnTimer = enemySpawnRate;
                }

                if (enemiesSpawned == 4)
                {
                    finishedSpawning = true;
                }

                EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
                bool allEnemiesDead = true;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].gameObject.GetComponentInParent<EnemyStats>().currentHealth > 0)
                    {
                        allEnemiesDead = false;
                        break;
                    }
                }

                if (allEnemiesDead && finishedSpawning)
                {
                    enemiesSpawned = 0;
                    usedSpawns.Clear();
                }
            } 
        }
    }

    public void SpawnEnemy()
    {
        do
        {
            randomSpawn = Random.Range(0, spawns.Length);
        } while (usedSpawns.Contains(spawns[randomSpawn]));

        spawns[randomSpawn].SpawnEnemy();
        usedSpawns.Add(spawns[randomSpawn]);
        enemiesSpawned++;
    }
}
