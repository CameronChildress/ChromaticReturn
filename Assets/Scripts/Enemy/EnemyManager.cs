using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyMovement enemyMovement;

    bool isPerformingAction;

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
    }

    void HandleCurrentAction()
    {
        if (enemyMovement.currentTarget == null)
        {
            enemyMovement.HandleDetection();
        }
    }
}
