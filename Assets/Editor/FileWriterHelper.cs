using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public partial class InputEventsManagerSettings
{
    const string INPUTMANAGERPROPERTIES_PATH = "/Objects/Inputs/InputManagerProperties.cs";

    const string PROPERTIES_AREA_START = "#region PROPERTIES_WRITE_AREA";
    const string PROPERTIES_AREA_END = "#endregion";

    const string INPUT_BUTTON_EVENT = "InputButtonEvent";
    const string INPUT_VALUE_EVENT = "InputValueEvent";

    public void GeneratePropertiesCode()
    {
        var filePath = $"{Application.dataPath}{INPUTMANAGERPROPERTIES_PATH}";

        var lines = File.ReadAllLines(filePath).ToList();

        int startIndex = lines.FindIndex(l => l.Contains(PROPERTIES_AREA_START)) + 1;
        int endIndex = lines.FindIndex(l => l.Contains(PROPERTIES_AREA_END));

        lines.RemoveRange(startIndex, endIndex - startIndex);

        string prop = "";
        foreach (var map in m_inputMapValues)
        {
            if (!map.Enabled) continue;
            foreach (var e in map.Value)
            {
                if (!e.Enabled) continue;
                prop = GetPropertyString(e.Value);
                lines.Insert(startIndex++, prop);
            }
        }

        File.WriteAllLines(filePath, lines);
        AssetDatabase.Refresh();
    }

    private string GetPropertyString(InputEventValue value) => $"\tpublic {GetInputEventString(value)} {value.Name};";

    private string GetInputEventString(InputEventValue value)
    {
        if (value.Type == InputEventValue.InputType.Button) return INPUT_BUTTON_EVENT;
        return $"{INPUT_VALUE_EVENT}<{value.Control}>";
    }
}
