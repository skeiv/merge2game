using merge2game.FormElements;
using merge2game.Interfaces;
using merge2game.Views;

namespace merge2game.Controllers;

public class ScoresController : IScoresController
{
    private readonly GameScreen _gameScreen;
    private readonly StartScreen _startScreen;
    private readonly Scores _scores;
    private int _completeOrders = 0;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="scores"></param>
    /// <param name="gameScreen"></param>
    public ScoresController(Scores scores, GameScreen gameScreen, StartScreen startScreen)
    {
        _scores = scores;
        _gameScreen = gameScreen;
        _startScreen = startScreen;
    }

    public void CompleteOrder(int level)
    {
        switch (level)
        {
            case 1: _scores.ScoresValue += 1; break;
            case 2: _scores.ScoresValue += 3; break;
            case 3: _scores.ScoresValue += 9; break;
            case 4: _scores.ScoresValue += 27; break;
        }

        _scores.Text = _scores.ScoresValue.ToString();
        _completeOrders += 1;
        if (_completeOrders == 10)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        MessageBox.Show($"Количество очков: {_scores.ScoresValue}", "Победа", MessageBoxButtons.OK);
        _gameScreen.Dispose();
        _startScreen.Show();
    }
}
