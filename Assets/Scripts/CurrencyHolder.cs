using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyHolder : MonoBehaviour
{
    public Text txtAmount;

    public void SetAmount(int amount)
    {
        txtAmount.text = amount.ToString();
    }
}
