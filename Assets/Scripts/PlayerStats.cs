using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public float maxHealth;
    public float currentHealth;

    public int staminaLevel = 8;
    public float maxStamina;
    public float currentStamina;

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

    private void Update()
    {
        //healthBar.transform.localScale = new Vector3(healthLevel / 10, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    float SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    float SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = (staminaLevel * 8) + 20;
        return maxStamina;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);
    }

    public void UseStamina(float stamina)
    {
        currentStamina -= (stamina * Time.deltaTime);
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void ReturnStamina(float stamina)
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += (stamina * Time.deltaTime);
            staminaBar.SetCurrentStamina(currentStamina);
        }
    }
}
