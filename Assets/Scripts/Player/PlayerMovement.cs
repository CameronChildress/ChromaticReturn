using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    Transform cameraObject;
    PlayerStats playerStats;
    public Weapon weapon;
    public float weaponTimer = 1f;
    public Rigidbody rigidbody;

    AudioSource audio;

    public bool isLightAttacking;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffSet = 0.5f;
    public LayerMask groundLayer;

    Vector3 moveDirection;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speed")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5f;
    public float sprintingSpeed = 7f;
    public float rotationSpeed = 15f;

    [Header("Jump Speed")]
    public float gravityIntensity = 3f;
    public float jumpHeight = -15f;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponentInChildren<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        rigidbody = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
        //audio = GetComponentInChildren<AudioSource>();

        cameraObject = Camera.main.transform;
    }

    private void Update()
    {
        if (isLightAttacking)
        {
            weaponTimer -= Time.deltaTime;
        }

        if (weaponTimer <= 0)
        {
            //weapon.ToggleCollider();
            weaponTimer = 1f;
            isLightAttacking = false;
        }
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting) return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        //shouldnt keep moving the player if mid jump
        if (isJumping) return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        //If sprinting, use sprintSpeed else use normal speed
        if (isSprinting && playerStats.currentStamina > 1)
        {
            moveDirection *= sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f || playerStats.currentStamina < 1)
            {
                moveDirection *= runningSpeed;
            }
            else
            {
                moveDirection *= walkingSpeed;
            }
        }

        Vector3 movementVelocity = moveDirection;
        rigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        //shouldnt rotate the player if mid jump
        if (isJumping) return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection += cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y += rayCastHeightOffSet;
        targetPosition = transform.position;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            animatorManager.animator.SetBool("isUsingRootMotion", false);
            inAirTimer += Time.deltaTime;
            rigidbody.AddForce(transform.forward * leapingVelocity);
            rigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            //cast a ray from the bottom of the character, if it hits then set our height to it
            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;

            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }

        if (playerManager.isInteracting || inputManager.moveAmount > 0)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            //Gets a smooth 'pop' off the ground and can change the values if needed
            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocty = moveDirection;
            //taking already existing movement velocity and adding the jump velocity
            playerVelocty.y = jumpingVelocity;

            rigidbody.velocity = playerVelocty;
        }
    }

    public void HandleDodge()
    {
        if (playerManager.isInteracting) return;

        animatorManager.PlayTargetAnimation("Dodge", true, true);
        //Toggle Invulnerable bool for no HP damage during action
    }

    public void HandleRoll()
    {
        if (playerManager.isInteracting) return;

        animatorManager.PlayTargetAnimation("Roll", true, true);
        //Toggle Invulnerable bool for no HP damage during action
    }

    public void HandleAttacking()
    {
        if (playerManager.isInteracting) return;

        if (!isLightAttacking)
        {
            animatorManager.PlayTargetAnimation("LightAttack01", true);
            isLightAttacking = true;
            //weapon.ToggleCollider();
            //audio?.Play();
        }
    }
}