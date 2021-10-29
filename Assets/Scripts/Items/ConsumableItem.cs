using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable Item")]
public class ConsumableItem : Item
{
    [Header("Item Quantity")]
    public int maxItemAmount;
    public int currentItemAmount;

    [Header("Item Model")]
    public GameObject itemPrefab;

    [Header("Item Animations")]
    public string consumableAnimation;
    public bool isInteracting;

    public virtual void AttemptToConsumeItem(AnimatorManager animatorManager, WeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        if (currentItemAmount > 0)
        {
            animatorManager.PlayTargetAnimation(consumableAnimation, isInteracting, true);
        }
        else
        {
            animatorManager.PlayTargetAnimation("ShakeNo", true);
        }
    }
}
