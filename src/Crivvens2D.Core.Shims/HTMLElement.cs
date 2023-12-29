namespace Crivvens2D.Core;

//https://developer.mozilla.org/en-US/docs/Web/API/HTMLCanvasElement
//https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D
public interface ICanvas {
}

public interface ICanvasRenderingContext2D {
  void FillRect(double x, double y, double width, double height);
  void ClearRect(double x, double y, double width, double height);
  void StrokeRect(double x, double y, double width, double height);
  void BeginPath();
  void MoveTo(double x, double y);
  void LineTo(double x, double y);
  void ClosePath();
  void Stroke();
  void Fill();
  void Arc(double x, double y, double radius, double startAngle, double endAngle, bool anticlockwise = false);
  void ArcTo(double x1, double y1, double x2, double y2, double radius);
  void FillText(string text, double x, double y, double maxWidth = double.MaxValue);
  void StrokeText(string text, double x, double y, double maxWidth = double.MaxValue);
  void MeasureText(string text);
  void Scale(double x, double y);
  void Rotate(double angle);
  void Translate(double x, double y);
  void Transform(double a, double b, double c, double d, double e, double f);
  void Save();
}

public interface IPoint {
  double X { get; }
  double Y { get; }
}

public record Point(double X, double Y) : IPoint;