using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacterIndex = 0;

    public void NextCharacter()
    {
        characters[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex = (selectedCharacterIndex + 1) % characters.Length;
        characters[selectedCharacterIndex].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex += characters.Length;
        }
        characters[selectedCharacterIndex].SetActive(true);
    }

    public void StartGameWithCharacter()
    {
        PlayerPrefs.SetInt("selectedCharacterIndex", selectedCharacterIndex);
        SceneManager.LoadScene("WorldTest", LoadSceneMode.Single);
    }
}
