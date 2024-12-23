using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public abstract class InputEvent : IDisposable
{
    [SerializeField] protected InputActionReference inputAction;
    private InputAction action;

    public abstract void Dispose();
    protected abstract void Init();

    public void SetActionRef(InputActionReference actionRef)
    {
        inputAction = actionRef;
        action = actionRef.action;
        Init();
    }
}

[Serializable]
public class InputButtonEvent : InputEvent
{
    //[SerializeField] private InputActionReference inputAction;
    [SerializeField] protected UnityEvent onInputStarted;
    [SerializeField] protected UnityEvent onInput;
    [SerializeField] protected UnityEvent onInputCanceled;

    public event UnityAction OnInputStarted
    {
        add => onInputStarted.AddListener(value);
        remove => onInputStarted.RemoveListener(value);
    }

    public event UnityAction OnInput
    {
        add => onInput.AddListener(value);
        remove => onInput.RemoveListener(value);
    }

    public event UnityAction OnInputCanceled
    {
        add => onInputCanceled.AddListener(value);
        remove => onInputCanceled.RemoveListener(value);
    }

    public InputButtonEvent()
    {
        onInputStarted = new UnityEvent();
        onInput = new UnityEvent();
        onInputCanceled = new UnityEvent();
    }

    public InputButtonEvent(InputActionReference actionRef) : this()
    {
        SetActionRef(actionRef);
    }

    public override void Dispose()
    {
        if (inputAction != null)
        {
            inputAction.action.started -= OnActionStarted;
            inputAction.action.performed -= OnActionPerformed;
            inputAction.action.canceled -= OnActionCanceled;
        }

        onInputStarted.RemoveAllListeners();
        onInput.RemoveAllListeners();
        onInputCanceled.RemoveAllListeners();
    }

    protected override void Init()
    {
        if (inputAction == null)
            throw new MissingReferenceException($"{nameof(inputAction)} reference is missing");

        inputAction.action.started += OnActionStarted;
        inputAction.action.performed += OnActionPerformed;
        inputAction.action.canceled += OnActionCanceled;
    }

    protected virtual void OnActionStarted(InputAction.CallbackContext input)
    {
        onInputStarted?.Invoke();
    }

    protected virtual void OnActionPerformed(InputAction.CallbackContext input)
    {
        onInput?.Invoke();
    }

    protected virtual void OnActionCanceled(InputAction.CallbackContext input)
    {
        onInputCanceled?.Invoke();
    }
}

[Serializable]
public class InputValueEvent<T> : InputEvent
{
    //[SerializeField] private InputActionReference inputAction;
    [SerializeField] protected UnityEvent<T> onInputStarted;
    [SerializeField] protected UnityEvent<T> onInput;
    [SerializeField] protected UnityEvent onInputCanceled;

    public event UnityAction<T> OnInputStarted
    {
        add => onInputStarted.AddListener(value);
        remove => onInputStarted.RemoveListener(value);
    }

    public event UnityAction<T> OnInput
    {
        add => onInput.AddListener(value);
        remove => onInput.RemoveListener(value);
    }

    public event UnityAction OnInputCanceled
    {
        add => onInputCanceled.AddListener(value);
        remove => onInputCanceled.RemoveListener(value);
    }

    public InputValueEvent()
    {
        onInputStarted = new UnityEvent<T>();
        onInput = new UnityEvent<T>();
        onInputCanceled = new UnityEvent();
    }

    public InputValueEvent(InputActionReference actionRef) : this()
    {
        SetActionRef(actionRef);
    }

    public override void Dispose()
    {
        if (inputAction != null)
        {
            inputAction.action.started -= OnActionStarted;
            inputAction.action.performed -= OnActionPerformed;
            inputAction.action.canceled -= OnActionCanceled;
        }

        onInputStarted.RemoveAllListeners();
        onInput.RemoveAllListeners();
        onInputCanceled.RemoveAllListeners();
    }

    protected override void Init()
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
        onInputStarted?.Invoke(value);
    }

    protected virtual void OnActionPerformed(InputAction.CallbackContext input)
    {
        T value = (T)input.ReadValueAsObject();
        onInput?.Invoke(value);
    }

    protected virtual void OnActionCanceled(InputAction.CallbackContext input)
    {
        onInputCanceled?.Invoke();
    }
}
