using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

class InputEventsManagerSettingsProvider : SettingsProvider
{
    class Styles
    {
        public static GUIContent inputAsset = new GUIContent("Input Asset Reference");
        public static GUIContent inputEvents = new GUIContent("Input Events");
        public static GUIContent generateEventButton = new GUIContent("Regenerate Input Events");
        public static GUIContent generatePropertiesButton = new GUIContent("Regenerate Properties Code");
    }

    private SerializedObject m_CustomSettings;

    private bool b_inputEventsDirty = false;

    const string k_SETTINGS_PATH = "Assets/Editor/InputEventsSettings.asset";

    public InputEventsManagerSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
        : base(path, scope) { }

    public static bool IsSettingsAvailable()
    {
        return File.Exists(k_SETTINGS_PATH);
    }

    // This function is called when the user clicks on the MyCustom element in the Settings window.
    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        m_CustomSettings = InputEventsManagerSettings.GetSerializedSettings();
    }

    public override void OnGUI(string searchContext)
    {
        var assetProp = m_CustomSettings.FindProperty("m_inputAsset");

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(assetProp, Styles.inputAsset);
        if (EditorGUI.EndChangeCheck())
        {
            m_CustomSettings.ApplyModifiedProperties();
            OnGUI(searchContext);
            return;
        }

        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button(Styles.generateEventButton))
            b_inputEventsDirty = true;

        var inputAsset = assetProp.objectReferenceValue as InputActionAsset;
        if (inputAsset == null)
        {
            EditorGUILayout.LabelField($"{Styles.inputAsset.text} is NULL");
        }
        else if (inputAsset.actionMaps.Count == 0)
        {
            EditorGUILayout.LabelField($"{Styles.inputAsset.text} Empty : No Actions Found");
        }
        else
        {
            /*
            for (int i = 0; i < inputAsset.actionMaps.Count; i++)
            {
                var map = inputAsset.actionMaps[i];
                EditorGUILayout.LabelField($"\n### Action Map : {map.name} ###", EditorStyles.boldLabel);

                foreach (var action in map.actions)
                {
                    EditorGUILayout.LabelField($"{action.name} : {action.type} ; {action.expectedControlType}");
                }
            }
            */
        }

        var inputEventsProp = m_CustomSettings.FindProperty("m_inputMapValues");
        EditorGUILayout.PropertyField(inputEventsProp, Styles.inputEvents);

        if (GUILayout.Button("[TEST] Get Action References"))
        {
            (m_CustomSettings.targetObject as InputEventsManagerSettings).GetActionReferencesFromAsset();
        }
        if (GUILayout.Button(Styles.generatePropertiesButton))
        {
            (m_CustomSettings.targetObject as InputEventsManagerSettings).GeneratePropertiesCode();
        }

        if (EditorApplication.isPlaying)
        {
            if (GUILayout.Button("[TEST] Get Field Infos"))
            {
                (m_CustomSettings.targetObject as InputEventsManagerSettings).GetFieldInfos();
            }
        }
        else
            EditorGUILayout.HelpBox("Application should be running to get FieldInfo", MessageType.Warning);

        if (EditorGUI.EndChangeCheck())
        {
            m_CustomSettings.ApplyModifiedProperties();
        }

        //------------------------------ Handling sensitive Logic after Draws ------------------------------
        if (b_inputEventsDirty && Event.current.type == EventType.Repaint)
        {
            (m_CustomSettings.targetObject as InputEventsManagerSettings).GenerateInputValuesFromAsset();
            m_CustomSettings.ApplyModifiedProperties();
            b_inputEventsDirty = false;
        }
    }

    // Register the SettingsProvider
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        // Settings Asset doesn't exist yet; no need to display anything in the Settings window.
        if (!IsSettingsAvailable()) return null;

        var provider = new InputEventsManagerSettingsProvider("Project/Input System Package/Input Events Manager", SettingsScope.Project);

        // Automatically extract all keywords from the Styles.
        provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
        return provider;
    }
}
