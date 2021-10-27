using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;

    public static GameManager Instance { get { return instance; } }
    static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Save()
    {
        SaveSystem.SavePlayer(playerStats);
        //Save other objects
    }

    public void Load()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();

        Vector3 playerPosition;
        playerPosition.x = playerData.position[0];
        playerPosition.y = playerData.position[1];
        playerPosition.z = playerData.position[2];

        playerStats.transform.position = playerPosition;

        playerStats.healthLevel = playerData.healthLevel;
        playerStats.maxHealth = playerData.maxHealth;
        playerStats.currentHealth = playerData.currentHealth;
        playerStats.staminaLevel = playerData.staminaLevel;
        playerStats.ChromaOrbsObtained = playerData.chromaOrbsObtained;
        playerStats.lostOrbs = playerData.lostChromaOrbs;
        //playerStats.respawnTimer = playerData.respawnTimer;
        //playerStats.fadeInTimer = playerData.fadeInTimer;
        //playerStats.fadeOutTimer = playerData.fadeOutTimer;
    }
}
