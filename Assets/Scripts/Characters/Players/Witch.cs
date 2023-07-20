using UnityEngine;
using UnityEngine.InputSystem;
using EricGames.Core.Characters;
using EricGames.Core.Mechanics;
using static UnityEngine.InputSystem.InputAction;

namespace My2DGame.Characters.Players
{
    [RequireComponent(typeof(Character))]
    public class Witch : MonoBehaviour
    {
        private PlayerInput playerInput;
        // Input Ations
        private InputAction actionMove;
        private InputAction actionRun;
        private InputAction actionJump;
        private InputAction actionBlock;
        private InputAction actionAttack;
        private InputAction actionDodge;
        public JumpCircle jumpCircle;
        public Transform weaponPoint;
        public Transform jumpPoint;

        private Character character;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            character = GetComponent<Character>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            character.OnHitted += this.OnHitted;

            actionMove = playerInput.actions["Move"];

            actionRun = playerInput.actions["Run"];

            actionJump = playerInput.actions["Jump"];
            actionJump.performed += this.Jump;

            actionBlock = playerInput.actions["Defend"];
            actionBlock.started += this.HandleBlocking;
            actionBlock.canceled += this.HandleBlocking;

            actionAttack = playerInput.actions["Attack"];
            actionAttack.started += this.Attack;

            actionDodge = playerInput.actions["Dodge"];
            actionDodge.started += this.Dodge;

            this.character.OnJump += this.OnJumpEvent;
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

        public void HandleBlocking(CallbackContext callbackContext)
        {
            if (callbackContext.started)
                character.Block(true);
            else if (callbackContext.canceled)
                character.Block(false);
        }

        private void OnMove()
        {

        }

        public void Attack(CallbackContext callbackContext)
        {
            character.Attack();
        }

        public void Dodge(CallbackContext callbackContext)
        {
            character.Dodge();
        }

        private void OnJumpEvent()
        {
            Instantiate(jumpCircle, jumpPoint.position, jumpPoint.rotation);
        }

        public void OnAttack()
        {
        }
    }
}