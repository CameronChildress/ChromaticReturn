using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSystem : MonoBehaviour
{
    public GameObject targetObject;

    public float offset = 0;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + offset, targetObject.transform.position.z);
    }
}
