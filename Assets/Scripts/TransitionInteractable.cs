using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionInteractable : Interactable
{
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

        SceneManager.LoadScene("BossArenaTest");
    }
}
