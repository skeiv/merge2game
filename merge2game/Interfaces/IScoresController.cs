namespace merge2game.Interfaces;

/// <summary>
/// Контроллер очков
/// </summary>
public interface IScoresController
{
    /// <summary>
    /// Выполнение заказа
    /// </summary>
    /// <param name="level">Уровень элемента выполненного заказа</param>
    void CompleteOrder(int level);
}
