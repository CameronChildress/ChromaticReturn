using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorChanger : MonoBehaviour
{
    void Update()
    {
        if (this.GetComponent<EnemyStats>().currentHealth <= 0)
        {
            WorldColorManager.Instance.OnChangeWorldProfile();
        }
    }
}
