using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHolderSlot LeftHandSlot;
    WeaponHolderSlot RightHandSlot;

    private void Awake()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                LeftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                RightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponInSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            LeftHandSlot.LoadWeaponObject(weaponItem);
        }
        else
        {
            RightHandSlot.LoadWeaponObject(weaponItem);
        }
    }
}
