using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyAnimatorManager enemyAnimatorManager;

    public Weapon weapon;
    public bool isAttacking;

    public float speed = 5f;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
    }

    //void HandleAttacking()
    //{
    //    if (!isAttacking)
    //    {
    //        enemyAnimatorManager.PlayTargetAnimation("SwordOutwardSlash", true);
    //        isAttacking = true;
    //        weapon.ToggleCollider();
    //    }
    //}
}
