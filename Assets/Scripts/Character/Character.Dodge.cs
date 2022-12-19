using UnityEngine;
using My2DGame.Utility;
using My2DGame.Core;
using My2DGame.Equipment.Weapons;
using System.Collections.Generic;

namespace My2DGame.Characters
{
    public partial class Character
    {
        private bool dodgeEnd = false;

        private void InitStateDodge()
        {
            stateMachine.ReigsterStateDelegate(State.DODGE, StateDelegateType.START, DodgeStateStart);
            stateMachine.ReigsterStateDelegate(State.DODGE, StateDelegateType.END, DodgeStateEnd);

            stateMachine.RegisterTransition(State.DODGE, State.MOVE, 0.0f,
                null,
                () => dodgeEnd && landingState == LandingState.GROUNDED);
            stateMachine.RegisterTransition(State.DODGE, State.FALL, 0.0f,
                null,
                () => dodgeEnd && landingState == LandingState.FALLING);
            stateMachine.RegisterTransition(State.DODGE, State.JUMP, 0.0f,
                null,
                () => dodgeEnd && landingState == LandingState.JUMPING);
            // stateMachine.RegisterTransition(State.DODGE, State.JUMP, 0.0f,
            //     new TriggerType[] { TriggerType.JUMP },
            //     () => dodgeEnd);
        }

        #region Trigger Function

        public void Dodge()
        {
            stateMachine.SetTrigger(TriggerType.DODGE, 0.4f);
        }

        #endregion

        #region State Delegate

        private void DodgeStateStart()
        {
            dodgeEnd = false;
            untouchable = true;
            obstacle.SetActive(false);

            animatorTriggerHandler.SetTrigger(dodgeParameterHash, 0.4f);
        }

        private void DodgeStateEnd()
        {
            dodgeEnd = false;
            untouchable = false;
            obstacle.SetActive(true);
        }

        private void DodgeEnd()
        {
            dodgeEnd = true;
        }

        #endregion
    }
}