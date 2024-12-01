using UnityEngine;
using UnityEngine.Events;

public interface IStockable
{
    public string Name { get; set; }
    public Sprite Icon { get; set; }
    public string Description { get; set; }

    public event UnityAction<IStockable> OnTaken;

    public void Take();
}

public abstract class StockableObject : ScriptableObject, IStockable
{
    [Header("Stock Data")]
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;

    [SerializeField] private UnityEvent<IStockable> _onTaken = new();

    public string Name { get => _name; set => _name = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public string Description { get => _description; set => _description = value; }

    public event UnityAction<IStockable> OnTaken
    {
        add => _onTaken.AddListener(value);
        remove => _onTaken.RemoveListener(value);
    }

    public void Take()
    {
        _onTaken.Invoke(this);
    }
}
