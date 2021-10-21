using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableHolderSlot : MonoBehaviour
{
    public Transform parentOverride;
    public bool isRightHandSlot;

    public GameObject currentConsumableObject;

    public void LoadConsumableObject(ConsumableItem consumableItem)
    {
        UnloadConsumableAndDestroy();

        if (consumableItem == null)
        {
            UnloadConsumable();
            return;
        }

        GameObject model = Instantiate(consumableItem.itemPrefab) as GameObject;
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

        currentConsumableObject = model;
    }

    public void UnloadConsumable()
    {
        if (currentConsumableObject != null)
        {
            currentConsumableObject.SetActive(false);
        }
    }

    public void UnloadConsumableAndDestroy()
    {
        if (currentConsumableObject != null)
        {
            Destroy(currentConsumableObject);
        }
    }
}
