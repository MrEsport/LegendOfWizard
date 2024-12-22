using System;
using System.Collections.Generic;

public partial class InputEventsManagerSettings
{
    [Serializable]
    internal class InputEventValueMap : Optional<List<Optional<InputEventValue>>>
    {
        public InputEventValueMap(string name) : this(name, new()) { }

        public InputEventValueMap(string name, List<Optional<InputEventValue>> value) : base(value)
        {
            this.name = name;
            Enabled = true;
        }
    }
}
