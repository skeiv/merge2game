using merge2game.Interfaces;
using merge2game.FormElements;
using merge2game.Utils;

namespace merge2game.Controllers;

/// <inheritdoc />
public class ElementController : IElementController
{
    private readonly IAnimationController _animationController = new AnimationController();
    private readonly IScoresController _scoresController;
    private readonly Field _gameField;
    private readonly OrderField _orderField;
    private readonly Scores _scores;
    private Point _offset;

    public List<Element> Elements { get; }

    /// <summary>
    /// .ctor
    /// </summary>
    public ElementController(OrderField orderField, Field gameField, Scores scores, IScoresController scoresController)
    {
        _scoresController = scoresController;
        _scores = scores;
        _orderField = orderField;
        _gameField = gameField;
        Elements = new List<Element>();
    }

    public void InitializeElements()
    {
        var elementCount = 0;
        while (elementCount != 8)
        {
            var initialLevel = 1;
            var elementX = Random.Shared.Next(7);
            var elementY = Random.Shared.Next(7);
            var cell = _gameField.Cells.First(x => x.X == elementX && x.Y == elementY);
            if (cell.Element != null)
            {
                continue;
            }

            var element = new Element(elementX, elementY, initialLevel, Colors.Blue);
            element.MouseDown += Element_MouseDown;
            element.MouseMove += Element_MouseMove;
            element.MouseUp += Element_MouseUp;
            element.MouseDoubleClick += Element_DoubleClick;
            cell.Element = element;
            cell.Controls.Add(element);
            Elements.Add(element);
            elementCount++;
        }
    }

    public void InitializeOrderElements()
    {
        GenerateNewOrderElement(0, 0);
        GenerateNewOrderElement(1, 0);
    }

    private void Element_MouseDown(object sender, MouseEventArgs e)
    {
        if (sender is not Element)
        {
            return;
        }
        Element element = (Element)sender;

        if (e.Button == MouseButtons.Left && element.IsOrderElement)
        {
            CheckOrderComplete(element);
            return;
        }

        if (e.Button == MouseButtons.Left)
        {
            var parentCell = element.Parent;
            parentCell.Controls.Remove(element);
            element.Parent = _gameField;
            element.Location = new Point(element.X * Consts.CELLSIZE, element.Y * Consts.CELLSIZE);
            element.BringToFront();
            _offset = e.Location;
        }
    }

    private void Element_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && sender is Element)
        {
            Element element = (Element)sender;
            element.Left += e.X - _offset.X;
            element.Top += e.Y - _offset.Y;
        }
    }

    private void Element_MouseUp(object sender, MouseEventArgs e)
    {
        if (sender is not Element)
        {
            return;
        }

        Element element = (Element)sender;

        var startCell = _gameField.Cells.First(x => x.X == element.X && x.Y == element.Y);
        var targetCell = _gameField.Cells.FirstOrDefault(
            x => x.Location.X <= element.Location.X + Consts.CELLSIZE / 2
            && x.Location.X >= element.Location.X - Consts.CELLSIZE / 2
            && x.Location.Y <= element.Location.Y + Consts.CELLSIZE / 2
            && x.Location.Y >= element.Location.Y - Consts.CELLSIZE / 2);

        if (targetCell == null)
        {
            element.Parent = startCell;
            element.Location = new Point(0, 0);
            return;
        }

        if (targetCell.Element == null || targetCell.Element == element)
        {
            UpdatePropertiesOnCellClick(startCell, targetCell, new Point(0, 0), element);
            return;
        }

        if (targetCell.Element.ElementColor == element.ElementColor && targetCell.Element.Level == element.Level && element.Level < 4)
        {
            UpdatePropertiesOnMerge(startCell, targetCell, new Point(0, 0), element);
            return;
        }

        if (targetCell.Element.ElementColor != element.ElementColor || element.Level == 4 || targetCell.Element.Level != element.Level)
        {
            HandleTargetElementSwap(targetCell, startCell, element);
            return;
        }
    }

    private void Element_DoubleClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && sender is Element)
        {
            Element element = (Element)sender;
            GenerateNewElement(element);
        }
    }

    private void UpdatePropertiesOnCellClick(Cell startCell, Cell targetCell, Point point, Element element)
    {
        startCell.Element = null;
        element.Parent = targetCell;
        element.Location = point;
        element.X = targetCell.X;
        element.Y = targetCell.Y;
        targetCell.Element = element;
    }

    private void UpdatePropertiesOnMerge(Cell startCell, Cell targetCell, Point point, Element element)
    {
        startCell.Element = null;
        if (targetCell.Element != null)
        {
            Elements.Remove(targetCell.Element);
        }

        targetCell.Element?.Dispose();
        targetCell.Element = element;
        element.Parent = targetCell;
        element.Level = element.Level + 1;
        element.Text = $"{element.Level}";
        element.Location = point;
        element.X = targetCell.X;
        element.Y = targetCell.Y;
    }

    private void HandleTargetElementSwap(Cell targetCell, Cell startCell, Element element)
    {
        var targetElement = targetCell.Element;
        targetCell.Controls.Remove(targetElement);
        targetElement.Parent = _gameField;
        targetElement.Location = new Point(targetElement.X * Consts.CELLSIZE, targetElement.Y * Consts.CELLSIZE);
        targetElement.BringToFront();
        _animationController.AnimateSwap(targetCell, startCell, targetElement, element);
    }

    private void GenerateNewElement(Element generateElement)
    {
        if (generateElement.Level < 4)
        {
            return;
        }

        var freeCells = _gameField.Cells.Where(x => x.Element == null).ToList();
        if (freeCells.Count == 0)
        {
            return;
        }

        var random = new Random();
        var freeCell = freeCells[random.Next(freeCells.Count)];
        switch (generateElement.ElementColor)
        {
            case Colors.Red: GenerateForRedElement(generateElement, freeCell); break;
            case Colors.Blue: GenerateForBlueElement(generateElement, freeCell); break;
            case Colors.Green: GenerateForGreenElement(generateElement, freeCell); break;
        }
    }

    private void GenerateForRedElement(Element element, Cell cell)
    {
        GenerateRandomElements(element, cell, Colors.Blue, Colors.Green);
    }

    private void GenerateForBlueElement(Element element, Cell cell)
    {
        GenerateRandomElements(element, cell, Colors.Green, Colors.Red);
    }

    private void GenerateForGreenElement(Element element, Cell cell)
    {
        GenerateRandomElements(element, cell, Colors.Red, Colors.Blue);
    }

    private void GenerateRandomElements(Element element, Cell cell, Colors primaryColor, Colors secondaryColor)
    {
        var random = new Random();
        int randomNumber = random.Next(100);

        if (randomNumber < 80)
        {
            var newElement = GenerateNewElement(element, primaryColor);
            MoveNewElementToFreeCell(cell, newElement);

        }
        else
        {
            var newElement = GenerateNewElement(element, secondaryColor);
            MoveNewElementToFreeCell(cell, newElement);
        }
    }

    private Element GenerateNewElement(Element generateElement, Colors Color)
    {
        var newElement = new Element(generateElement.X, generateElement.Y, 1, Color);
        newElement.Parent = _gameField;
        newElement.Location = new Point(generateElement.Location.X, generateElement.Location.Y - 30);
        newElement.BringToFront();
        newElement.MouseDown += Element_MouseDown;
        newElement.MouseMove += Element_MouseMove;
        newElement.MouseUp += Element_MouseUp;
        newElement.MouseDoubleClick += Element_DoubleClick;
        Elements.Add(newElement);
        return newElement;
    }

    private void MoveNewElementToFreeCell(Cell cell, Element element)
    {
        _animationController.AnimateMoveToFreeCell(cell, element);
    }

    private void CheckOrderComplete(Element element)
    {
        var targetElement = Elements.LastOrDefault(x => x.Level == element.Level && x.ElementColor == element.ElementColor);
        if (targetElement == null)
        {
            return;
        }

        _scoresController.CompleteOrder(targetElement.Level);
        var x = element.X;
        var y = element.Y;
        element.Parent = null;
        targetElement.Parent = null;
        element.Dispose();
        Elements.Remove(targetElement);
        targetElement.Dispose();

        GenerateNewOrderElement(x, y);
    }

    private void GenerateNewOrderElement(int x, int y)
    {
        Random random = new Random();
        var colorInt = random.Next(3);
        var level = random.Next(4) + 1;
        var color = colorInt switch
        {
            0 => Colors.Red,
            1 => Colors.Blue,
            2 => Colors.Green,
        };
        var newOrderElement = new Element(x, y, level, color, true);
        newOrderElement.Parent = _orderField.OrderCells.First(cell => cell.X == x && cell.Y == y);
        newOrderElement.Location = new Point(0, 0);
        newOrderElement.BringToFront();
        newOrderElement.MouseDown += Element_MouseDown;
    }
}
