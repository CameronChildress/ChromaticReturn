using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;
    ConsumableSlotManager consumableSlotManager;

    public ConsumableItem currentConsumableItem;
    public ConsumableItem emptyConsumable;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public WeaponItem unarmedWeapon;

    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];

    public ConsumableItem[] consumableItemsInBottomQuickSlot = new ConsumableItem[1];

    public int currentRightWeaponIndex = -1;
    public int currentLeftWeaponIndex = -1;

    public List<WeaponItem> weaponsInventory;

    public int currentConsumableIndex = -1;
    public List<ConsumableItem> consumableItemsInventory;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        consumableSlotManager = GetComponentInChildren<ConsumableSlotManager>();
    }

    private void Start()
    {
        rightWeapon = weaponsInRightHandSlots[0];
        leftWeapon = weaponsInLeftHandSlots[0];
        weaponSlotManager.LoadWeaponInSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponInSlot(leftWeapon, true);
    }

    //public void ChangeConsumableItem()
    //{
    //    currentConsumableIndex++;

    //    if (currentConsumableIndex == 0 && consumableItemsInBottomQuickSlot[0] != null)
    //    {
    //        consumableItem = consumableItemsInBottomQuickSlot[currentConsumableIndex];
    //        consumableSlotManager.LoadConsumableInSlot(consumableItemsInBottomQuickSlot[currentConsumableIndex]);
    //    }
    //    else if (currentConsumableIndex == 0 && consumableItemsInBottomQuickSlot[0] == null)
    //    {
    //        currentConsumableIndex++;
    //    }
    //    else if (currentConsumableIndex == 1 && consumableItemsInBottomQuickSlot[1] != null)
    //    {
    //        consumableItem = consumableItemsInBottomQuickSlot[currentConsumableIndex];
    //        consumableSlotManager.LoadConsumableInSlot(consumableItemsInBottomQuickSlot[currentConsumableIndex]);
    //    }
    //    else
    //    {
    //        currentRightWeaponIndex++;
    //    }

    //    if (currentConsumableIndex > consumableItemsInBottomQuickSlot.Length - 1)
    //    {
    //        currentConsumableIndex = -1;
    //        consumableItem = emptyConsumable;
    //        consumableSlotManager.LoadConsumableInSlot(emptyConsumable);
    //    }
    //}

    public void ChangeRightWeapon()
    {
        currentRightWeaponIndex++;

        if (currentRightWeaponIndex == 0 && weaponsInRightHandSlots[0] != null)
        {
            rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
            weaponSlotManager.LoadWeaponInSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
        }
        else if (currentRightWeaponIndex == 0 && weaponsInRightHandSlots[0] == null)
        {
            currentRightWeaponIndex++;
        }
        else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlots[1] != null)
        {
            rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
            weaponSlotManager.LoadWeaponInSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
        }
        else
        {
            currentRightWeaponIndex++;
        }

        if (currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
        {
            currentRightWeaponIndex = -1;
            rightWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponInSlot(unarmedWeapon, false);
        }
    }

    public void ChangeLeftWeapon()
    {
        currentLeftWeaponIndex++;

        if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlots[0] != null)
        {
            leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponInSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
        }
        else if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlots[0] == null)
        {
            currentLeftWeaponIndex++;
        }
        else if (currentLeftWeaponIndex == 1 && weaponsInLeftHandSlots[1] != null)
        {
            leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponInSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
        }
        else
        {
            currentLeftWeaponIndex++;
        }

        if (currentLeftWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        {
            currentLeftWeaponIndex = -1;
            leftWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponInSlot(unarmedWeapon, true);
        }
    }
}
