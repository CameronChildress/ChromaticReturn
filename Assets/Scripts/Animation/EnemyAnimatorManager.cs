using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AllAnimatorManager
{
    EnemyMovement enemyMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyMovement.rigidbody.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyMovement.rigidbody.velocity = velocity;
    }
}
