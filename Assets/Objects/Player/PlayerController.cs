using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private Vector2 aimDirection = Vector2.down;
    private Vector2 moveDirection = Vector2.down;
    private Vector2 dashDirection = Vector2.down;

    private bool isMoving = false;
    private bool isDashing = false;
    private bool canDash = true;
    private Coroutine dashRoutine = null;

    private List<Item> items = new();

    private void Start()
    {
        var inputManager = InputManager.Instance;

        inputManager.OnMove.OnInput += OnMoveInput;
        inputManager.OnMove.OnInputStarted += OnMoveInputStart;
        inputManager.OnMove.OnInputCanceled += OnMoveInputCancel;

        inputManager.OnLook.OnInput += OnLookInput;

        inputManager.OnDash.OnInput += OnDashInput;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.KeypadMinus)) return;
        if (items.Count == 0) return;

        int index = UnityEngine.Random.Range(0, items.Count);
        items[index].Unregister(playerStats);
        items.RemoveAt(index);
    }

    private void FixedUpdate()
    {
        if (!isDashing && isMoving)
            Move();
    }

    private void OnDestroy()
    {
        items.ForEach(i => i.Unregister(playerStats));

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
        if (!canDash) return;

        dashDirection = moveDirection;
        Dash();
    }
    #endregion

    private void Move()
    {
        transform.Translate(playerStats.MoveSpeed.Value * Time.deltaTime * moveDirection);
    }

    private void Dash()
    {
        isDashing = true;
        canDash = false;
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

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);

        EndDash();
    }

    private void EndDash()
    {
        isDashing = false;
        canDash = true;
        if (dashRoutine == null) return;
        StopCoroutine(dashRoutine);
        dashRoutine = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<ItemPickup>(out var pickup)) return;

        var item = pickup.Item;

        items.Add(item);
        item.Register(playerStats);
    }
}
