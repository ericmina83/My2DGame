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

    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
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
                    transform.position += forward * Time.deltaTime;
                }
                else
                {
                    animator.SetTrigger("attack");
                }
            }
        }
    }
}
