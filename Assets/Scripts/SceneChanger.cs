using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SwitchScenes()
    {
        SceneManager.LoadScene(sceneName);
    }
}
