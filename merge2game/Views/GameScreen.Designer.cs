using merge2game.FormElements;

namespace merge2game.Views
{
    partial class GameScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GameField = merge2game.FormElements.Field.GetInstance();
            
            // 
            // GameField
            // 
            this.GameField.Location = new System.Drawing.Point(12, 12);
            this.GameField.Name = "GameField";
            this.GameField.Size = new System.Drawing.Size(480, 480);
            this.GameField.TabIndex = 0;
            this.GameField.BorderStyle = BorderStyle.FixedSingle;

            this.OrderField = new OrderField();

            //
            // OrderField
            //
            this.OrderField.Location = new System.Drawing.Point(504, 432);
            this.OrderField.Name = "OrderField";
            this.OrderField.Size = new System.Drawing.Size(120, 60);
            this.OrderField.TabIndex = 0;
            this.OrderField.BorderStyle = BorderStyle.FixedSingle;

            this.OrderLabel = new System.Windows.Forms.Label();

            //
            // OrderLabel
            //
            this.OrderLabel.Location = new System.Drawing.Point(504, 402);
            this.OrderLabel.Name = "OrderLabel";
            this.OrderLabel.Text = "Заказы: ";

            this.ScoreLabel = new System.Windows.Forms.Label();
            //
            // ScoreLabel
            //
            this.ScoreLabel.Location = new System.Drawing.Point(504, 12);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Text = "Очки: ";

            this.Scores = new Scores();
            //
            // ScoreLabel
            //
            this.Scores.Location = new System.Drawing.Point(504, 42);
            this.Scores.Name = "Scores";

            this.SuspendLayout();
            // 
            // GameScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.OrderLabel);
            this.Controls.Add(this.GameField);
            this.Controls.Add(this.OrderField);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.Scores);
            this.DesktopLocation = new Point(0, 0);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.FormClosed += GameScreen_FormClosed;
            this.Name = "GameScreen";
            this.Text = "GameScreen";
            this.ResumeLayout(false);

        }

        private void GameScreen_FormClosed1(object sender, FormClosedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label OrderLabel;
        private Label ScoreLabel;
        private Scores Scores;
        private OrderField OrderField;
        private Field GameField;
    }
}