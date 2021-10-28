using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Windows")]
    public GameObject selectWindow;
    public GameObject hudWindow;
    public GameObject weaponInventoryWindow;
    public GameObject equipmentWindow;
    public GameObject gameOptionsWindow;
    public GameObject deathScreen;
    public GameObject levelUpScreen;
    public GameObject RestMenuScreen;

    public PlayerInventory playerInventory;
    public EquipmentWindowUI equipmentWindowUI;

    [Header("Equipment Window Slot Selected")]
    public bool rightHandSlot1Selected;
    public bool rightHandSlot2Selected;
    public bool leftHandSlot1Selected;
    public bool leftHandSlot2Selected;

    [Header("Weapon Inventory")]
    public Transform weaponInventorySlotParent;
    public GameObject weaponInventorySlotPrefab;
    WeaponInventorySlot[] weaponInventorySlots;

    void Awake()
    {
    
    }

    void Start()
    {
        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
        equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
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
        ResetAllSelectedSlots();
        equipmentWindow.SetActive(false);
        weaponInventoryWindow.SetActive(false);
        gameOptionsWindow.SetActive(false);
        levelUpScreen.SetActive(false);
        RestMenuScreen.SetActive(false);
    }

    public void ResetAllSelectedSlots()
    {
        rightHandSlot1Selected = false;
        rightHandSlot2Selected = false;
        leftHandSlot1Selected = false;
        leftHandSlot2Selected = false;
    }
}
