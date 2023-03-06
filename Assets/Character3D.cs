using System.Collections;
using System.Collections.Generic;
using EricGames.Core.Characters;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character3D : Character
{
    private CharacterController characterController;

    [SerializeField]
    private float rotationSpeed;

    protected override float speedY => characterController.velocity.y;

    public override Quaternion CheckRotation(Vector2 input)
    {
        Vector3 dir;

        // if (aiming && !running)
        //     dir = Camera.main.transform.forward;
        // else
        dir = Camera.main.transform.forward * input.y + Camera.main.transform.right * input.x;

        dir.y = 0.0f;

        var lookAtTarget = Quaternion.LookRotation(dir, transform.up);

        return Quaternion.Lerp(transform.rotation, lookAtTarget, rotationSpeed * Time.deltaTime);
    }

    public override void HandleAttacking()
    {
        throw new System.NotImplementedException();
    }

    public override void HandlePreparing()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleRestoring()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleMove(Vector2 input)
    {
        throw new System.NotImplementedException();
    }

    protected override bool CheckIsGrounded()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleApplyJumpForce(float jumpForce)
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleFall(Vector2 moveInput, Vector3 lookInput)
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleJump(Vector2 moveInput, Vector3 lookInput)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
