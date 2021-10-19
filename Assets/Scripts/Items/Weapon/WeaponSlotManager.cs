using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHolderSlot LeftHandSlot;
    WeaponHolderSlot RightHandSlot;

    Weapon leftWeaponCollider;
    Weapon rightWeaponCollider;

    Animator animator;

    QuickSlotsUI quickSlotsUI;

    private void Awake()
    {
        //animator.GetComponent<Animator>();
        quickSlotsUI = FindObjectOfType<QuickSlotsUI>();

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
            LoadLeftWeaponCollider();
            quickSlotsUI.UpdateWeaponUIQuickSlots(true, weaponItem);
        }
        else
        {
            RightHandSlot.LoadWeaponObject(weaponItem);
            LoadRightWeaponCollider();
            quickSlotsUI.UpdateWeaponUIQuickSlots(false, weaponItem);
        }
    }

    void LoadLeftWeaponCollider()
    {
        leftWeaponCollider = LeftHandSlot.currentWeaponObject.GetComponentInChildren<Weapon>();
    }

    void LoadRightWeaponCollider()
    {
        rightWeaponCollider = RightHandSlot.currentWeaponObject.GetComponentInChildren<Weapon>();
    }

    public void OpenRightWeaponCollider()
    {
        rightWeaponCollider?.ToggleCollider();

        if (rightWeaponCollider == null) return;
    }

    public void OpenLeftWeaponCollider()
    {
        leftWeaponCollider?.ToggleCollider();

        if (leftWeaponCollider == null) return;
    }

    public void CloseRightWeaponCollider()
    {
        if (rightWeaponCollider == null) return;

        rightWeaponCollider?.ToggleOffCollider();
    }

    public void CloseLeftWeaponCollider()
    {
        if (leftWeaponCollider == null) return;

        leftWeaponCollider?.ToggleOffCollider();
    }
}
