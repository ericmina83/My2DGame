using UnityEngine;
using UnityEngine.InputSystem;
using EricGames.Runtime.Characters;
using EricGames.Runtime.Equipment;
using static UnityEngine.InputSystem.InputAction;

namespace My2DGame.Characters.Players
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(PlayerInput))]
    public class Witch : MonoBehaviour
    {
        private PlayerInput playerInput;
        // Input Actions
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

        [SerializeField]
        private Shield shield;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            character = GetComponent<Character>();

            actionMove = playerInput.actions["Move"];
            actionRun = playerInput.actions["Run"];
            actionJump = playerInput.actions["Jump"];
            actionBlock = playerInput.actions["Defend"];
            actionAttack = playerInput.actions["Attack"];
            actionDodge = playerInput.actions["Dodge"];
        }

        // Start is called before the first frame update
        private void OnEnable()
        {
            actionJump.performed += Jump;
            actionBlock.started += StartBlocking;
            actionBlock.canceled += StopBlocking;
            actionAttack.started += Attack;
            actionDodge.started += Dodge;
            character.OnJump += OnJumpEvent;
            character.OnBlockStartEvent += OnBlockStartEvent;
            character.OnBlockEndEvent += OnBlockEndEvent;
        }

        private void OnDisable()
        {
            actionJump.performed -= Jump;
            actionBlock.started -= StartBlocking;
            actionBlock.canceled -= StopBlocking;
            actionAttack.started -= Attack;
            actionDodge.started -= Dodge;
            character.OnJump -= OnJumpEvent;
            character.OnBlockStartEvent -= OnBlockStartEvent;
            character.OnBlockEndEvent -= OnBlockEndEvent;
        }

        public void Jump(CallbackContext callbackContext) => character.Jump();

        public void StartBlocking(CallbackContext callbackContext) => character.Block(true);

        public void StopBlocking(CallbackContext callbackContext) => character.Block(false);

        public void Attack(CallbackContext callbackContext) => character.Attack();

        public void Dodge(CallbackContext callbackContext) => character.Dodge();

        void Update()
        {
            character.Move(actionMove.ReadValue<Vector2>());
        }

        private void OnMove()
        {

        }

        private void OnJumpEvent()
        {
            Instantiate(jumpCircle, jumpPoint.position, jumpPoint.rotation);
        }

        private void OnBlockStartEvent()
        {
            shield.Show();
        }

        private void OnBlockEndEvent()
        {
            shield.Hide();
        }


        public void OnAttack()
        {
        }
    }
}