using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPoint : Interactable
{
    public UIManager uiManager;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

        //pick up weapon and add it to player inventory
        SetReturnPoint(playerManager);
    }

    void SetReturnPoint(PlayerManager playerManager)
    {
        PlayerInventory playerInventory;
        PlayerMovement playerMovement;
        AnimatorManager animatorManager;
        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerMovement = playerManager.GetComponent<PlayerMovement>();
        animatorManager = playerManager.GetComponentInChildren<AnimatorManager>();

        playerMovement.rigidbody.velocity = Vector3.zero;

        uiManager.RestMenuScreen.SetActive(true);

        GameManager.Instance.Save();
    }
}