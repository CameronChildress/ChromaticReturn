using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int healthLevel;
    public int staminaLevel;
    public int chromaOrbsObtained;
    public int lostChromaOrbs;

    public PlayerData(PlayerStats playerStats)
    {
        position = new float[3];
        position[0] = playerStats.transform.position.x;
        position[1] = playerStats.transform.position.y;
        position[2] = playerStats.transform.position.z;

        healthLevel = playerStats.healthLevel;
        staminaLevel = playerStats.staminaLevel;
        chromaOrbsObtained = playerStats.ChromaOrbsObtained;
        lostChromaOrbs = playerStats.lostOrbs;
    }
}
