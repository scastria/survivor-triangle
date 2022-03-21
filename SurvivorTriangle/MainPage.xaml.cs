using SkiaSharp;
using SkiaSharp.Views.Maui;
using SurvivorTriangle.Models;

namespace SurvivorTriangle;

using NodeGroup = List<Node>;

public partial class MainPage : ContentPage
{

    private const int MARGIN = 20;
    private const int TEXT_MARGIN = 100;

    private Graph _graph = null;
    private List<Triangle> _triangles = null;
    private int _triangleIndex = -1;

    public MainPage()
	{
		InitializeComponent();
        _graph = new Graph();
        _triangles = _graph.FindTriangles();
    }

    private void OnPaint(object sender, SKPaintSurfaceEventArgs e)
    {
        SKImageInfo info = e.Info;
        SKCanvas canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.White);
        (int Width, int Height) bounds = _graph.GetSize();
        canvas.Scale(Math.Min(info.Width / (float)(bounds.Width + MARGIN * 2), info.Height / (float)(bounds.Height + MARGIN * 2)));
        canvas.Translate(MARGIN, MARGIN);
        //Draw lines
        using (SKPaint sp = new SKPaint { Style = SKPaintStyle.Stroke, StrokeWidth = 2, Color = SKColors.Black.WithAlpha(128), StrokeCap = SKStrokeCap.Round, IsAntialias = true })
        {
            foreach (NodeGroup ng in _graph.AllNodeGroups)
            {
                Node nStart = ng[0];
                Node nEnd = ng[^1];
                canvas.DrawLine(nStart.X, nStart.Y, nEnd.X, nEnd.Y, sp);
            }
        }
        //Draw node labels
        using (SKPaint fp = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.Red, TextSize = 12, IsAntialias = true })
        {
            foreach (Node n in _graph.AllNodes)
            {
                SKRect textBounds = SKRect.Empty;
                fp.MeasureText(n.Name, ref textBounds);
                canvas.DrawText(n.Name, n.X - textBounds.MidX, n.Y - textBounds.MidY, fp);
            }
        }
        //Draw solution
        using (SKPaint fp = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.Black, TextSize = 20, IsAntialias = true })
        {
            Node nE = _graph.GetNode("E");
            canvas.DrawText("" + _triangles.Count + " Triangles", nE.X + TEXT_MARGIN, nE.Y, fp);
        }
        //Draw active triangle
        if (_triangleIndex != -1)
        {
            using (SKPaint fp = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.Green.WithAlpha(128), IsAntialias = true })
            {
                Triangle tri = _triangles[_triangleIndex];
                SKPath path = new SKPath();
                path.MoveTo(tri.Nodes[0].X, tri.Nodes[0].Y);
                path.LineTo(tri.Nodes[1].X, tri.Nodes[1].Y);
                path.LineTo(tri.Nodes[2].X, tri.Nodes[2].Y);
                path.Close();
                canvas.DrawPath(path, fp);
            }
            using (SKPaint fp = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.Black, TextSize = 18, IsAntialias = true })
            {
                Node nJ = _graph.GetNode("J");
                canvas.DrawText("(" + (_triangleIndex + 1) + "/" + _triangles.Count + ")", nJ.X + TEXT_MARGIN, nJ.Y, fp);
            }
        }
    }

    private void OnPrevClicked(object sender, EventArgs args)
    {
        if (_triangleIndex == -1)
            return;
        _triangleIndex--;
        canvasV.InvalidateSurface();
    }

    private void OnNextClicked(object sender, EventArgs args)
    {
        if (_triangleIndex == (_triangles.Count - 1))
            return;
        _triangleIndex++;
        canvasV.InvalidateSurface();
    }
}

