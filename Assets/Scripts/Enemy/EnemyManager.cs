using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering.PostProcessing;

public class EnemyManager : MonoBehaviour
{
    EnemyMovement enemyMovement;
    public VolumeProfile volumeProfile;

    public float health = 5f;

    public bool isPerformingAction;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;
    //The higher and lower angle, the greater or lower FOV
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        HandleCurrentAction();

        if (health <= 0)
        {
            Destroy(gameObject);
            
            if (volumeProfile != null)
            {
                Volume globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
                globalVolume.profile = volumeProfile;
            }
        }
    }

    void FixedUpdate()
    {
        HandleCurrentAction();
    }

    void HandleCurrentAction()
    {
        if (enemyMovement.currentTarget == null)
        {
            enemyMovement.HandleDetection();
        }
        else
        {
            enemyMovement.HandleMoveToTarget();
        }
    }

    public void TakeDamage()
    {
        if (health <= 0) Destroy(gameObject);

        health--;
    }
}
