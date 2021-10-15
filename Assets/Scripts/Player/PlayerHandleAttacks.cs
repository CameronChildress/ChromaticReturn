using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandleAttacks : MonoBehaviour
{
    AnimatorManager animatorManager;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        animatorManager.PlayTargetAnimation(weapon.oneHand_LightAttack1, true);
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        animatorManager.PlayTargetAnimation(weapon.oneHand_HeavyAttack1, true);
    }
}
