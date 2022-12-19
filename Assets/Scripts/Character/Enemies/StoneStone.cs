using My2DGame.Equipment.Weapons;
using UnityEngine;

namespace My2DGame.Characters.Enemies
{
    public class StoneStone : Character
    {
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

        protected override void Awake()
        {
            base.Awake();
        }

        void Update()
        {
            Vector3 distanceFromEnemy = target.transform.position - transform.position;

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
}
