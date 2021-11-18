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
        playerStats.orbsNeededToLevel = playerData.orbsNeededToLevel;
        //playerStats.respawnTimer = playerData.respawnTimer;

        WorldColorManager.Instance.currentProfile = WorldColorManager.Instance.allWorldVolumeProfiles[playerData.volumeProfileIndex];
    }

    public void FreshSave()
    {
        //Vector3 playerPosition;
        //playerPosition.x = -61;
        //playerPosition.y = 2;
        //playerPosition.z = -68;

        //playerStats.transform.position = playerPosition;

        //playerStats.healthLevel = 10;
        //playerStats.maxHealth = 100;
        //playerStats.currentHealth = 100;
        //playerStats.staminaLevel = 8;
        //playerStats.ChromaOrbsObtained = 0;
        //playerStats.lostOrbs = 0;
        //playerStats.orbsNeededToLevel = 100;
        ////playerStats.respawnTimer = playerData.respawnTimer;

        //WorldColorManager.Instance.currentProfile = WorldColorManager.Instance.allWorldVolumeProfiles[0];

        PlayerData playerData = SaveSystem.LoadPlayer();

        playerData.position[0] = -61;
        playerData.position[1] = 2;
        playerData.position[2] = -68;

        Vector3 playerPosition;
        playerPosition.x = playerData.position[0];
        playerPosition.y = playerData.position[1];
        playerPosition.z = playerData.position[2];

        playerStats.transform.position = playerPosition;

        playerStats.healthLevel = 10;
        playerStats.maxHealth = 100;
        playerStats.currentHealth = 100;
        playerStats.staminaLevel = 8;
        playerStats.ChromaOrbsObtained = 0;
        playerStats.lostOrbs = 0;
        playerStats.orbsNeededToLevel = 100;
        //playerStats.respawnTimer = playerData.respawnTimer;

        WorldColorManager.Instance.currentProfile = WorldColorManager.Instance.allWorldVolumeProfiles[0];
    }
}
