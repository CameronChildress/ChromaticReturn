using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    EnemyMovement enemyMovement;
    EnemyStats enemyStats;
    EnemyAnimatorManager enemyAnimatorManager;
    public Rigidbody rigidbody;

    public NavMeshAgent navMeshAgent;
    public CharacterStats currentTarget;
    public State currentState;
    public VolumeProfile volumeProfile;

    public bool isPerformingAction;

    public float rotationSpeed = 50;
    public float maximumAttackRange = 1.5f;

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
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        navMeshAgent.enabled = false;
    }

    private void Start()
    {
        rigidbody.isKinematic = false;

        currentState = GetComponentInChildren<IdleState>();
    }

    void Update()
    {
        HandleRecoveryTime();

        if (enemyStats.currentHealth <= 0 && gameObject.tag == "Enemy")
        {
            //enemyAnimatorManager.PlayTargetAnimation("death", true);
            Destroy(gameObject, 2f);
        }
    }

    void FixedUpdate()
    {
        HandleStateMachine();
    }

    void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }



        //if (enemyMovement.currentTarget != null)
        //{
        //    enemyMovement.distanceFromTarget = Vector3.Distance(enemyMovement.currentTarget.transform.position, transform.position);
        //}

        //if (enemyMovement.currentTarget == null)
        //{
        //    enemyMovement.HandleDetection();
        //}
        //else if (enemyMovement.distanceFromTarget > enemyMovement.stoppingDistance)
        //{
        //    enemyMovement.HandleMoveToTarget();
        //}
        //else if (enemyMovement.distanceFromTarget <= enemyMovement.stoppingDistance)
        //{
        //    AttackTarget();
        //}
    }

    void SwitchToNextState(State state)
    {
        currentState = state;
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
