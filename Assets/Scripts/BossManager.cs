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
    public float spawnTimer = 2f;
    public float enemySpawnRate = 3f;

    public string bossName;
    BossHealthBar bossHealthBar;
    EnemyStats enemyStats;
    EnemyAnimatorManager enemyAnimatorManager;
    EnemyManager enemyManager;

    AttackState attackState;

    public bool firstDeath = false;
    public float firstDeathTimer = 5f;

    public GameObject fireballSpawnSpot;
    public GameObject fireballOBJ;
    GameObject fireballClone;
    public float projectileSpeed;

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

                    for (int i = 0; i < spawns.Length; i++)
                    {
                        spawns[i].spawnFX.SetActive(true);
                    }

                    //Invoke("SpawnEnemy", 2f);

                    StartCoroutine("SpawnEnemyAfterTime");
                }

                if (enemiesSpawned == 4)
                {
                    finishedSpawning = true;
                    //StopAllCoroutines();
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

    IEnumerator SpawnEnemyAfterTime()
    {
        spawnTimer = 0;
        do
        {
            randomSpawn = Random.Range(0, spawns.Length);
            spawnTimer += Time.deltaTime;
            yield return new WaitForSeconds(2f);
        } while (usedSpawns.Contains(spawns[randomSpawn]) && spawnTimer <= 2);

        spawns[randomSpawn].SpawnEnemy();
        usedSpawns.Add(spawns[randomSpawn]);
        enemiesSpawned++;
    }

    public void SpawnProjectile()
    {
        fireballClone = Instantiate(fireballOBJ, fireballSpawnSpot.transform.position, fireballSpawnSpot.transform.rotation);
        Debug.Log("Spawned fireball");
    }

    public void ThrowProjectile()
    {
        fireballClone.GetComponent<SphereCollider>().enabled = true;
        fireballClone.GetComponent<Rigidbody>().velocity = (fireballSpawnSpot.transform.position - enemyManager.currentTarget.transform.position).normalized * projectileSpeed;
    }
}
