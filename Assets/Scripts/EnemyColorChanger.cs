using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorChanger : MonoBehaviour
{
    public bool colorHasChanged = false;

    private void Start()
    {
        
    }

    void Update()
    {
        if (this.GetComponent<EnemyStats>().currentHealth <= 0 && colorHasChanged == false)
        {
            WorldColorManager.Instance.OnChangeWorldProfile();
            colorHasChanged = true;
        }
        else if (this.GetComponent<EnemyStats>().currentHealth > 0 && colorHasChanged == true)
        {
            colorHasChanged = false;
        }
    }

    public void ChangeColor()
    {
        if (this.GetComponent<EnemyStats>().currentHealth <= 0)
        {
            WorldColorManager.Instance.OnChangeWorldProfile();
        }
    }
}
