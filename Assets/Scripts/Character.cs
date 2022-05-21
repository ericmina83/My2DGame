
using UnityEngine;

public class Character : MonoBehaviour
{
    int hp;
    int mp;

    private const float INPUT_DIR_CHANGED_THRES = 0.12f;
    [SerializeField] protected bool facingRight = true;
    protected Vector3 forward
    {
        get
        {
            return transform.right * (flip ? -1 : 1);
        }
    }
    private bool flip;

    public void Start()
    {
        flip = !facingRight;
    }

    public void GetHit(Damage damage)
    {
        PopupDamage.Create(transform.position, damage.damageAmount);
    }

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
}