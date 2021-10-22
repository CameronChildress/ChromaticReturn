using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipmentSlotUI : MonoBehaviour
{
    UIManager uiManager;
    public Image icon;
    WeaponItem weapon;

    public bool rightHandSlot1;
    public bool rightHandSlot2;
    public bool leftHandSlot1;
    public bool leftHandSlot2;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void AddItem(WeaponItem newWeapon)
    {
        weapon = newWeapon;
        icon.sprite = weapon.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        weapon = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void SelectSlot()
    {
        if (rightHandSlot1)
        {
            uiManager.rightHandSlot1Selected = true;
        }
        else if (rightHandSlot2)
        {
            uiManager.rightHandSlot2Selected = true;
        }
        else if (leftHandSlot1)
        {
            uiManager.leftHandSlot1Selected = true;
        }
        else
        {
            uiManager.leftHandSlot2Selected = true;
        }
    }
}
