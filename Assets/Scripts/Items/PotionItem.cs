using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable/HealthPotion")]
public class PotionItem : ConsumableItem
{
    public bool healthPotion;
    public int healthRecoveryAmount;
    public GameObject recoveryFX;

    public override void AttemptToConsumeItem(AnimatorManager animatorManager, WeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        base.AttemptToConsumeItem(animatorManager, weaponSlotManager, playerEffectsManager);
        GameObject potion = Instantiate(itemPrefab, weaponSlotManager.LeftHandSlot.transform);

        playerEffectsManager.currentParticleFX = recoveryFX;
        playerEffectsManager.amountToHeal = healthRecoveryAmount;
        playerEffectsManager.instantiatedFXModel = potion;
        weaponSlotManager.LeftHandSlot.UnloadWeapon();
    }

}
