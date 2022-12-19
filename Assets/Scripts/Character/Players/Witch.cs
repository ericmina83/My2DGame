using UnityEngine;
using UnityEngine.InputSystem;

namespace My2DGame.Characters.Players
{
    public class Witch : MonoBehaviour
    {
        PlayerInput playerInput;

        // Input Ations
        InputAction actionMove;
        InputAction actionRun;
        InputAction actionJump;
        InputAction actionBlock;
        InputAction actionAttack;
        InputAction actionDodge;
        public JumpCircle jumpCircle;
        public Transform weaponPoint;
        public Transform jumpPoint;

        private Character character;

        // Start is called before the first frame update
        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            character = GetComponent<Character>();

            character.OnHitted += this.OnHitted;

            actionMove = playerInput.actions["Move"];

            actionRun = playerInput.actions["Run"];

            actionJump = playerInput.actions["Jump"];
            actionJump.started += this.Jump;

            actionBlock = playerInput.actions["Defend"];
            actionBlock.started += this.HandleBlocking;
            actionBlock.canceled += this.HandleBlocking;

            actionAttack = playerInput.actions["Attack"];
            actionAttack.started += this.Attack;

            actionDodge = playerInput.actions["Dodge"];
            actionDodge.started += this.Dodge;
        }

        Damage OnHitted(Damage damage)
        {
            return damage;
        }

        public void Jump(InputAction.CallbackContext callbackContext)
        {
            character.Jump();
        }

        void Update()
        {
            character.Move(actionMove.ReadValue<Vector2>());
        }

        public void HandleBlocking(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
                character.Block(true);
            else if (callbackContext.canceled)
                character.Block(false);
        }

        public void Attack(InputAction.CallbackContext callbackContext)
        {
            character.Attack();
        }

        public void Dodge(InputAction.CallbackContext callbackContext)
        {
            character.Dodge();
        }


        public void OnJump()
        {
            Instantiate(jumpCircle, jumpPoint.position, jumpPoint.rotation);
        }
    }
}