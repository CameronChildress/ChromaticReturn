using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
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
            for (int i = 0; i < attackState.enemyAttacks.Length; i++)
            {
                if (attackState.enemyAttacks[i].name == "SpawnADS01")
                {
                    Debug.Log("Spawn?");
                    gameObject.GetComponent<EnemySpawner>().SpawnEnemy();
                }
            }
        }
    }
}
