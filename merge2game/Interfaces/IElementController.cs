using merge2game.FormElements;

namespace merge2game.Interfaces;

/// <summary>
/// Контроллер для элементов
/// </summary>
public interface IElementController
{
    /// <summary>
    /// Элементы
    /// </summary>
    List<Element> Elements { get; }

    /// <summary>
    /// Инициализация элементов
    /// </summary>
    void InitializeElements();

    /// <summary>
    /// Инициализация элементов заказа
    /// </summary>
    void InitializeOrderElements();
}
