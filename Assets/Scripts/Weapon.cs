using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Collider collider;

    public float damage = 1f;

    private void Awake()
    {
        collider = GetComponent<MeshCollider>();
    }

    public void ToggleCollider()
    {
        collider.enabled = !enabled;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyManager>().health -= damage;
        }

        Debug.Log(other.gameObject.name);
    }
}
