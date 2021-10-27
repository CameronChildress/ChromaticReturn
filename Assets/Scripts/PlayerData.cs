using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int healthLevel;
    public float maxHealth;
    public float currentHealth;
    public int staminaLevel;
    public int chromaOrbsObtained;
    public int lostChromaOrbs;
    public float respawnTimer;
    public float fadeInTimer;
    public float fadeOutTimer;

    public PlayerData(PlayerStats playerStats)
    {
        position = new float[3];
        position[0] = playerStats.transform.position.x;
        position[1] = playerStats.transform.position.y;
        position[2] = playerStats.transform.position.z;

        healthLevel = playerStats.healthLevel;
        maxHealth = playerStats.maxHealth;
        currentHealth = playerStats.currentHealth;
        staminaLevel = playerStats.staminaLevel;
        chromaOrbsObtained = playerStats.ChromaOrbsObtained;
        lostChromaOrbs = playerStats.lostOrbs;
        respawnTimer = playerStats.respawnTimer;
    }
}
