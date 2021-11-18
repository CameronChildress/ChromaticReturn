using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    EnemyMovement enemyMovement;
    PlayerStats playerStats;
    BossManager bossManager;
    EnemyAnimatorManager enemyAnimatorManager;

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
        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);

        //animator.Play("Hurt");

        if (currentHealth <= 0)
        {
            enemyAnimatorManager.PlayTargetAnimation("death", true);
        }

        if ((currentHealth <= 0 && bossManager.firstDeath == true) || (currentHealth <= 0 && gameObject.tag == "Enemy"))
        {
            currentHealth = 0;
            playerStats.AddChromaOrbs(ChromaOrbsToGive);
            enemyAnimatorManager.PlayTargetAnimation("death", true);
            WorldColorManager.Instance.OnChangeWorldProfile();

            enemyMovement.enabled = false;
        }

        if ((currentHealth <= 0 && bossManager.firstDeath == true))
        {
            WorldColorManager.Instance.OnChangeWorldProfile();
        }
    }
}
