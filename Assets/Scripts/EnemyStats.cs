using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

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

    int SetMaxHealthFromHealthLevel()
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
