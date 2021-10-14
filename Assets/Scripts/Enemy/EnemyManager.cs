using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering.PostProcessing;

public class EnemyManager : MonoBehaviour
{
    EnemyMovement enemyMovement;
    EnemyStats enemyStats;
    public VolumeProfile volumeProfile;

    public bool isPerformingAction;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;
    //The higher and lower angle, the greater or lower FOV
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyStats = GetComponent<EnemyStats>();
    }

    void Update()
    {
        HandleCurrentAction();

        if (enemyStats.currentHealth <= 0)
        {
            Destroy(gameObject);

            WorldColorManager.Instance.OnChangeWorldProfile();

            //if (volumeProfile != null)
            //{
                
            //}
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

    //public void TakeDamage()
    //{
    //    if (health <= 0) Destroy(gameObject);

    //    health--;
    //}
}
