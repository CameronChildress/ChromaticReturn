using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public WeaponItem unarmedWeapon;

    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];

    public int currentRightWeaponIndex = 0;
    public int currentLeftWeaponIndex = 0;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    private void Start()
    {
        rightWeapon = unarmedWeapon;
        leftWeapon = unarmedWeapon;
    }

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
            currentRightWeaponIndex = 0;
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
            currentLeftWeaponIndex = 0;
            leftWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponInSlot(unarmedWeapon, true);
        }
    }
}
