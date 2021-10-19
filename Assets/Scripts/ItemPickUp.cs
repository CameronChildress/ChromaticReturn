using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : Interactable
{
    public Item item;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

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

        playerInventory.itemsInventory.Add(item);
        playerManager.itemInteractableGameObject.SetActive(true);
        playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = item.itemName;
        playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = item.itemIcon.texture;

        Destroy(gameObject);
    }
}
