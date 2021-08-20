using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerMovement playerMovement;
    AnimatorManager animatorManager;
    
    Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool bButtonInput;
    public bool ctrlInput;
    public bool altInput;
    public bool jumpInput;

    public bool isAttacking;

    void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.B.performed += i => bButtonInput = true;
            playerControls.PlayerActions.B.canceled += i => bButtonInput = false;

            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
            playerControls.PlayerActions.Jump.canceled += i => jumpInput = false;

            playerControls.PlayerActions.CTRL.performed += i => ctrlInput = true;
            playerControls.PlayerActions.ALT.performed += i => altInput = true;
            playerControls.PlayerActions.LeftClick.performed += i => isAttacking = true;
        }

        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleDodgeInput();
        HandleRollInput();
        HandleAttackInput();
    }

    void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;

        cameraInputX = Mathf.Clamp(cameraInput.x, -1.25f, 1.25f);
        cameraInputY = Mathf.Clamp(cameraInput.y, -1.25f, 1.25f);

        //cameraInputX = cameraInput.x;
        //cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerMovement.isSprinting);
    }

    void HandleSprintingInput()
    {
        if (bButtonInput && moveAmount > 0.5f)
        {
            playerMovement.isSprinting = true;
        }
        else
        {
            playerMovement.isSprinting = false;
        }
    }

    void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerMovement.HandleJumping();
        }
    }

    void HandleDodgeInput()
    {
        if (ctrlInput)
        {
            ctrlInput = false;
            playerMovement.HandleDodge();
        }
    }

    void HandleRollInput()
    {
        if (altInput)
        {
            altInput = false;
            playerMovement.HandleRoll();
        }
    }

    void HandleAttackInput()
    {
        if (isAttacking)
        {
            isAttacking = false;
            playerMovement.HandleAttacking();
        }
    }
}
