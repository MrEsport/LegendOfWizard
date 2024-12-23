using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class InputEventsManagerSettings : ScriptableObject
{
    public const string k_InputManagerSettingsPath = "Assets/Editor/InputEventsSettings.asset";

    [SerializeField] private InputActionAsset m_inputAsset;
    [SerializeField] private List<InputEventValueMap> m_inputMapValues;
    [SerializeField] private InputActionReference[] m_inputActionReferences = new InputActionReference[0];

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

    public void GetActionReferencesFromAsset()
    {
        m_inputActionReferences = m_inputAsset.GetAllInputActionReferences();
    }

    public void GenerateInputValuesFromAsset()
    {
        m_inputMapValues.Clear();

        InputEventValueMap mapValues;
        InputActionReference inputRef;
        Optional<InputEventValue> e;
        foreach (var map in m_inputAsset.actionMaps)
        {
            mapValues = new InputEventValueMap(map.name);
            foreach (var action in map)
            {
                inputRef = m_inputActionReferences.First(r => r.action == action);
                e = new InputEventValue(inputRef);
                if (e.Value == null) continue;

                mapValues.Value.Add(e);
            }
            m_inputMapValues.Add(mapValues);
        }
    }
}
