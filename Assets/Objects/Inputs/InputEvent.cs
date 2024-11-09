using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class InputEvent : IDisposable
{
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] protected UnityEvent OnInputStarted;
    [SerializeField] protected UnityEvent OnInput;
    [SerializeField] protected UnityEvent OnInputCanceled;

    public InputEvent()
    {
        OnInputStarted = new UnityEvent();
        OnInput = new UnityEvent();
        OnInputCanceled = new UnityEvent();
    }

    public virtual void Dispose()
    {
        if (inputAction != null)
        {
            inputAction.action.started -= OnActionStarted;
            inputAction.action.performed -= OnActionPerformed;
            inputAction.action.canceled -= OnActionCanceled;
        }

        OnInputStarted.RemoveAllListeners();
        OnInput.RemoveAllListeners();
        OnInputCanceled.RemoveAllListeners();
    }

    public void Init()
    {
        if (inputAction == null)
            throw new MissingReferenceException($"{nameof(inputAction)} reference is missing");

        inputAction.action.started += OnActionStarted;
        inputAction.action.performed += OnActionPerformed;
        inputAction.action.canceled += OnActionCanceled;
    }

    protected virtual void OnActionStarted(InputAction.CallbackContext input)
    {
        Debug.Log($"{input.action.name} Started");
        OnInputStarted?.Invoke();
    }

    protected virtual void OnActionPerformed(InputAction.CallbackContext input)
    {
        Debug.Log($"{input.action.name} Performed");
        OnInput?.Invoke();
    }

    protected virtual void OnActionCanceled(InputAction.CallbackContext input)
    {
        Debug.Log($"{input.action.name} Canceled");
        OnInputCanceled?.Invoke();
    }
}

[Serializable]
public class InputEvent<T> : IDisposable
{
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] protected UnityEvent<T> OnInputStarted;
    [SerializeField] protected UnityEvent<T> OnInput;
    [SerializeField] protected UnityEvent OnInputCanceled;

    public InputEvent()
    {
        OnInputStarted = new UnityEvent<T>();
        OnInput = new UnityEvent<T>();
        OnInputCanceled = new UnityEvent();
    }

    public virtual void Dispose()
    {
        if (inputAction != null)
        {
            inputAction.action.started -= OnActionStarted;
            inputAction.action.performed -= OnActionPerformed;
            inputAction.action.canceled -= OnActionCanceled;
        }

        OnInputStarted.RemoveAllListeners();
        OnInput.RemoveAllListeners();
        OnInputCanceled.RemoveAllListeners();
    }

    public void Init()
    {
        if (inputAction == null)
            throw new MissingReferenceException($"{nameof(inputAction)} reference is missing");

        inputAction.action.started += OnActionStarted;
        inputAction.action.performed += OnActionPerformed;
        inputAction.action.canceled += OnActionCanceled;
    }

    protected virtual void OnActionStarted(InputAction.CallbackContext input)
    {
        T value = (T)input.ReadValueAsObject();
        Debug.Log($"{input.action.name} Started : {value}");
        OnInputStarted?.Invoke(value);
    }

    protected virtual void OnActionPerformed(InputAction.CallbackContext input)
    {
        T value = (T)input.ReadValueAsObject();
        Debug.Log($"{input.action.name} Performed : {value}");
        OnInput?.Invoke(value);
    }

    protected virtual void OnActionCanceled(InputAction.CallbackContext input)
    {
        Debug.Log($"{input.action.name} Canceled");
        OnInputCanceled?.Invoke();
    }
}
