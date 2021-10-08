using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    private void Awake()
    {
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }

    private void Start()
    {
        weaponSlotManager.LoadWeaponInSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponInSlot(leftWeapon, true);
    }
}
