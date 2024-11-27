using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private Vector2 aimDirection = Vector2.down;
    private Vector2 moveDirection = Vector2.down;
    private Vector2 dashDirection = Vector2.down;

    private bool isMoving = false;
    private bool isDashing = false;
    private Coroutine dashRoutine = null;

    private void Start()
    {
        var inputManager = InputManager.Instance;

        inputManager.OnMove.OnInput += OnMoveInput;
        inputManager.OnMove.OnInputStarted += OnMoveInputStart;
        inputManager.OnMove.OnInputCanceled += OnMoveInputCancel;

        inputManager.OnLook.OnInput += OnLookInput;

        inputManager.OnDash.OnInput += OnDashInput;
    }

    private void FixedUpdate()
    {
        if (!isDashing && isMoving)
            Move();
    }

    private void OnDestroy()
    {
        var inputManager = InputManager.Instance;
        if (inputManager == null) return;

        inputManager.OnMove.OnInput -= OnMoveInput;
        inputManager.OnMove.OnInputStarted -= OnMoveInputStart;
        inputManager.OnMove.OnInputCanceled -= OnMoveInputCancel;

        inputManager.OnLook.OnInput -= OnLookInput;

        inputManager.OnDash.OnInput -= OnDashInput;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, aimDirection * 2f, Color.cyan, 0);
    }

    #region Inputs Handlers Functions
    private void OnMoveInputStart(Vector2 moveInputDirection)
    {
        isMoving = true;
        OnMoveInput(moveInputDirection);
    }

    private void OnMoveInput(Vector2 moveInputDirection)
    {
        if (!isMoving) return;
        moveDirection = moveInputDirection;
    }

    private void OnMoveInputCancel()
    {
        isMoving = false;
    }

    private void OnLookInput(Vector2 lookInputDirection)
    {
        aimDirection = lookInputDirection.normalized;
    }

    private void OnDashInput()
    {
        if (isDashing) return;

        dashDirection = moveDirection;
        Dash();
    }
    #endregion

    private void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
    }

    private void Dash()
    {
        isDashing = true;
        dashRoutine = StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        float dashTimer = dashDuration;

        while (dashTimer > 0)
        {
            transform.Translate(dashSpeed * Time.deltaTime * dashDirection);
            dashTimer -= Time.deltaTime;
            yield return null;
        }

        EndDash();
    }

    private void EndDash()
    {
        isDashing = false;
        if (dashRoutine == null) return;
        StopCoroutine(dashRoutine);
        dashRoutine = null;
    }
}
