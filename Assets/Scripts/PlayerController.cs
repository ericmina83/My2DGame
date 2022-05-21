using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : Character
{

    PlayerInput playerInput;
    Animator animator;
    Rigidbody2D rb;

    // Input Ations
    InputAction actionMove;
    InputAction actionRun;
    InputAction actionJump;
    InputAction actionDefend;
    InputAction actionAttack;
    InputAction actionDodge;

    [SerializeField] private Weapon weapon; // weapon's gameobject
    [SerializeField] private float jumpForce = 7.0f; // decide how height when jumping
    [SerializeField] private LayerMask groundMask; // 
    [SerializeField] private float runningSpeed = 4.0f; // how running fast

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        actionMove = playerInput.actions["Move"];
        actionRun = playerInput.actions["Run"];
        actionJump = playerInput.actions["Jump"];
        actionDefend = playerInput.actions["Defend"];
        actionAttack = playerInput.actions["Attack"];
        actionDodge = playerInput.actions["Dodge"];
    }

    Vector2 input;
    float speedX;
    float speedY;
    bool isGround;

    bool canDoNext = false;
    bool shootTriggerEnable = false;
    bool shootTriggerOn = false;
    bool doubleJump = true;

    void FixedUpdate()
    {
        CheckIsGround();
    }

    // Update is called once per frame
    void Update()
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0); // get current state in animator
        var currentTransition = animator.GetAnimatorTransitionInfo(0); // get current state in animator

        input = actionMove.ReadValue<Vector2>();
        speedX = Mathf.Abs(input.x);

        // handle
        if (!currentState.IsTag("Attacking"))
            CheckRotation(input.x);

        // handle movement
        if (!currentState.IsTag("Attacking"))
            transform.position += transform.right * runningSpeed * speedX * Time.deltaTime;
        // else
        //     rb.velocity = Vector3.zero;

        // handle jump detect
        if (actionJump.triggered)
        {
            if ((!animator.IsInTransition(0) && !(currentState.IsTag("OnAir") || currentState.IsTag("Attacking"))) || currentTransition.IsUserName("Landing"))
            {
                rb.velocity = new Vector2(0, jumpForce);
                animator.SetTrigger("jump");
                doubleJump = true;
            }
            else if (!isGround && doubleJump)
            {
                rb.velocity = new Vector2(0, jumpForce);
                doubleJump = false;
            }
        }

        // handle attack detect
        if (actionAttack.triggered)
        {
            if (currentState.IsName("Move&Idle"))
            {
                animator.SetTrigger("attack");
            }
            else if (currentState.IsName("Jump") || currentState.IsName("Fall"))
            {
                animator.SetTrigger("attack");
                // rb.velocity = Vector3.zero;
            }
            else if (currentState.IsTag("Attacking"))
                if (shootTriggerEnable == true)
                {
                    shootTriggerOn = true;
                    shootTriggerEnable = false;
                }
        }

        if (canDoNext)
        {
            if (shootTriggerOn)
            {
                animator.SetTrigger("attack");
                canDoNext = false;
                shootTriggerOn = false;
            }
        }

        // information for animator
        animator.SetFloat("speedY", rb.velocity.y); // curent vertical speed
        animator.SetFloat("speedX", speedX, 0.02f, Time.deltaTime); // now horizontal input value
        animator.SetBool("isGround", isGround); // is ground or not (checked by CheckIsGround function)
        animator.SetBool("defend", actionDefend.IsPressed()); // is defend or not
    }

    void CheckIsGround()
    {
        // check ground
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y) + Vector2.up * 0.5f,
            Vector2.down, 0.7f,
            groundMask);

        if (hit.collider != null)
            isGround = true;
        else
            isGround = false;

    }

    void ShootTriggerEnable()
    {
        shootTriggerEnable = true;
        shootTriggerOn = false;
        weapon.attacking = true;
    }

    void CanDoNext()
    {
        canDoNext = true;
        weapon.attacking = false;
    }

}
