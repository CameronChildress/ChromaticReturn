using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Windows")]
    public GameObject selectWindow;
    public GameObject hudWindow;
    public GameObject weaponInventoryWindow;

    public PlayerInventory playerInventory;

    [Header("Weapon Inventory")]
    public Transform weaponInventorySlotParent;
    public GameObject weaponInventorySlotPrefab;
    WeaponInventorySlot[] weaponInventorySlots;

    void Start()
    {
        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
    }

    public void UpdateUI()
    {
        //Weapon Inventory Slots
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            if (i < playerInventory.weaponsInventory.Count)
            {
                if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                    weaponInventorySlots = weaponInventorySlotPrefab.GetComponentsInChildren<WeaponInventorySlot>();
                }

                weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }
        //END
    }

    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);
        hudWindow.SetActive(false);
    }

    public void CloseSelectWindow()
    {
        selectWindow.SetActive(false);
        hudWindow.SetActive(true);
    }

    public void CloseAllInventoryWindows()
    {
        weaponInventoryWindow.SetActive(false);
    }
}
