using merge2game.Utils;

namespace merge2game.FormElements;

/// <summary>
/// Ячейка
/// </summary>
public class Cell : Panel
{
    /// <summary>
    /// Координата x
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Координата y
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Элемент, содержащийся в ячейке
    /// </summary>
    public Element? Element { get; set; }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        BorderStyle = BorderStyle.FixedSingle;
        Size = new Size(Consts.CELLSIZE, Consts.CELLSIZE);
        MaximumSize = new Size(Consts.CELLSIZE, Consts.CELLSIZE);
        Padding = new Padding(0, 0, 0, 0);
        Margin = new Padding(0, 0, 0, 0);
    }
}
