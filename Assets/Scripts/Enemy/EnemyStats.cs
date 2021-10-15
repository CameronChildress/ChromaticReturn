using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    EnemyMovement enemyMovement;

    //public HealthBar healthBar;

    //Animator animator;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;

        //healthBar.SetMaxHealth(maxHealth);
    }

    float SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //animator.Play("Hurt");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //animator.Play("Dying");

            enemyMovement.enabled = false;
        }

        ///healthBar.SetCurrentHealth(currentHealth);
    }
}
