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
    EnemyAnimatorManager enemyAnimatorManager;
    public VolumeProfile volumeProfile;

    public bool isPerformingAction;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;
    //The higher and lower angle, the greater or lower FOV
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    public float currentRecoveryTime = 0;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyStats = GetComponent<EnemyStats>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
    }

    void Update()
    {
        HandleRecoveryTime();

        if (enemyStats.currentHealth <= 0)
        {
            Destroy(gameObject);

            WorldColorManager.Instance.OnChangeWorldProfile();
        }
    }

    void FixedUpdate()
    {
        HandleCurrentAction();
    }

    void HandleCurrentAction()
    {
        if (enemyMovement.currentTarget != null)
        {
            enemyMovement.distanceFromTarget = Vector3.Distance(enemyMovement.currentTarget.transform.position, transform.position);
        }

        if (enemyMovement.currentTarget == null)
        {
            enemyMovement.HandleDetection();
        }
        else if (enemyMovement.distanceFromTarget > enemyMovement.stoppingDistance)
        {
            enemyMovement.HandleMoveToTarget();
        }
        else if (enemyMovement.distanceFromTarget <= enemyMovement.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void GetNewAttack()
    {
        Vector3 targetDirection = enemyMovement.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        enemyMovement.distanceFromTarget = Vector3.Distance(enemyMovement.currentTarget.transform.position, transform.position);

        int maxScore = 0;
        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyMovement.distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
                && enemyMovement.distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomVal = Random.Range(0, maxScore);
        int tempScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyMovement.distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
                && enemyMovement.distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (currentAttack != null) return;

                    tempScore += enemyAttackAction.attackScore;

                    if (tempScore > randomVal)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }

    void AttackTarget()
    {
        if (isPerformingAction) return;

        if (currentAttack == null)
        {
            GetNewAttack();
        }
        else
        {
            isPerformingAction = true;
            currentRecoveryTime = currentAttack.recoveryTime;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            currentAttack = null;
        }
    }

    void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }
}
