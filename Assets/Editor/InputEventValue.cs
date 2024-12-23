using System;
using UnityEngine.InputSystem;

public partial class InputEventsManagerSettings
{
    [Serializable]
    internal class InputEventValue
    {
        [Serializable] public enum InputType { Button, Value }
        [Serializable] public enum ControlType { Button, Vector2, Vector3, Quaternion, Float }

        public string Name;
        public InputType Type;
        public ControlType Control;
        public InputActionReference ActionRef;

        public InputEventValue(InputActionReference actionRef)
        {
            ActionRef = actionRef;
            Name = $"On{actionRef.action.name}";

            var types = GetTypesFromActionType(actionRef);
            Type = types.Item1;
            Control = types.Item2;
        }

        static (InputType, ControlType) GetTypesFromActionType(InputAction action)
        {
            return action.type switch
            {
                InputActionType.Button => (InputType.Button, ControlType.Button),
                InputActionType.Value => GetTypesFromActionControlType(action),
                InputActionType.PassThrough => GetTypesFromActionControlType(action),
                _ => throw new NotImplementedException()
            };
        }

        static (InputType, ControlType) GetTypesFromActionControlType(InputAction action)
        {
            return (action.expectedControlType) switch
            {
                "Button" => (InputType.Button, ControlType.Button),
                "Vector2" => (InputType.Value, ControlType.Vector2),
                "Vector3" => (InputType.Value, ControlType.Vector3),
                "Quaternion" => (InputType.Value, ControlType.Quaternion),
                _ => throw new NotImplementedException($"Action Control Type Not Implemented: {action.name} ; {action.expectedControlType}")
            };
        }
    }
}
