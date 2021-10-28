using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public Text HealthText;
    public Text EnduranceText;
    public Text StrengthText;
    public Text orbsNeededText;

    public PlayerStats playerStats;
    public GameObject restScreen;

    void Start()
    {
        restScreen.SetActive(false);
    }

    private void Awake()
    {
        HealthText.text = playerStats.Vitality.ToString();
        EnduranceText.text = playerStats.Endurance.ToString();
        StrengthText.text = playerStats.Strength.ToString();
    }

    void Update()
    {
        orbsNeededText.text = playerStats.orbsNeededToLevel.ToString();
    }

    public void LevelUpHealth()
    {
        playerStats.LevelUpHEALTH();
        HealthText.text = playerStats.Vitality.ToString();
    }

    public void LevelUpEndurance()
    {
        playerStats.LevelUpENDURANCE();
        EnduranceText.text = playerStats.Endurance.ToString();
    }

    public void LevelUpStrength()
    {
        playerStats.LevelUpSTRENGTH();
        StrengthText.text = playerStats.Strength.ToString();
    }
}
