using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickUp : Interactable
{
    public WeaponItem weapon;

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

        playerInventory.weaponsInventory.Add(weapon);
        playerManager.itemInteractableGameObject.SetActive(true);
        playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
        playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;

        Destroy(gameObject);
    }
}
