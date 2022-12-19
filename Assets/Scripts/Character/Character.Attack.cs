using UnityEngine;
using My2DGame.Utility;
using My2DGame.Core;
using My2DGame.Equipment.Weapons;
using System.Collections.Generic;

namespace My2DGame.Characters
{
    public partial class Character
    {
        private void InitStateAttack()
        {
            stateMachine.ReigsterStateDelegate(State.ATTACK, StateDelegateType.START, AttackStateStart);

            stateMachine.RegisterTransition(State.ATTACK, State.ATTACK, 0f,
                new TriggerType[] { TriggerType.ATTACK },
                () => canDoNext);
            stateMachine.RegisterTransition(State.ATTACK, State.MOVE, 0.0f,
                null,
                () => animator.CheckCurrentStateIs(0, moveStateTagHash) && landingState == LandingState.GROUNDED);
            stateMachine.RegisterTransition(State.ATTACK, State.FALL, 0.0f,
                null,
                () => animator.CheckCurrentStateIs(0, moveStateTagHash) && landingState == LandingState.FALLING);
            stateMachine.RegisterTransition(State.ATTACK, State.JUMP, 0.0f,
                null,
                () => animator.CheckCurrentStateIs(0, moveStateTagHash) && landingState == LandingState.JUMPING);
        }

        #region Trigger Function

        public void Attack()
        {
            stateMachine.SetTrigger(TriggerType.ATTACK, 0.4f);
            animatorTriggerHandler.SetTrigger(attackParameterHash, 0.4f);
        }

        #endregion

        #region State Delegate

        public void CanDoNext()
        {
            canDoNext = true;
        }

        public void CanDoNextEnd()
        {
            canDoNext = false;
        }

        private void AttackStateStart()
        {
            canDoNext = false;
        }

        #endregion
    }
}