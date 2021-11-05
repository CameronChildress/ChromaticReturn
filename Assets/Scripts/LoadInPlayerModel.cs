using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInPlayerModel : MonoBehaviour
{
    public GameObject[] characterModels;
    public Transform spawnPoint;
    public Transform ParentOBJ;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacterIndex");
        GameObject model = characterModels[selectedCharacter];
        GameObject clone = Instantiate(model, spawnPoint.position, Quaternion.identity, ParentOBJ);
    }
}
