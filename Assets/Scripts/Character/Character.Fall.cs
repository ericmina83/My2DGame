using UnityEngine;
using My2DGame.Utility;
using My2DGame.Core;
using My2DGame.Equipment.Weapons;
using System.Collections.Generic;

namespace My2DGame.Characters
{
    public partial class Character
    {
        private void InitStateFall()
        {
            stateMachine.ReigsterStateDelegate(State.FALL, StateDelegateType.UPDATE, FallStateUpdate);

            stateMachine.RegisterTransition(State.FALL, State.MOVE, 0.0f,
                null,
                () => landingState == LandingState.GROUNDED);
            stateMachine.RegisterTransition(State.FALL, State.DODGE, 0.0f,
                new TriggerType[] { TriggerType.DODGE },
                null);
            stateMachine.RegisterTransition(State.FALL, State.ATTACK, 0.0f,
                new TriggerType[] { TriggerType.ATTACK },
                null);
        }

        #region State Delegate

        private void FallStateUpdate()
        {
            CheckRotation(targMoveInput.x);
        }

        #endregion
    }
}