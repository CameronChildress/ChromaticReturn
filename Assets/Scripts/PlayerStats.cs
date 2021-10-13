using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public int staminaLevel = 8;
    public int maxStamina;
    public int currentStamina;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    int SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = (staminaLevel * 8) + 20;
        return maxStamina;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetCurrentHealth(currentHealth);
    }
}
