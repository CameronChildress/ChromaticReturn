using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Collider collider;
    public GameObject trailObject;
    TrailRenderer trail;

    public float damage = .5f;

    private void Awake()
    {
        collider = GetComponent<MeshCollider>();
        trail = trailObject.GetComponent<TrailRenderer>();
    }

    public void ToggleCollider()
    {
        if (collider.enabled)
        {
            collider.enabled = false;
            trail.enabled = false;
        }
        else if (!collider.enabled)
        {
            collider.enabled = true;
            trail.enabled = true;
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
