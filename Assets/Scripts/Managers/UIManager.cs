using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject selectWindow;
    public GameObject hudWindow;

    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);
        hudWindow.SetActive(false);
    }

    public void CloseSelectWindow()
    {
        selectWindow.SetActive(false);
        hudWindow.SetActive(true);
    }
}
