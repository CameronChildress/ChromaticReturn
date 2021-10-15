using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    EnemyMovement enemyMovement;
    PlayerStats playerStats;

    public HealthBar healthBar;

    int ChromaOrbsToGive = 100;

    //Animator animator;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        playerStats = FindObjectOfType<PlayerStats>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;

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
            currentHealth = 0;
            playerStats.AddChromaOrbs(ChromaOrbsToGive);

            //animator.Play("Dying");

            enemyMovement.enabled = false;
        }
    }
}
