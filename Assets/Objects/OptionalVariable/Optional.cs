using System;
using UnityEngine;

[Serializable]
public class Optional<T>
{
    /// <summary>
    /// used by Inspector, displayed as Element Label
    /// </summary>
    [HideInInspector, SerializeField] protected string name;

    [SerializeField] private T m_value;
    [SerializeField] private bool m_enabled;

    public T Value { get => m_value; set => m_value = value; }
    public bool Enabled { get => m_enabled; set => m_enabled = value; }

    // Constructor  "Optional<float> F = new Optional<float>(3.5f);"
    public Optional(T value)
    {
        m_value = value;
        m_enabled = true;
    }

    // Permits Constructors like "Optional<float> F = 3.5f;"
    public static implicit operator Optional<T>(T value)
    {
        return new Optional<T>(value);
    }

    // Permits if statements shortcut  "if(optional)"
    public static implicit operator bool(Optional<T> optional)
    {
        return optional.m_enabled;
    }

    protected virtual string GetName { get => name; }
}
