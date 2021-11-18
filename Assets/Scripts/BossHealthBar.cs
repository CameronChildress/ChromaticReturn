using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Text bossName;
    Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        bossName = GetComponentInChildren<Text>();

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SetHealthBarInActive();
    }

    public void SetBossName(string name)
    {
        bossName.text = name;
    }

    public void SetUIHealthBarActive()
    {
        slider.gameObject.SetActive(true);
    }

    public void SetHealthBarInActive()
    {
        slider.gameObject.SetActive(false);
    }

    public void SetBossMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetBossCurrentHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }
}
