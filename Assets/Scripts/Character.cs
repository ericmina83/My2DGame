using UnityEngine;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour
{
    public enum State
    {
        IDLING,
        ATTACKING,
        HITTED,
        JUMPING,
        FALLING,
    }

    public enum Team
    {
        ALLY,
        ENEMY,
    }

    public int hp;
    public int mp;

    public bool isGrounded;

    [SerializeField] private LayerMask groundMask; // 

    private const float INPUT_DIR_CHANGED_THRES = 0.12f;
    [SerializeField] protected bool facingRight = true;
    [SerializeField] protected Body[] bodies = null;

    protected Damage damage;
    public bool untouchable; // can't touch, will close body collider

    public State state;

    protected Vector3 forward
    {
        get
        {
            return transform.right * (flip ? -1 : 1);
        }
    }

    public Team team;

    private bool flip;
    protected abstract void Init();
    public float speedY;

    public virtual void Start()
    {
        flip = !facingRight;
        damage = new Damage(this);

        bodies = GetComponentsInChildren<Body>();

        // handle each body part
        foreach (var body in bodies)
            body.SetOwner(this);


        Init();
    }

    void FixedUpdate()
    {
        CheckIsGrounded();
    }

    void Update()
    {

    }

    void CheckIsGrounded()
    {
        // check ground
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y) + Vector2.up * 0.5f,
            Vector2.down, 0.7f,
            groundMask);

        if (hit.collider != null)
            isGrounded = true;
        else
            isGrounded = false;


        // Vector3 newPos = transform.position;

        // speedY -= 9.8f * Time.deltaTime;
        // newPos.y += speedY * Time.deltaTime;

        // if (isGrounded)
        // {
        //     if (newPos.y < hit.point.y)
        //     {
        //         speedY = 0.0f;
        //         newPos.y = hit.point.y;
        //     }
        // }

        // newPos.z = 0.0f;

        // transform.position = newPos;
    }


    public void Hitted(Damage damage)
    {
        damage = OnHitted(damage);
        if (damage.enable)
        {
            PopupDamage.Create(transform.position, damage.damageAmount);
            damage.enable = false;
        }
    }

    // edit damage or reaction when take damage 
    abstract protected Damage OnHitted(Damage damage);

    // check current rotation is same as input
    protected void CheckRotation(float movingX)
    {
        if ((movingX > INPUT_DIR_CHANGED_THRES && facingRight == false) || // looking right but wanna look left
            (movingX < -INPUT_DIR_CHANGED_THRES && facingRight == true)) // looking left but wanna look right
        {
            transform.Rotate(Vector3.up * 180.0f);
            facingRight = !facingRight;
        }
    }

    #region state JUMPING

    #endregion
}