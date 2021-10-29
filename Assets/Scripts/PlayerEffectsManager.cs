using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : MonoBehaviour
{
    PlayerStats playerStats;
    WeaponSlotManager weaponSlotManager;
    public GameObject currentParticleFX;
    public int amountToHeal;

    public GameObject instantiatedFXModel;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }

    public void HealPlayerFromEffect()
    {
        playerStats.currentHealth += amountToHeal;
        GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform);
        Destroy(instantiatedFXModel.gameObject);
        weaponSlotManager.LoadBothWeaponsInSlots();
    }
}
