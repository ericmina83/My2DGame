using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Character enemy;
    private Animator animator;
    [SerializeField] private float movingSpeed;

    enum MonsterAIState
    {
        IDLE,
        MOVE,
        ATTACK,
    }

    MonsterAIState aiState;
    [SerializeField] private Weapon backWeapon;
    [SerializeField] private Weapon frontWeapon;


    override protected void Init()
    {
        animator = GetComponent<Animator>();
    }

    protected override Damage OnHitted(Damage damage)
    {
        return damage;
    }

    void Update()
    {
        Vector3 distanceFromEnemy = enemy.transform.position - transform.position;

        var currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (!animator.IsInTransition(0))
        {
            if (currentState.IsName("Idle@Monster"))
            {
                CheckRotation(distanceFromEnemy.x);

                if (Mathf.Abs(distanceFromEnemy.x) > 2.0f)
                {
                    transform.position += movingSpeed * forward * Time.deltaTime;
                }
                else
                {
                    animator.SetTrigger("attack");
                }
            }
        }
    }

    void AttackingStart()
    {
        damage.enable = true;
        backWeapon.SetDamage(damage);
        frontWeapon.SetDamage(damage);
    }

    void AttackingEnd()
    {
        damage.enable = false;
        backWeapon.SetDamage(damage);
        frontWeapon.SetDamage(damage);
    }
}
