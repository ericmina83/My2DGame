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
    InputAction actionBlock;
    InputAction actionAttack;
    InputAction actionDodge;
    TriggerHandler triggerHandler;

    [SerializeField] private List<Weapon> weapon; // weapon's gameobject
    [SerializeField] private float jumpForce = 7.0f; // decide how height when jumping
    [SerializeField] private float blockingDuration = 0.2f;

    public GameObject obstacle;
    public JumpCircle jumpCircle;
    public Transform weaponPoint;
    public Transform jumpPoint;

    // Start is called before the first frame update
    override protected void Init()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        triggerHandler = new TriggerHandler(animator);

        actionMove = playerInput.actions["Move"];
        actionRun = playerInput.actions["Run"];
        actionJump = playerInput.actions["Jump"];
        actionBlock = playerInput.actions["Defend"];
        actionAttack = playerInput.actions["Attack"];
        actionDodge = playerInput.actions["Dodge"];
    }

    Vector2 input;
    float speedX;

    bool canDoNext = false;
    bool shootTriggerEnable = false;
    bool shootTriggerOn = false;
    bool doubleJump = true;
    public bool blocking = false;
    public bool awaking = false;
    float blockingTime = 0.0f;
    float hitForce;
    bool unstopable = false;
    bool canJump = false;

    protected override Damage OnHitted(Damage damage)
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0);
        var currentTransition = animator.GetAnimatorTransitionInfo(0);

        if (currentState.IsName("Hitted"))
            return damage;

        if (blocking) // blocking
        {
            triggerHandler.SetTrigger("Hitted when Blocking", 0.1f);

            if (Vector3.Dot(damage.attackFrom, transform.right) < 0.0f)

                if (blockingTime < blockingDuration) // perfect blocking
                {
                    damage.enable = false; // disable
                }
                else // normal blocking
                {
                    // todo 
                }
        }
        else // non-blocking
        {
            triggerHandler.SetTrigger("Hitted", 0.1f);

            if (animator.IsInTransition(0)) // in transition
            {
            }
            else // in state
            {
            }
        }

        return damage;
    }

    // Update is called once per frame
    void Update()
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0); // get current state in animator
        var nextState = animator.GetNextAnimatorStateInfo(0); // get current state in animator
        var currentTransition = animator.GetAnimatorTransitionInfo(0); // get current state in animator

        // handle  movement detect
        input = actionMove.ReadValue<Vector2>();
        speedX = Mathf.Abs(input.x);

        awaking = !currentState.IsName("Hitted");

        if (speedX > 0f)
            if (!awaking)
            {
                awaking = true;
                triggerHandler.SetTrigger("Awake", 0.1f);
            }

        animator.SetFloat("speedX", speedX, 0.02f, Time.deltaTime); // now horizontal input value

        // handle rotation
        if ((!currentState.IsTag("Attacking")) // can't rotate when attacking
         && (!currentState.IsName("Hitted")) // can't rotate when hitted
        )
            CheckRotation(input.x);

        JumpHandler(currentState);
        AttackHandler(currentState);
        BlockHandler(currentState, nextState);

        // handle dodge
        if (actionDodge.triggered)
            triggerHandler.SetTrigger("Dodge", 0.1f);

        animator.SetBool("unstopable", unstopable);
        animator.SetBool("Can Jump", doubleJump);

        triggerHandler.Update(Time.deltaTime);
    }

    #region Animation Events
    void ShootTriggerEnable()
    {
        Debug.Log("ShootTriggerEnable");
        shootTriggerEnable = true;
        shootTriggerOn = false;
        damage.enable = true;
        // weapon.SetDamage(damage);
    }

    void CanDoNext()
    {
        Debug.Log("CanDoNext");
        canDoNext = true;
        damage.enable = false;
        // weapon.SetDamage(damage);
    }

    void DodgeStart()
    {
        untouchable = true;
        obstacle.SetActive(false);
    }

    void DodgeEnd()
    {
        untouchable = false;
        obstacle.SetActive(true);
    }

    #endregion

    #region Action Handler

    void BlockHandler(AnimatorStateInfo currentState, AnimatorStateInfo nextState)
    {
        // handle block detect
        animator.SetBool("blocking", actionBlock.IsPressed());

        if (!animator.IsInTransition(0)) // in state
            blocking = currentState.IsName("Blocking");
        else // in transition
            blocking = nextState.IsName("Blocking");

        if (blocking)
            blockingTime += Time.deltaTime;
        else
            blockingTime = 0.0f;
    }

    void JumpHandler(AnimatorStateInfo currentState)
    {
        // handle can Jump
        if (currentState.IsTag("Attacking"))
            canJump = false;
        else
            canJump = true;

        // handle jump detect
        if (isGrounded) // reset double jump
            doubleJump = true;

        if (actionJump.triggered && canJump)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(0, jumpForce);
                triggerHandler.SetTrigger("Jump", 0.1f);
                Instantiate(jumpCircle, jumpPoint.position, jumpPoint.rotation);
            }
            else if (doubleJump)
            {
                rb.velocity = new Vector2(0, jumpForce);
                triggerHandler.SetTrigger("Jump", 0.1f);
                doubleJump = false;
                Instantiate(jumpCircle, jumpPoint.position, jumpPoint.rotation);
            }
        }


        // information for animator
        animator.SetFloat("speedY", rb.velocity.y); // curent vertical speed
        animator.SetBool("isGrounded", isGrounded);
    }

    void AttackHandler(AnimatorStateInfo currentState)
    {
        // handle attack detect
        if (actionAttack.triggered)
        {
            if (currentState.IsTag("Attacking"))
            {
                if (shootTriggerEnable == true)
                {
                    shootTriggerOn = true;
                    shootTriggerEnable = false;
                }
            }
            else
            {
                triggerHandler.SetTrigger("Attack", 0.1f);
            }
        }

        if (canDoNext)
        {
            if (shootTriggerOn)
            {
                triggerHandler.SetTrigger("Attack", 0.1f);
                canDoNext = false;
                shootTriggerOn = false;
            }
        }
    }

    #endregion
}
