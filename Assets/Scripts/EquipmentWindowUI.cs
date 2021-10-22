using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindowUI : MonoBehaviour
{
    public bool rightHandSlot1Selected;
    public bool rightHandSlot2Selected;
    public bool leftHandSlot1Selected;
    public bool leftHandSlot2Selected;

    HandEquipmentSlotUI[] handEquipmentSlotUIs;

    private void Start()
    {
        handEquipmentSlotUIs = GetComponentsInChildren<HandEquipmentSlotUI>();
    }

    public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
    {
        for (int i = 0; i < handEquipmentSlotUIs.Length; i++)
        {
            if (handEquipmentSlotUIs[i].rightHandSlot1)
            {
                handEquipmentSlotUIs[i].AddItem(playerInventory.weaponsInRightHandSlots[0]);
            }
            else if (handEquipmentSlotUIs[i].rightHandSlot2)
            {
                handEquipmentSlotUIs[i].AddItem(playerInventory.weaponsInRightHandSlots[1]);
            }
            else if (handEquipmentSlotUIs[i].leftHandSlot1)
            {
                handEquipmentSlotUIs[i].AddItem(playerInventory.weaponsInLeftHandSlots[0]);
            }
            else
            {
                handEquipmentSlotUIs[i].AddItem(playerInventory.weaponsInLeftHandSlots[1]);
            }
        }
    }

    public void SelectRightHandSlot1()
    {
        rightHandSlot1Selected = true;
    }

    public void SelectRightHandSlot2()
    {
        rightHandSlot2Selected = true;
    }

    public void SelectLeftHandSlot1()
    {
        leftHandSlot1Selected = true;
    }

    public void SelectLeftHandSlot2()
    {
        leftHandSlot2Selected = true;
    }
}
