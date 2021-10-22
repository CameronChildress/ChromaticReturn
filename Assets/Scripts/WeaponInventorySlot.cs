using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    PlayerInventory playerInventory;
    WeaponSlotManager weaponSlotManager;
    UIManager uiManager;

    public Image icon;
    WeaponItem weaponItem;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        uiManager = FindObjectOfType<UIManager>();
        weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
    }

    public void AddItem(WeaponItem newItem)
    {
        weaponItem = newItem;
        icon.sprite = newItem.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        weaponItem = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipItem()
    {
        //Remove current item
        //Add current item to inventory
        //equip new item
        //Remove that item from inventory

        if (uiManager.rightHandSlot1Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInRightHandSlots[0]);
            playerInventory.weaponsInRightHandSlots[0] = weaponItem;
            playerInventory.weaponsInventory.Remove(weaponItem);
        }
        else if (uiManager.rightHandSlot2Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInRightHandSlots[1]);
            playerInventory.weaponsInRightHandSlots[1] = weaponItem;
            playerInventory.weaponsInventory.Remove(weaponItem);
        }
        else if (uiManager.leftHandSlot1Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInLeftHandSlots[0]);
            playerInventory.weaponsInLeftHandSlots[0] = weaponItem;
            playerInventory.weaponsInventory.Remove(weaponItem);
        }
        else if (uiManager.leftHandSlot2Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInLeftHandSlots[1]);
            playerInventory.weaponsInLeftHandSlots[1] = weaponItem;
            playerInventory.weaponsInventory.Remove(weaponItem);
        }
        else
        {
            return;
        }

        playerInventory.rightWeapon = playerInventory.weaponsInRightHandSlots[playerInventory.currentRightWeaponIndex];
        playerInventory.leftWeapon = playerInventory.weaponsInLeftHandSlots[playerInventory.currentLeftWeaponIndex];

        weaponSlotManager.LoadWeaponInSlot(playerInventory.rightWeapon, false);
        weaponSlotManager.LoadWeaponInSlot(playerInventory.leftWeapon, true);

        uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        uiManager.ResetAllSelectedSlots();
    }
}
