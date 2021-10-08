using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Collider collider;
    GameObject trailObject;
    TrailRenderer trail;

    public float damage = .5f;

    private void Awake()
    {
        collider = GetComponent<MeshCollider>();
        if (trailObject != null)
        {
            trail = trailObject.GetComponent<TrailRenderer>();
        }
    }

    public void ToggleCollider()
    {
        if (collider.enabled)
        {
            collider.enabled = false;
            if (trail != null)
            {
                trail.enabled = false;
            }
        }
        else if (!collider.enabled)
        {
            collider.enabled = true;
            if (trail != null)
            {
                trail.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyManager>().health -= damage;
        }

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().health -= damage;
        }

        Debug.Log(other.gameObject.name);
    }
}
