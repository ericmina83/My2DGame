using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerHandler
{
    Animator animator;
    Dictionary<string, float> timers = new Dictionary<string, float>();

    public TriggerHandler(Animator animator)
    {
        this.animator = animator;
    }

    public void Update(float deltaTime)
    {
        var keys = new List<string>(timers.Keys); // copy keys list becuase the dicionary will change later

        foreach (var triggerId in keys)
        {
            timers[triggerId] -= deltaTime;
            if (timers[triggerId] < 0.0f)
            {
                animator.ResetTrigger(triggerId);
                timers.Remove(triggerId);
            }
        }
    }

    public void SetTrigger(string triggerId, float countdownTime)
    {
        animator.SetTrigger(triggerId);
        timers[triggerId] = countdownTime;
    }
}