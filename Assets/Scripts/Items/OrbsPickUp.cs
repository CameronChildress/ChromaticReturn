using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbsPickUp : Interactable
{
    //public Item item;
    int orbAmount;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

        //pick up weapon and add it to player inventory
        PickUpItem(playerManager);
    }

    void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventory playerInventory;
        PlayerMovement playerMovement;
        AnimatorManager animatorManager;
        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerMovement = playerManager.GetComponent<PlayerMovement>();
        animatorManager = playerManager.GetComponentInChildren<AnimatorManager>();

        playerMovement.rigidbody.velocity = Vector3.zero;
        //animatorManager.PlayTargetAnimation("PickUpItem", true);

        playerManager.itemInteractableGameObject.SetActive(true);
        playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().enabled = false;
        GameObject.Find("ItemUIBackground").SetActive(false);
        playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = playerManager.GetComponent<PlayerStats>().lostOrbs.ToString();
        playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = null;

        orbAmount = playerManager.GetComponent<PlayerStats>().lostOrbs;
        playerManager.GetComponent<PlayerStats>().AddChromaOrbs(orbAmount);

        Destroy(gameObject);
    }
}
