using UnityEngine;
using EricGames.Runtime.Characters;

namespace My2DGame.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character2D : Character
    {
        private new Rigidbody2D rigidbody2D;
        protected override float SpeedY => rigidbody2D.velocity.y;

        private const float INPUT_DIR_CHANGED_THRESHOLD = 0.12f;

        [SerializeField] private bool facingRight = true;

        // check current rotation is same as input
        public override Quaternion CheckRotation(Quaternion currentRotation, Vector2 input)
        {
            if (Mathf.Abs(input.x) < INPUT_DIR_CHANGED_THRESHOLD)
                return currentRotation;

            var angle = 0.0f;

            if (input.x < 0)
            {
                angle += 180.0f;
            }

            if (!facingRight)
            {
                angle += 180.0f;
            }

            return Quaternion.Euler(0, angle, 0);
        }

        public override void HandleMove(Vector2 input)
        {
            if (!animator.applyRootMotion)
            {
                var velocity = rigidbody2D.velocity;
                velocity.x = input.x * moveSpeed;
                rigidbody2D.velocity = velocity;
            }
        }

        protected override void HandleAttacking()
        {
        }

        protected override void HandlePreparing()
        {
        }

        protected override void HandleResetting()
        {
        }

        protected override void HandleFall(Vector2 moveInput, Vector3 lookInput)
        {
        }

        protected override void HandleApplyJumpForce(float jumpForce)
        {
            var velocity = rigidbody2D.velocity;
            velocity.y = jumpForce;
            rigidbody2D.velocity = velocity;
        }

        [SerializeField] protected LayerMask groundMask; // 

        protected override bool CheckIsGrounded()
        {
            // check ground
            var hit = Physics2D.Raycast(
                new Vector2(transform.position.x, transform.position.y) + Vector2.up * 0.5f,
                Vector2.down, 0.7f,
                groundMask);

            return hit.collider != null;
        }

        protected override void HandleJump(Vector2 moveInput, Vector3 lookInput)
        {
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnAwake()
        {
        }

        protected override void OnStart()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}