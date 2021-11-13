using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : MonoBehaviour
{
    Weapon rightWeaponCollider;
    Weapon leftWeaponCollider;

    BossManager bossManager;

    private void Awake()
    {
        rightWeaponCollider = GetComponentInChildren<Weapon>();
        leftWeaponCollider = GetComponentInChildren<Weapon>();

        bossManager = FindObjectOfType<BossManager>();
    }

    public void OpenRightWeaponCollider()
    {
        rightWeaponCollider.ToggleCollider();

        if (rightWeaponCollider == null) return;
    }

    public void OpenLeftWeaponCollider()
    {
        leftWeaponCollider.ToggleCollider();

        if (leftWeaponCollider == null) return;
    }

    public void CloseRightWeaponCollider()
    {
        if (rightWeaponCollider == null) return;

        rightWeaponCollider.ToggleOffCollider();
    }

    public void CloseLeftWeaponCollider()
    {
        if (leftWeaponCollider == null) return;

        leftWeaponCollider.ToggleOffCollider();
    }

    public void SpawnProjectile()
    {
        bossManager.SpawnProjectile();
    }

    public void ThrowProjectile()
    {
        bossManager.ThrowProjectile();
    }
}
