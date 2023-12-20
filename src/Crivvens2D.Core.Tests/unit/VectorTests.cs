//import Vector, { VectorClass } from '../../src/vector.js';
namespace Crivvens2D.Core.Tests;

[TestClass]
public class VectorTest {


  // test-context:start

  // Nope. Not the needful.

  // test-context:end

  // --------------------------------------------------
  // vector
  // --------------------------------------------------


  // --------------------------------------------------
  // init
  // --------------------------------------------------

  [TestMethod]
  public void Init_ShouldSetXAndY() {
    var vector = new Vector(10, 20);

    vector.X.Should().Be(10);
    vector.Y.Should().Be(20);
  }

  [TestMethod]
  public void Init_ShouldTakeAnObject() {
    var vector = new Vector(new Point(X: 10, Y: 20));
    vector.X.Should().Be(10);
    vector.Y.Should().Be(20);
  }

  // --------------------------------------------------
  // set
  // --------------------------------------------------
  [TestMethod]
  public void Set_ShouldSetXAndY() {
    var vector = new Vector(10, 20);
    vector.Set(new Point(X: 20, Y: 10));

    vector.X.Should().Be(20);
    vector.Y.Should().Be(10);
  }

  // --------------------------------------------------
  // add
  // --------------------------------------------------
  [TestMethod]
  public void Add_ShouldAddTwoVectors() {
    var vector1 = new Vector(10, 20);
    var vector2 = new Vector(5, 10);

    var vector = vector1.Add(vector2);

    vector.X.Should().Be(15);
    vector.Y.Should().Be(30);
  }

  [TestMethod]
  public void Add_ShouldNotModifyEitherVectors() {
    var vector1 = new Vector(10, 20);
    var vector2 = new Vector(5, 10);
    var vector = vector1.Add(vector2);

    vector.X.Should().Be(15);
    vector.Y.Should().Be(30);

    vector1.X.Should().Be(10);
    vector1.Y.Should().Be(20);
    vector2.X.Should().Be(5);
    vector2.Y.Should().Be(10);
  }

  // --------------------------------------------------
  // subtract
  // --------------------------------------------------
  [TestMethod]
  public void Subtract_ShouldSubtractOneVectorFromAnother() {
    var vector1 = new Vector(10, 20);
    var vector2 = new Vector(5, 10);

    var vector = vector1.Subtract(vector2);

    vector.X.Should().Be(5);
    vector.Y.Should().Be(10);
  }

  [TestMethod]
  public void Subtract_ShouldNotModifyEitherVectors() {
    var vector1 = new Vector(10, 20);
    var vector2 = new Vector(5, 10);

    var vector = vector1.Subtract(vector2);

    vector.X.Should().Be(5);
    vector.Y.Should().Be(10);

    vector1.X.Should().Be(10);
    vector1.Y.Should().Be(20);
    vector2.X.Should().Be(5);
    vector2.Y.Should().Be(10);
  }

  // --------------------------------------------------
  // scale
  // --------------------------------------------------
  [TestMethod]
  public void Scale_ShouldScaleAVectorByAScalar() {
    var vector1 = new Vector(5, 10);

    var vector = vector1.Scale(2);

    vector.X.Should().Be(10);
    vector.Y.Should().Be(20);
  }

  [TestMethod]
  public void Scale_ShouldNotModifyTheVector() {
    var vector1 = new Vector(5, 10);

    vector1.Scale(2);

    vector1.X.Should().Be(5);
    vector1.Y.Should().Be(10);
  }

  // --------------------------------------------------
  // normalize
  // --------------------------------------------------

  [TestMethod]
  public void Normalize_ShouldNormalizeAVector() {
    var vector1 = new Vector(4, 3);

    var normalize = vector1.Normalize();

    normalize.X.Should().Be(4d / 5);
    normalize.Y.Should().Be(3d / 5);
  }

  [TestMethod]
  public void Normalize_ShouldReturnZeroVectorForZeroVector() {
    var vector1 = new Vector(0, 0);

    var normalize = vector1.Normalize();

    normalize.X.Should().Be(0);
    normalize.Y.Should().Be(0);
  }

  // --------------------------------------------------
  // dot
  // --------------------------------------------------
  [TestMethod]
  public void Dot_ShouldCalculateDotProductOfTwoVectors() {
    var vector1 = new Vector(10, 20);
    var vector2 = new Vector(5, 10);

    var dot = vector1.Dot(vector2);

    dot.Should().Be(250);
  }

  // --------------------------------------------------
  // length
  // --------------------------------------------------
  [TestMethod]
  public void Length_ShouldCalculateLengthOfVector() {
    var vector1 = new Vector(4, 3);

    var length = vector1.Length();

    length.Should().Be(5);
  }

  // --------------------------------------------------
  // distance
  // --------------------------------------------------
  [TestMethod]
  public void Distance_ShouldCalculateDistanceBetweenTwoVectors() {
    var vector1 = new Vector(10, 20);
    var vector2 = new Vector(6, 17);

    var distance = vector1.Distance(vector2);

    distance.Should().Be(5);
  }

  // --------------------------------------------------
  // angle
  // --------------------------------------------------
  [TestMethod]
  public void Angle_ShouldCalculateAngleBetweenTwoVectors() {
    var vector1 = new Vector(4, 3);
    var vector2 = new Vector(3, 5);

    var angle = vector1.Angle(vector2);

    angle.Should().BeApproximately(0.39, .1);
  }

  // --------------------------------------------------
  // direction
  // --------------------------------------------------
  [TestMethod]
  public void Direction_ShouldCalculateAngleOfVector() {
    var vector1 = new Vector(0, 3);
    var vector2 = new Vector(-4, -4);

    var angle1 = vector1.Direction();
    var angle2 = vector2.Direction();

    angle1.Should().Be(Math.PI / 2);
    angle2.Should().Be(-Math.PI + Math.PI / 4);
  }

  // --------------------------------------------------
  // clamp
  // --------------------------------------------------
  [TestMethod]
  public void Clamp_ShouldClampVectorsXValue() {
    var vector = new Vector(10, 20);
    vector.Clamp(0, 10, 50, 75);

    vector.X = -10;

    vector.X.Should().Be(0);

    vector.X = 100;

    vector.X.Should().Be(50);
  }

  [TestMethod]
  public void Clamp_ShouldClampVectorsYValue() {
    var vector = new Vector(10, 20);
    vector.Clamp(0, 10, 50, 75);

    vector.Y = -10;

    vector.Y.Should().Be(10);

    vector.Y = 100;

    vector.Y.Should().Be(75);
  }

  [TestMethod]
  public void Clamp_ShouldNotClampVectorsXValue() {
    var vector = new Vector(10, 20);
    vector.Clamp(0, 10, 50, 75);

    vector.X = 20;

    vector.X.Should().Be(20);
  }

  [TestMethod]
  public void Clamp_ShouldNotClampVectorsYValue() {
    var vector = new Vector(10, 20);
    vector.Clamp(0, 10, 50, 75);

    vector.Y = 30;

    vector.Y.Should().Be(30);
  }

  [TestMethod]
  public void Clamp_ShouldNotClampVectorsXValueWhenNotClamped() {
    var vector = new Vector(10, 20);

    vector.X = -10;

    vector.X.Should().Be(-10);

    vector.X = 100;

    vector.X.Should().Be(100);
  }

  [TestMethod]
  public void Clamp_ShouldNotClampVectorsYValueWhenNotClamped() {
    var vector = new Vector(10, 20);

    vector.Y = -10;

    vector.Y.Should().Be(-10);

    vector.Y = 100;

    vector.Y.Should().Be(100);
  }

  [TestMethod]
  public void Clamp_ShouldPreserveClampSettingsWhenAddingVectors() {
    var vector = new Vector(10, 20);
    vector.Clamp(0, 10, 50, 75);

    var addme = new Vector(100, 100, null);

    var vec = vector.Add(addme);

    vec.X.Should().Be(50);
    vec.Y.Should().Be(75);
  }
}