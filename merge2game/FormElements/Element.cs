using merge2game.Utils;

namespace merge2game.FormElements;

/// <summary>
/// Игровой элемент
/// </summary>
public class Element : RoundedPanel
{
    /// <summary>
    /// Является ли элемент заказом
    /// </summary>
    public bool IsOrderElement { get; }

    /// <summary>
    /// Цвет
    /// </summary>
    public Colors ElementColor { get; private set; }

    /// <summary>
    /// Уровень
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Может ли генерировать элементы?
    /// </summary>
    public bool IsGenerate { get; set; }

    /// <summary>
    /// Координата x
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Координата y
    /// </summary>
    public int Y { get; set; } 

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="level">Уровень</param>
    /// <param name="color">Цвет</param>
    public Element(int x, int y, int level = 1, Colors color = Colors.Blue, bool isOrderElement = false)
    {
        X = x;
        Y = y;
        Level = level;
        ElementColor = color;
        IsOrderElement = isOrderElement;

        Location = new Point(0, 0);
        Size = new Size(58, 58);
        MinimumSize = new Size(58, 58);
        Cursor = Cursors.Hand;
        Margin = new Padding(1, 1, 1, 1);
        Text = $"{Level}";
        ForeColor = Color.White;
        AllowDrop = true;
        Font = new Font("Arial", 12, FontStyle.Regular);
        BackColor = ElementColor switch
        {
            Colors.Red => Color.Red,
            Colors.Blue => Color.Blue,
            Colors.Green => Color.Green,
            _ => Color.Gray
        };
    }
}
