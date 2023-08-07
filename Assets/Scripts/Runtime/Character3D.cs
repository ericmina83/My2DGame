using EricGames.Runtime.Characters;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character3D : Character
{
    private CharacterController characterController;

    [SerializeField]
    private float rotationSpeed;

    protected override float SpeedY => characterController.velocity.y;

    protected override void OnAwake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public override Quaternion CheckRotation(Quaternion currentRotation, Vector2 input)
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

    protected override void HandleAttacking()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandlePreparing()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleResetting()
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

    protected override void OnStart()
    {
        throw new System.NotImplementedException();
    }
}
