using UnityEngine;

public interface IInventory<T> where T : IStockable
{
    public void Add(T toAdd);
    public void Take(T toTake);
}
