using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    PlayerMovement playerMovement;

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public CurrencyHolder currencyHolder;

    public int staminaLevel = 8;
    public float maxStamina;
    public float currentStamina;

    public float staminaTimer = 3f;

    public int ChromaOrbsObtained = 0;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    void Update()
    {
        //healthBar.transform.localScale = new Vector3(healthLevel / 10, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if (staminaTimer > 0)
        {
            staminaTimer -= Time.deltaTime;
        }
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

        if (currentHealth <= 0)
        {
            ChromaOrbsObtained -= ChromaOrbsObtained;
            currencyHolder.SetAmount(ChromaOrbsObtained);
        }
    }

    public void UseStamina(float stamina)
    {
        if (playerMovement.isSprinting)
        {
            currentStamina -= (stamina * Time.deltaTime);
        }
        else
        {
            currentStamina -= stamina;
        }
        staminaBar.SetCurrentStamina(currentStamina);

        staminaTimer = 3f;
    }

    public void ReturnStamina(float stamina)
    {
        if (currentStamina < maxStamina && staminaTimer < 0)
        {
            currentStamina += (stamina * Time.deltaTime);
            staminaBar.SetCurrentStamina(currentStamina);
        }
    }

    public void AddChromaOrbs(int orbs)
    {
        ChromaOrbsObtained += orbs;
        currencyHolder.SetAmount(ChromaOrbsObtained);
    }
}
