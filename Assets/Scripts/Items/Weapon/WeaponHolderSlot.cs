using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    public Transform parentOverride;
    public bool isLeftHandSlot;
    public bool isRightHandSlot;

    public GameObject currentWeaponObject;

    public void LoadWeaponObject(WeaponItem weaponItem)
    {
        UnloadWeaponAndDestroy();

        if (weaponItem == null)
        {
            UnloadWeapon();
            return;
        }

        GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
        if (model != null)
        {
            if (parentOverride != null)
            {
                model.transform.parent = parentOverride;
            }
            else
            {
                model.transform.parent = transform;
            }

            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        currentWeaponObject = model;
    }

    public void UnloadWeapon()
    {
        if (currentWeaponObject != null)
        {
            currentWeaponObject.SetActive(false);
        }
    }

    public void UnloadWeaponAndDestroy()
    {
        if (currentWeaponObject != null)
        {
            Destroy(currentWeaponObject);
        }
    }
}
