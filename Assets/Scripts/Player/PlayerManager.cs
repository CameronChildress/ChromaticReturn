using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputManager inputManager;
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    CameraManager cameraManager;
    Animator animator;

    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableGameObject;

    public bool isInteracting;
    public bool isUsingRootMotion;

    public float respawnTimer = 2f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
        interactableUI = FindObjectOfType<InteractableUI>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
        CheckForInteractable();
    }

    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        isUsingRootMotion = animator.GetBool("isUsingRootMotion");
        playerMovement.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerMovement.isGrounded);

        inputManager.pickUpInput = false;
    }

    public void CheckForInteractable()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraManager.collisionLayers))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if (interactableObject != null)
                {
                    string interactableText = interactableObject.interactableText;
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);

                    if (inputManager.pickUpInput)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if (interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if (itemInteractableGameObject != null && inputManager.pickUpInput)
            {
                itemInteractableGameObject.SetActive(false);
            }
        }
    }
}
