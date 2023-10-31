using merge2game.Utils;

namespace merge2game.FormElements;

/// <summary>
/// Панель заказов
/// </summary>
public class OrderField : Panel
{
    public List<Cell> OrderCells = new List<Cell>();

    /// <summary>
    /// .ctor
    /// </summary>
    public OrderField() 
    { 
    }

    /// <summary>
    /// Инициализация ячеек
    /// </summary>
    public void InitialOrderCells()
    {
        for (int i = 0; i < 2; i++)
        {
            var cell = new Cell(i, 0)
            {
                Location = new Point(i * Consts.CELLSIZE, 0)
            };
            cell.AllowDrop = true;
            Controls.Add(cell);
            OrderCells.Add(cell);
        }
    }
}
