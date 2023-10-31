using merge2game.FormElements;

namespace merge2game.Interfaces;

/// <summary>
/// Контроллер для анимаций элементов
/// </summary>
public interface IAnimationController
{
    /// <summary>
    /// Анимация смены местами клеток
    /// </summary>
    /// <param name="targetCell">Целевая клетка</param>
    /// <param name="startCell">Начальная клетка</param>
    /// <param name="target">Элемент целевой клетки</param>
    /// <param name="start">Перетаскиваемый элемент</param>
    void AnimateSwap(Cell targetCell, Cell startCell, Element target, Element start);

    /// <summary>
    /// Анимация перемещения сгенерированного элемента в свободную клетку
    /// </summary>
    /// <param name="targetCell">Клетка</param>
    /// <param name="element">Элемент</param>
    void AnimateMoveToFreeCell(Cell targetCell, Element element);
}
