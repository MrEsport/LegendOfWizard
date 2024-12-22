using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class InputEventsManagerSettings : ScriptableObject
{
    public const string k_InputManagerSettingsPath = "Assets/Editor/InputEventsSettings.asset";

    [SerializeField] private InputEventValue inputValue;
    [SerializeField] private InputActionAsset m_inputAsset;

    [SerializeField] private List<InputEventValue> m_inputEvents;

    internal static InputEventsManagerSettings GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<InputEventsManagerSettings>(k_InputManagerSettingsPath);
        if (settings == null)
        {
            settings = CreateInstance<InputEventsManagerSettings>();
            AssetDatabase.CreateAsset(settings, k_InputManagerSettingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }

    public void GenerateInputEvents()
    {
        m_inputEvents.Clear();

        InputEventValue e;
        foreach (var map in m_inputAsset.actionMaps)
        {
            foreach (var action in map)
            {
                //e = GetEventFromAction(action);
                e = new InputEventValue(action);
                if (e == null) continue;

                m_inputEvents.Add(e);
                Debug.Log($"Added {action.name} : {action.type} ; {action.expectedControlType}");
            }
        }
    }

    /*
    private InputEvent GetEventFromAction(InputAction action)
    {
        switch (action.type)
        {
            case InputActionType.Value:
            case InputActionType.PassThrough:
                var e = GetEventFromControlType(action, action.expectedControlType);
                if (e == null) break;
                return e;

            case InputActionType.Button:
                return GetButtonEvent(action);

            default:
                break;
        }

        Debug.LogWarning($"INVALID ACTION - {action.name} : {action.type} ; {action.expectedControlType} !");
        return null;
    }

    private InputEvent GetButtonEvent(InputAction action)
    {
        return new InputButtonEvent(GetActionReference(action));
    }

    private InputEvent GetEventFromControlType(InputAction action, string controlType)
    {
        return (controlType) switch
        {
            "Button" => GetButtonEvent(action),
            "Vector2" => new InputValueEvent<Vector2>(GetActionReference(action)),
            "Vector3" => new InputValueEvent<Vector3>(GetActionReference(action)),
            "Quaternion" => new InputValueEvent<Quaternion>(GetActionReference(action)),
            _ => null
        };
    }

    private InputActionReference GetActionReference(InputAction action)
    {
        InputActionReference actionRef = CreateInstance<InputActionReference>();
        actionRef.Set(action);
        return actionRef;
    }
     */
}
