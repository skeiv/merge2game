using merge2game.Interfaces;
using merge2game.Controllers;
using merge2game.FormElements;

namespace merge2game.Views;

public partial class GameScreen : Form
{
    private readonly IElementController _elementController;
    private readonly IScoresController _scoresController;

    /// <summary>
    /// .ctor
    /// </summary>
    public GameScreen(StartScreen startScreen)
    {
        InitializeComponent();
        GameField.InitializeCells();
        OrderField.InitialOrderCells();

        Scores.ScoresValue = 0;
        Scores.Text = Scores.ScoresValue.ToString();

        _scoresController = new ScoresController(Scores, this, startScreen);

        _elementController = new ElementController(OrderField, GameField, Scores, _scoresController);
        _elementController.InitializeElements();
        _elementController.InitializeOrderElements();
    }

    private void GameScreen_FormClosed(Object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }
}
