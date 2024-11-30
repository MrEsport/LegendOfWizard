using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat<T>
{
    public delegate T Modifier(T baseValue);

    [SerializeField] private T baseValue;

    private List<Modifier> mods;

    public T BaseValue { get => baseValue; }

    public T Value { get => ProcessValue(); }

    public Stat()
    {
        baseValue = default;
        mods = new();
    }

    public void AddModifier(Modifier mod)
    {
        mods.Add(mod);
    }

    public void RemoveModifier(Modifier mod)
    {
        mods.Remove(mod);
    }

    public void Clear()
    {
        mods.Clear();
    }

    private T ProcessValue()
    {
        if (mods.Count == 0) return baseValue;

        T value = baseValue;
        foreach (var m in mods)
        {
            if (m == null)
            {
                Debug.LogError($"Null Reference StatModifier in Stat : {new NullReferenceException()}");
                continue;
            }

            value = m.Invoke(value);
        }
        return value;
    }
}
