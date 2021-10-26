using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;

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
        playerStats.staminaLevel = playerData.staminaLevel;
        playerStats.ChromaOrbsObtained = playerData.chromaOrbsObtained;
        playerStats.lostOrbs = playerData.lostChromaOrbs;

        /*
         *     public float[] position;
    public int healthLevel;
    public int staminaLevel;
    public int chromaOrbsObtained;
    public int lostChromaOrbs;
         * */
    }
}
