using UnityEngine;
using EricGames.Runtime.Equipment;
using EricGames.Runtime.Characters;
using EricGames.Runtime.StateMachine;

namespace My2DGame.Characters.Enemies
{
    public class StoneStone : MonoBehaviour
    {
        [SerializeField] private Character2D character;

        [SerializeField] private float movingSpeed;

        enum MonsterAIState
        {
            IDLE,
            MOVE,
            ATTACK,
        }

        private readonly StateMachine<MonsterAIState> stateMachine = new(MonsterAIState.IDLE);

        private void Awake()
        {
            var idleState = stateMachine.GetSubState(MonsterAIState.IDLE);
            idleState.RegisterTransition(MonsterAIState.ATTACK, 3.0f, null);

            var attackState = stateMachine.GetSubState(MonsterAIState.ATTACK);
            attackState.RegisterTransition(MonsterAIState.IDLE, 3.0f, null);
            attackState.StateStartEvent += OnAttackStateStart;
        }

        private void Update()
        {
            stateMachine.Tick(Time.deltaTime);
        }

        private void OnAttackStateStart()
        {
            character.Attack();
        }
    }
}
