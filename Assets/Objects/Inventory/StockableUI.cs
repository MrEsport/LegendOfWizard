using UnityEngine;
using UnityEngine.UI;

public class StockableUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public IStockable Stockable;

    public void SetStockUI(IStockable stock)
    {
        Stockable = stock;
        iconImage.sprite = stock.Icon;
    }

    public void TakeStockable()
    {
        Stockable.Take();
    }
}
