namespace merge2game.Views;

public partial class StartScreen : Form
{
    public StartScreen()
    {
        InitializeComponent();
    }

    private void PlayButton_Click(object sender, EventArgs e)
    {
        Hide();
        GameScreen gameScreen = new GameScreen(this);
        gameScreen.Show();
    }
}
