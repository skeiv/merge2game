using System.Drawing;
using System.Drawing.Drawing2D;

namespace merge2game.FormElements;

public class RoundedPanel : Panel
{
    public int CornerRadius { get; set; }

    public RoundedPanel()
    {
        CornerRadius = 10;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        GraphicsPath path = new GraphicsPath();
        path.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
        path.AddArc(Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
        path.AddArc(Width - CornerRadius, Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
        path.AddArc(0, Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
        Region = new Region(path);

        StringFormat stringFormat = new StringFormat();
        stringFormat.Alignment = StringAlignment.Center;
        stringFormat.LineAlignment = StringAlignment.Center;
        using (SolidBrush brush = new SolidBrush(ForeColor))
        {
            graphics.DrawString(Text, Font, brush, ClientRectangle, stringFormat);
        }
    }
}
