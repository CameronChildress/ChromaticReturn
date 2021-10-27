using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    PlayerMovement playerMovement;

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public UIManager uiManager;
    public CurrencyHolder currencyHolder;

    public int staminaLevel = 8;
    public float maxStamina;
    public float currentStamina;
    public float staminaTimer = 3f;

    public int ChromaOrbsObtained = 0;
    public int lostOrbs = 0;

    public int Vitality;
    public int Endurance;
    public int Strength;

    public float respawnTimer = 3f;

    public float fadeTimer = 5f;

    public static bool firstLoaded = true;

    void Awake()
    {
        GameManager.Instance.Load();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if (firstLoaded)
        {
            fadeTimer = 0;
            firstLoaded = false;
        }

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

        if (currentHealth <= 0)
        {
            respawnTimer -= Time.deltaTime;
            fadeTimer += Time.deltaTime;
            fadeTimer = Mathf.Clamp(fadeTimer, 0, 5);

            Color colorIn = uiManager.deathScreen.GetComponent<Image>().color;
            colorIn.a = Mathf.Lerp(0, 1, fadeTimer);

            uiManager.deathScreen.GetComponent<Image>().color = colorIn;
            uiManager.deathScreen.SetActive(true);

            if (fadeTimer >= 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            fadeTimer -= Time.deltaTime;

            fadeTimer = Mathf.Clamp(fadeTimer, 0, 5);
            if (fadeTimer > 0)
            {
                Color colorOut = uiManager.deathScreen.GetComponent<Image>().color;
                colorOut.a = Mathf.Lerp(0, 1, fadeTimer);

                uiManager.deathScreen.GetComponent<Image>().color = colorOut;
            }
            else if (fadeTimer <= 0)
            {
                uiManager.deathScreen.SetActive(false);
            }

            if (respawnTimer <= 0)
            {
                if (fadeTimer <= 0)
                {
                    currentHealth = maxHealth;
                    healthBar.SetMaxHealth(maxHealth);
                }
            }
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
            lostOrbs = ChromaOrbsObtained;
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
