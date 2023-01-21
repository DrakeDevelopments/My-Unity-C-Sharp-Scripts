using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public CinemachineSwitcher cinemachineSwitcher;

    private PauseMenu pauseMenu;
    private SpiritBlast spiritBlast;

    public Animator animator;
    public Transform playerRot;
    private float playerYRot;

    private PlayerInputActions playerControls;
    private PlayerInput playerInput;
    private IInteractable interactable;

    private Rigidbody2D rb;

    public float runSpeed = 0f;
    public float jumpDamp = 0f;
    public float xAxisDeadZone = 0.3f;

    public GameObject prompt;

    private float timeBtwnAttack;
    public float startTimeBtwnAttack;

    float horizontalMove = 0f;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction sword;
    private InputAction jumpRelease;

    bool pjump = false;
    public bool canPause;
    public bool canRest;

    //Used for swapping camera states for the player looking up and down
    //Time before camera moves
    private float timeSpan = 0.6f;

    //Amount of time player has press the up and down buttons for moving the camera
    public float timeUp;
    public float timeDown;


    public float playerSpeedX;
    public float playerSpeedY;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spiritBlast = GetComponent<SpiritBlast>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        fire = playerControls.Player.Fire;
        jump = playerControls.Player.Jump;
        jumpRelease = playerControls.Player.JumpRelease;
        sword = playerControls.Player.Sword;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void PlayerControls()
    {
        playerControls.Player.Enable();
        playerControls.UI.Disable();
    }

    public void UIControls()
    {
        playerControls.Player.Disable();
        playerControls.UI.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            prompt.SetActive(true);
            interactable = collision.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            prompt.SetActive(false);
            interactable = null;
        }
    }

    void Update()
    {
        //Interact button
        if (playerControls.Player.Interact.triggered)
        {
            if(interactable != null)
            {
                interactable.Interact();
            }
        }

        //Pause Menu
        if (canPause == true && playerControls.Player.Pause.triggered)
        {
            pauseMenu.Pause();
        }

        //movement
        if(Mathf.Abs(move.ReadValue<Vector2>().x) > xAxisDeadZone)
        {
            horizontalMove = move.ReadValue<Vector2>().x * runSpeed;
        }

        if (Mathf.Abs(move.ReadValue<Vector2>().x) <= xAxisDeadZone)
        {
            horizontalMove = 0f;
        }

        //FireBreath
        if (fire.triggered)
        {
            spiritBlast.Shoot();
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //Jump animation plays when jump button is pressed.
        if (jump.triggered)
            {
                pjump = true;
            }
        
        //Damping jump when letting go of the jump button.
        if(jumpRelease.triggered)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpDamp);
            }
        }

        if (jump.triggered)
        {
            controller.DoubleJump();
        }

        //Attack  Start

        if (timeBtwnAttack <= 0)
        {
            if (sword.triggered)
            {

                playerYRot = playerRot.rotation.y;                                          //Updates playerYrot with the y rotation of the player i.e what way the player is facing.

                if (move.ReadValue<Vector2>().y <= 0.5 && controller.m_Grounded == true)
                {
                    animator.SetBool("NGAttack", true);                                        //Activate neutral ground attack animation
                }

                if (move.ReadValue<Vector2>().y >= 0.5 && controller.m_Grounded == true)
                {
                    animator.SetBool("UGAttack", true);                                        //Activate up ground attack animation
                }

                if (controller.m_Grounded == false & playerYRot < 0f && move.ReadValue<Vector2>().x <= 0 && Mathf.Abs(move.ReadValue<Vector2>().y) < 0.5)
                {
                    animator.SetBool("NAAttack", true);                                        //Activate Facing Left - neutral air attack 
                }

                if (controller.m_Grounded == false & playerYRot >= 0f && move.ReadValue<Vector2>().x >= 0 && Mathf.Abs(move.ReadValue<Vector2>().y) < 0.5)
                {
                    animator.SetBool("NAAttack", true);                                        //Activate Facing Right - neutral air 
                }

                if (move.ReadValue<Vector2>().y >= 0.5 && controller.m_Grounded == false)
                {
                    animator.SetBool("UAAttack", true);                                        //Activate up air attack animation
                }

                if (controller.m_Grounded == false & playerYRot < 0f && move.ReadValue<Vector2>().x > 0.5 && Mathf.Abs(move.ReadValue<Vector2>().y) < 0.5)
                {
                    animator.SetBool("BAAttack", true);                                        //Activate Facing Left - back air attack animation
                }

                if (controller.m_Grounded == false & playerYRot >= 0f && move.ReadValue<Vector2>().x < -0.5 && Mathf.Abs(move.ReadValue<Vector2>().y) < 0.5)
                {
                    animator.SetBool("BAAttack", true);                                        //Activate Facing Right - back air attack 
                }

                if (move.ReadValue<Vector2>().y <= -0.5 && controller.m_Grounded == false)
                {
                    animator.SetBool("DAAttack", true);                                        //Activate down air attack 
                }

                timeBtwnAttack = startTimeBtwnAttack;
            }
        }

        else
        {
            timeBtwnAttack -= Time.deltaTime;
        }

        //Checks if player is grounded and updates animator parameter with value.
        animator.SetBool("IsGrounded", controller.m_Grounded);

        //This is all camera stuff bellow
        //Theres no crouch its just looks for the "s" key or down on game pad.
        if (move.ReadValue<Vector2>().y <= -0.5)
        {
            timeDown += Time.deltaTime;
            if (timeDown > timeSpan)
            {
                animator.SetBool("LookDown", true);
            }
        }

        if (move.ReadValue<Vector2>().y > -0.5)
        {
            ResetDownCameraTypes();
        }

        if(move.ReadValue<Vector2>().y >= 0.5)
        {
            timeUp += Time.deltaTime;
            if (timeUp > timeSpan)
            {
                animator.SetBool("LookUp", true);
            }
        }

        if (move.ReadValue<Vector2>().y < 0.5)
        {
            ResetUpCameraTypes();
        }

        playerSpeedX = Mathf.Abs(horizontalMove);
        playerSpeedY = Mathf.Abs(rb.velocity.y);

        if (playerSpeedX > 0.1 | playerSpeedY >0.1)
        {
            ResetDownCameraTypes();
            ResetUpCameraTypes();
        }
    }

    private void LookDown()
    {
        cinemachineSwitcher.SwitchStateDown();
    }

    private void LookUp()
    {
        cinemachineSwitcher.SwitchStateUp();
    }

    private void ResetUpCameraTypes()
    {
        timeUp = 0;
        animator.SetBool("LookUp", false);
    }
    private void ResetDownCameraTypes()
    {
        timeDown = 0;
        animator.SetBool("LookDown", false);
    }

    void FixedUpdate()
    {
        // Move our character

        controller.Move(horizontalMove * Time.fixedDeltaTime, pjump);


        pjump = false;
    }
}