using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Collider damageCollider;
    GameObject trailObject;
    TrailRenderer trail;

    public int damage = 25;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject?.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
        if (trailObject != null)
        {
            trail = trailObject.GetComponent<TrailRenderer>();
        }
    }

    public void ToggleCollider()
    {
        damageCollider.enabled = true;

        //if (damageCollider.enabled)
        //{
        //    damageCollider.enabled = false;
        //    if (trail != null)
        //    {
        //        trail.enabled = false;
        //    }
        //}
        //else if (!damageCollider.enabled)
        //{
        //    damageCollider.enabled = true;
        //    if (trail != null)
        //    {
        //        trail.enabled = true;
        //    }
        //}
    }
    
    public void ToggleOffCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage);
            }

            if (other.gameObject.tag == this.tag)
            {
                enemyStats.TakeDamage(0);
                return;
            }
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }
}
