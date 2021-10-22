using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    public GameObject targetTransform; //The obj the camera will follow
    public Transform cameraPivot; //The obj the camera uses to pivot (Look up and down)
    public Transform cameraTransform; //The transform of the actual camera obj in the scene
    public LayerMask collisionLayers; //The layers we want our camera to collide with
    float defaultPosition;
    Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float cameraCollisionRadius = 0.2f;
    public float cameraCollisionOffSet = 0.2f; //How much the camera will jump off of objects its colliding with
    public float minimumCollisionOffSet = 0.2f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 720f;
    public float cameraPivotSpeed = 720f;

    public float lookAngle; //Camera look up and down
    public float pivotAngle; //Camera look left and right
    public float minimumPivotAngle = -35f;
    public float maximumPivotAngle = 35f;

    public float minimumLockAngle = -50;
    public float maximumLockAngle = 50;
    public float maximumLockOnDistance = 30f;

    List<CharacterManager> availableTargets = new List<CharacterManager>();
    public Transform currentLockOnTarget;
    public Transform nearestLockOnTarget;

    void Awake()
    {
        //targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.transform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    void RotateCamera()
    {
        if (!inputManager.lockOnFlag && currentLockOnTarget == null)
        {
            Vector3 rotation;
            Quaternion targetRotation;

            lookAngle += (inputManager.cameraInputX * cameraLookSpeed * Time.deltaTime);
            //cameraLookSpeed = Mathf.Clamp(cameraLookSpeed, -1, 1);
            pivotAngle -= (inputManager.cameraInputY * cameraPivotSpeed * Time.deltaTime);
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

            rotation = Vector3.zero;
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
        }
        else
        {
            float velocity = 0;

            Vector3 direction = currentLockOnTarget.position - transform.position;
            direction.Normalize();
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

            direction = currentLockOnTarget.position - cameraPivot.transform.position;
            direction.Normalize();

            targetRotation = Quaternion.LookRotation(direction);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivot.localEulerAngles = eulerAngle;
        }
    }

    void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffSet);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffSet)
        {
            targetPosition -= minimumCollisionOffSet;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }

    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(targetTransform.transform.position, 26);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager characterManager = colliders[i].GetComponent<CharacterManager>();

            if (characterManager != null)
            {
                Vector3 lockTargetDirection = characterManager.transform.position - targetTransform.transform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.transform.position, characterManager.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);

                if (characterManager.transform.root != targetTransform.transform.root && viewableAngle > minimumLockAngle && viewableAngle < maximumLockAngle 
                    && distanceFromTarget <= maximumLockOnDistance)
                {
                    availableTargets.Add(characterManager);
                }
            }
        }

        for (int j = 0; j < availableTargets.Count; j++)
        {
            float distanceFromTarget = Vector3.Distance(targetTransform.transform.position, availableTargets[j].transform.position);

            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = availableTargets[j].lockOnTransform;
            }
        }
    }

    public void ClearLockOnTarget()
    {
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
    }
}
