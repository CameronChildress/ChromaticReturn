using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    PlayerManager playerManager;
    EnemyManager enemyManager;
    public Slider characterSlider;
    public Slider enemySlider;

    private void Awake()
    {
        if (characterSlider != null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        if (enemySlider != null)
        {
            enemyManager = FindObjectOfType<EnemyManager>();
        }
    }

    void Start()
    {
        characterSlider.maxValue = playerManager.health;
        enemySlider.maxValue = enemyManager.health;
    }

    void Update()
    {
        characterSlider.value = playerManager.health;
        enemySlider.value = enemyManager.health;
    }
}
