using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedButton : Button
{
    private int borderWidth = 5; 
    private Color borderColor = Color.Black; 

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(0, 0, Width - 1, Height - 1);

        Brush brush = new SolidBrush(BackColor);
        g.FillPath(brush, path);

        Pen pen = new Pen(borderColor, borderWidth);
        g.DrawPath(pen, path);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(0, 0, Width - 1, Height - 1);
        this.Region = new Region(path);
    }
}