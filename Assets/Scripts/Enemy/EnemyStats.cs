using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStats
{
    EnemyMovement enemyMovement;
    PlayerStats playerStats;
    BossManager bossManager;
    EnemyAnimatorManager enemyAnimatorManager;
    NavMeshAgent navMeshAgent;

    public HealthBar healthBar;

    public int ChromaOrbsToGive = 100;

    //Animator animator;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        playerStats = FindObjectOfType<PlayerStats>();
        enemyMovement = GetComponent<EnemyMovement>();
        bossManager = GetComponent<BossManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    float SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        enemyAnimatorManager.PlayTargetAnimation("Hurt", true, false);
        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);

        //if (currentHealth <= 0)
        //{
        //    enemyAnimatorManager.PlayTargetAnimation("Dying", true);
        //}

        //if ((currentHealth <= 0 && bossManager.firstDeath == true) || (currentHealth <= 0 && gameObject.tag == "Enemy"))
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            playerStats.AddChromaOrbs(ChromaOrbsToGive);
            navMeshAgent.enabled = false;
            enemyAnimatorManager.PlayTargetAnimation("Dying", true);
            enemyMovement.enabled = false;
        }
        
        if ((currentHealth <= 0 && bossManager?.firstDeath == true))
        {
            WorldColorManager.Instance.OnChangeWorldProfile();
        }
    }
}
