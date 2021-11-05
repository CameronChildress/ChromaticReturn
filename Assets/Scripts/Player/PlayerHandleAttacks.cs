using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandleAttacks : MonoBehaviour
{
    AnimatorManager animatorManager;
    PlayerMovement playerMovement;

    private void Awake()
    {
        
    }

    private void Start()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        playerMovement.rigidbody.velocity = Vector3.zero;
        animatorManager.PlayTargetAnimation(weapon.oneHand_LightAttack1, true);
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        playerMovement.rigidbody.velocity = Vector3.zero;
        animatorManager.PlayTargetAnimation(weapon.oneHand_HeavyAttack1, true);
    }
}
