using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public string bossName;
    BossHealthBar bossHealthBar;
    EnemyStats enemyStats;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<BossHealthBar>();
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Start()
    {
        bossHealthBar.SetBossName(bossName);
        bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
    }
}
