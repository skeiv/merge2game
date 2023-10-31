using merge2game.Utils;

namespace merge2game.FormElements;

public class Field : Panel
{
    public List<Cell> Cells { get; }

    public Field()
    {
        Cells = new List<Cell>();
    }

    public void InitializeCells()
    {
        // Пример создания клеток в Panel
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var cell = new Cell(i, j)
                {
                    Location = new Point(i * Consts.CELLSIZE, j * Consts.CELLSIZE)
                };
                cell.AllowDrop = true;
                Controls.Add(cell);
                Cells.Add(cell);
            }
        }
    }
}
