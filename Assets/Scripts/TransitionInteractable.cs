using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionInteractable : Interactable
{
    public string sceneName;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

        SwitchScenes(playerManager);
    }

    void SwitchScenes(PlayerManager playerManager)
    {
        PlayerInventory playerInventory;
        PlayerMovement playerMovement;
        AnimatorManager animatorManager;
        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerMovement = playerManager.GetComponent<PlayerMovement>();
        animatorManager = playerManager.GetComponentInChildren<AnimatorManager>();

        playerMovement.rigidbody.velocity = Vector3.zero;

        //if (SceneManager.GetActiveScene().Equals("BossArenaTest"))
        if (sceneName.Equals("BossArenaTest"))
        {
            SceneManager.LoadScene(sceneName);
            playerManager.transform.position = new Vector3(0, 1, -12);
        }

        if (sceneName.Equals("WorldTest"))
        {
            //PlayerPrefs.SetInt("selectedCharacterIndex", selectedCharacterIndex);
            SceneManager.LoadScene(sceneName);
            playerManager.transform.position = new Vector3(9, 0, 63);
        }

        GameManager.Instance.Save();
    }
}
