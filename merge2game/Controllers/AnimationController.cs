using merge2game.Interfaces;
using merge2game.FormElements;
using merge2game.Utils;

namespace merge2game.Controllers;

/// <inheritdoc />
public class AnimationController : IAnimationController
{
    /// <summary>
    /// .ctor
    /// </summary>
    public AnimationController()
    {
    }

    public void AnimateSwap(Cell targetCell, Cell startCell, Element target, Element start)
    {
        int animationSteps = 10;
        int currentStep = 0;
        var animationTimer = new System.Windows.Forms.Timer();
        start.Location = new Point(startCell.X * Consts.CELLSIZE, startCell.Y * Consts.CELLSIZE);

        int xDiff = targetCell.Location.X - startCell.Location.X;
        int yDiff = targetCell.Location.Y - startCell.Location.Y;
        int xStep = xDiff / animationSteps;
        int yStep = yDiff / animationSteps;


        animationTimer.Interval = 20; // Интервал времени между шагами анимации (в миллисекундах)
        animationTimer.Tick += (sender, e) =>
        {
            if (currentStep < animationSteps)
            {
                target.Left -= xStep;
                target.Top -= yStep;
                start.Left += xStep;
                start.Top += yStep;
                currentStep++;
            }
            else
            {
                animationTimer.Stop();
                currentStep = 0;
                SwapElements(targetCell, startCell, target, start);
            }
        };

        animationTimer.Start();
    }

    private void SwapElements(Cell targetCell, Cell startCell, Element target, Element start)
    {
        targetCell.Element = start;
        startCell.Element = target;

        startCell.Controls.Clear();
        startCell.Controls.Add(target);
        target.Location = new Point(0, 0);

        targetCell.Controls.Clear();
        targetCell.Controls.Add(start);
        start.Location = new Point(0, 0);

        target.X = start.X;
        target.Y = start.Y;
        start.X = targetCell.X;
        start.Y = targetCell.Y;
    }

    public void AnimateMoveToFreeCell(Cell targetCell, Element element)
    {
        int animationSteps = 10;
        int currentStep = 0;
        var animationTimer = new System.Windows.Forms.Timer();

        int xDiff = targetCell.Location.X - element.Location.X;
        int yDiff = targetCell.Location.Y - element.Location.Y;
        int xStep = xDiff / animationSteps;
        int yStep = yDiff / animationSteps;


        animationTimer.Interval = 20; // Интервал времени между шагами анимации (в миллисекундах)
        animationTimer.Tick += (sender, e) =>
        {
            if (currentStep < animationSteps)
            {
                element.Left += xStep;
                element.Top += yStep;
                currentStep++;
            }
            else
            {
                animationTimer.Stop();
                currentStep = 0;
                targetCell.Element = element;
                element.Parent = targetCell;
                element.X = targetCell.X;
                element.Y = targetCell.Y;
                element.Location = new Point(0, 0);
            }
        };

        animationTimer.Start();
    }
}


