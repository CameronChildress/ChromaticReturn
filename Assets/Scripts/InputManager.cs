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
        //HandleJumpInput
        //HandleActionInput
    }

    void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

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
}
