using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator animator;

    private string sceneName;
    private int sceneIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void FadeToLevel(string name)
    {
        sceneName = name;
        animator.SetTrigger("FadeOut");
    }

    public void FadeToLevel(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneName);
    }
}
