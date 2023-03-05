using System;
using UnityEngine;

namespace EricGames.Core.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character2D : Character
    {
        private new Rigidbody2D rigidbody2D;
        protected override float speedY => rigidbody2D.velocity.y;

        private const float INPUT_DIR_CHANGED_THRES = 0.12f;
        [SerializeField] private bool facingRight = true;
        private bool flip;

        // check current rotation is same as input
        public override Quaternion CheckRotation(Vector2 input)
        {
            var angle = 0.0f;

            if (input.x < 0)
            {
                angle += 180.0f;
            }

            if (!facingRight)
            {
                angle += 180.0f;
            }
            // if ((movingX > INPUT_DIR_CHANGED_THRES && facingRight == false) || // looking right but wanna look left
            //     (movingX < -INPUT_DIR_CHANGED_THRES && facingRight == true)) // looking left but wanna look right
            // {
            //     transform.Rotate(Vector3.up * 180.0f);
            //     facingRight = !facingRight;
            // }

            return Quaternion.Euler(0, angle, 0);
        }

        public override void Start()
        {
            base.Start();

            flip = !facingRight;

            rigidbody2D = GetComponent<Rigidbody2D>();
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

        public override void HandleAttack()
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
            RaycastHit2D hit = Physics2D.Raycast(
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
            animator.SetBool("isGrounded", landingState == LandingState.GROUNDED);
            animator.SetBool("unstopable", unstopable);
            animator.SetBool("Can Jump", doubleJump);
        }
    }
}