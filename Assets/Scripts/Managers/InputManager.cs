using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerHandleAttacks playerHandleAttacks;
    PlayerInventory playerInventory;
    PlayerMovement playerMovement;
    AnimatorManager animatorManager;
    PlayerStats playerStats;


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

    public bool pickUpInput;

    public bool dPadUp;
    public bool dPadDown;
    public bool dPadLeft;
    public bool dPadRight;

    public bool isLightAttacking;
    public bool isHeavyAttacking;

    void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHandleAttacks = GetComponent<PlayerHandleAttacks>();
        playerInventory = GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();
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
            playerControls.PlayerActions.LightAttack.performed += i => isLightAttacking = true;
            playerControls.PlayerActions.HeavyAttack.performed += i => isHeavyAttacking = true;

            playerControls.PlayerQuickSlots.DPadUp.performed += i => dPadUp = true;
            playerControls.PlayerQuickSlots.DPadDown.performed += i => dPadDown = true;
            playerControls.PlayerQuickSlots.DPadLeft.performed += i => dPadLeft = true;
            playerControls.PlayerQuickSlots.DPadRight.performed += i => dPadRight = true;

            playerControls.PlayerActions.PickUp.performed += i => pickUpInput = true;
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

        HandleQuickSlotInput();
        HandleInteractableInput();
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
            playerStats.UseStamina(8);   
        }
        else
        {
            playerStats.ReturnStamina(8);
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
        if (isLightAttacking && playerStats.currentStamina > 0)
        {
            isLightAttacking = false;
            playerHandleAttacks.HandleLightAttack(playerInventory.rightWeapon);
            playerStats.UseStamina(25);
        }

        if (isHeavyAttacking && playerStats.currentStamina > 0)
        {
            isHeavyAttacking = false;
            playerHandleAttacks.HandleHeavyAttack(playerInventory.rightWeapon);
            playerStats.UseStamina(50);
        }

        if (playerStats.currentStamina <= 0)
        {
            playerStats.currentStamina = 0;
        }
    }

    void HandleQuickSlotInput()
    {
        if (dPadRight)
        {
            playerInventory.ChangeRightWeapon();
            dPadRight = false;
        }
        if (dPadLeft)
        {
            playerInventory.ChangeLeftWeapon();
            dPadLeft = false;
        }
    }

    void HandleInteractableInput()
    {
        //pickUpInput = false;
    }
}
