using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotsUI : MonoBehaviour
{
    public Image leftWeaponIcon;
    public Image rightWeaponIcon;

    public Image bottomQuickSlotIcon;

    public void UpdateQuickSlot(ConsumableItem consumableItem)
    {
        if (consumableItem != null)
        {
            bottomQuickSlotIcon.sprite = consumableItem.itemIcon;
            bottomQuickSlotIcon.enabled = true;
        }
        else
        {
            bottomQuickSlotIcon.sprite = null;
            bottomQuickSlotIcon.enabled = false;
        }
    }

    public void UpdateWeaponUIQuickSlots(bool isLeft, WeaponItem weaponItem)
    {
        if (isLeft == false)
        {
            if (weaponItem.itemIcon != null)
            {
                rightWeaponIcon.sprite = weaponItem.itemIcon;
                rightWeaponIcon.enabled = true;
            }
            else
            {
                rightWeaponIcon.sprite = null;
                rightWeaponIcon.enabled = false;
            }
        }
        else
        {
            if (weaponItem.itemIcon != null)
            {
                leftWeaponIcon.sprite = weaponItem.itemIcon;
                leftWeaponIcon.enabled = true;
            }
            else
            {
                leftWeaponIcon.sprite = null;
                leftWeaponIcon.enabled = false;
            }
        }
    }
}
