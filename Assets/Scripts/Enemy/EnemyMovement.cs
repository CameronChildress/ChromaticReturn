using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager enemyManager;
    public PlayerManager currentTarget;

    public LayerMask detectionLayer;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerManager playerManager = colliders[i].transform.GetComponent<PlayerManager>();

            if (playerManager != null)
            {
                Vector3 targetDirection = playerManager.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    currentTarget = playerManager;
                }
            }
        }
    }
}
