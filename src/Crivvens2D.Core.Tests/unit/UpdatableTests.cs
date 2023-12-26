//import Updatable from '../../src/updatable.js';
//import Vector, { VectorClass } from '../../src/vector.js';
namespace Crivvens2D.Core.Tests;
// test-context:start
//let testContext = {
//  GAMEOBJECT_VELOCITY: true,
//  GAMEOBJECT_ACCELERATION: true,
//  GAMEOBJECT_TTL: true,
//  VECTOR_SCALE: true
//};
// test-context:end

// --------------------------------------------------
// updatable
// --------------------------------------------------
[TestClass]
public class UpdatableTests {


  // --------------------------------------------------
  // constructor
  // --------------------------------------------------
  // Nope.

  // --------------------------------------------------
  // init
  // --------------------------------------------------
  [TestMethod]
  public void New_HasDefaultPosition() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.Position.X.Should().Be(0);
    updatable.Position.Y.Should().Be(0);
  }

  [TestMethod]
  public void New_HasDefaultVelocity() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.Velocity.X.Should().Be(0);
    updatable.Velocity.Y.Should().Be(0);
  }

  [TestMethod]
  public void New_HasDefaultAcceleration() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.Acceleration.X.Should().Be(0);
    updatable.Acceleration.Y.Should().Be(0);
  }

  [TestMethod]
  public void New_HasDefaultTTL() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.TTL.Should().Be(int.MaxValue);
  }

  [TestMethod]
  public void New_SetsVelocity() {
    var updatable = new Mock<Updatable>(null!, new Vector(10, 20), null!, null!).Object;
    updatable.dx.Should().Be(10);
    updatable.dy.Should().Be(20);
  }

  [TestMethod]
  public void New_SetsAcceleration() {
    var updatable = new Mock<Updatable>(null!, null!, new Vector(10, 20), null!).Object;
    updatable.ddx.Should().Be(10);
    updatable.ddy.Should().Be(20);
  }

  [TestMethod]
  public void New_SetsTTL() {
    var updatable = new Mock<Updatable>(null!, null!, null!, 20).Object;
    updatable.TTL.Should().Be(20);
  }

  [TestMethod]
  public void New_SetsAnyProperty() {
    // Nope.
  }

  // --------------------------------------------------
  // velocity
  // --------------------------------------------------
  [TestMethod]
  public void Velocity_SetsDX() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.dx = 10;
    updatable.Velocity.X.Should().Be(10);
  }

  [TestMethod]
  public void Velocity_GetsDX() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.Velocity.X = 10;
    updatable.dx.Should().Be(10);
  }

  [TestMethod]
  public void Velocity_SetsDY() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.dy = 10;
    updatable.Velocity.Y.Should().Be(10);
  }

  [TestMethod]
  public void Velocity_GetsDY() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.Velocity.Y = 10;
    updatable.dy.Should().Be(10);
  }

  // --------------------------------------------------
  // acceleration
  // --------------------------------------------------

  [TestMethod]
  public void Acceleration_SetsDDX() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.ddx = 10;
    updatable.Acceleration.X.Should().Be(10);
  }

  [TestMethod]
  public void Acceleration_GetsDDX() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.Acceleration.X = 10;
    updatable.ddx.Should().Be(10);
  }

  [TestMethod]
  public void Acceleration_SetsDDY() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.ddy = 10;
    updatable.Acceleration.Y.Should().Be(10);
  }

  [TestMethod]
  public void Acceleration_GetsDDY() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.Acceleration.Y = 10;
    updatable.ddy.Should().Be(10);
  }

  // --------------------------------------------------
  // isAlive
  // --------------------------------------------------

  [TestMethod]
  public void IsAlive_IsTrueIfTTLAboveZero() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.TTL = 20;
    updatable.IsAlive.Should().BeTrue();
  }

  [TestMethod]
  public void IsAlive_IsFalseIfTTLLessThanZero() {
    var updatable = new Mock<Updatable>(null!, null!, null!, null!).Object;
    updatable.TTL = 0;
    updatable.IsAlive.Should().BeFalse();
    updatable.TTL = -20;
    updatable.IsAlive.Should().BeFalse();
  }

  // --------------------------------------------------
  // update
  // --------------------------------------------------
  [TestMethod]
  public void Update_WillCallAdvance() {
    var mock = new Mock<Updatable>(MockBehavior.Strict, null!, null!, null!, null!);
    mock.Setup(m => m.Advance(It.IsAny<double>()));
    mock.Object.Update(0);
    mock.Verify(u => u.Advance(It.IsAny<double>()), Times.Once);
  }

  [TestMethod]
  public void Update_WillPassDTToAdvance() {
    var mock = new Mock<Updatable>(MockBehavior.Strict, null!, null!, null!, null!);
    mock.Setup(m => m.Advance(It.IsAny<double>()));
    mock.Object.Update(1 / 60);
    mock.Verify(u => u.Advance(1 / 60), Times.Once);
  }

  // --------------------------------------------------
  // advance
  // --------------------------------------------------
  [TestMethod]
  public void Advance_WillAddAccelerationToTheVelocity() {
    var mock = new Mock<Updatable>(null!, null!, null!, null!);
    mock.CallBase = true;
    var updatable = mock.Object;
    updatable.Velocity = new Vector(5, 10);
    updatable.Acceleration = new Vector(15, 20);

    updatable.Advance(0);
    updatable.Velocity.X.Should().Be(20);
    updatable.Velocity.Y.Should().Be(30);
  }

  [TestMethod]
  public void Advance_WillUseDTToScaleTheAcceleration() {
    var mock = new Mock<Updatable>(null!, null!, null!, null!);
    mock.CallBase = true;
    var updatable = mock.Object;
    updatable.Velocity = new Vector(5, 10);
    updatable.Acceleration = new Vector(10, 20);

    updatable.Advance(0.5);
    updatable.Velocity.X.Should().Be(10);
    updatable.Velocity.Y.Should().Be(20);
  }

  [TestMethod]
  public void Advance_WillAddVelocityToThePosition() {
    var mock = new Mock<Updatable>(null!, null!, null!, null!);
    mock.CallBase = true;
    var updatable = mock.Object;
    updatable.Position = new Vector(5, 10);
    updatable.Velocity = new Vector(15, 20);

    updatable.Advance(0);
    updatable.Position.X.Should().Be(20);
    updatable.Position.Y.Should().Be(30);
  }

  [TestMethod]
  public void Advance_WillUseDTToScaleTheVelocity() {
    var mock = new Mock<Updatable>(null!, null!, null!, null!);
    mock.CallBase = true;
    var updatable = mock.Object;
    updatable.Position = new Vector(5, 10);
    updatable.Velocity = new Vector(10, 20);

    updatable.Advance(0.5);
    updatable.Position.X.Should().Be(10);
    updatable.Position.Y.Should().Be(20);
  }

  [TestMethod]
  public void Advance_WillUpdateTTL() {
    var mock = new Mock<Updatable>(null!, null!, null!, null!);
    mock.CallBase = true;
    var updatable = mock.Object;
    updatable.TTL = 10;

    updatable.Advance(0);
    updatable.TTL.Should().Be(9);
  }
}