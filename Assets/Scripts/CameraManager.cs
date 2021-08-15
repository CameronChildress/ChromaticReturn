using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    public Transform targetTransform; //The obj the camera will follow
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
    public float cameraLookSpeed = 2f;
    public float cameraPivotSpeed = 2f;

    public float lookAngle; //Camera look up and down
    public float pivotAngle; //Camera look left and right
    public float minimumPivotAngle = -35f;
    public float maximumPivotAngle = 35f;

    void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle += (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle -= (inputManager.cameraInputY * cameraPivotSpeed);
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
}
