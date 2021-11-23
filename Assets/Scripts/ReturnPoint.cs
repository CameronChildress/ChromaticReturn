using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPoint : Interactable
{
    public UIManager uiManager;
    public InputManager inputManager;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        inputManager.restingFlag = true;

        Debug.Log("Resting");

        //if (uiManager.RestMenuScreen.activeInHierarchy || uiManager.levelUpScreen.activeInHierarchy)
        //{
        //    playerMovement.rigidbody.velocity = Vector3.zero;
        //    inputManager.inventoryFlag = true;
        //}

        GameManager.Instance.Save();
    }
}
