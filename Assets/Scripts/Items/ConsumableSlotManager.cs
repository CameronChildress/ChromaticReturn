using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSlotManager : MonoBehaviour
{
    ConsumableHolderSlot RightHandSlot;

    Animator animator;

    QuickSlotsUI quickSlotsUI;

    private void Awake()
    {
        //animator.GetComponent<Animator>();
        quickSlotsUI = FindObjectOfType<QuickSlotsUI>();

        ConsumableHolderSlot[] consumableHolderSlots = GetComponentsInChildren<ConsumableHolderSlot>();
        foreach (ConsumableHolderSlot consumableSlot in consumableHolderSlots)
        {
            if (consumableSlot.isRightHandSlot)
            {
                RightHandSlot = consumableSlot;
            }
        }
    }

    public void LoadConsumableInSlot(ConsumableItem consumableItem)
    {
        RightHandSlot.LoadConsumableObject(consumableItem);
        quickSlotsUI.UpdateQuickSlot(consumableItem);
        
    }
}
