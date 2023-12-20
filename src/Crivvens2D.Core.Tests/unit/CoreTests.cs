namespace Crivvens2D.Core.Tests;

// --------------------------------------------------
// core
// --------------------------------------------------
[TestClass]
public class CoreTests {

  [TestInitialize]
  public void TestInitialize() {
    Events.Callbacks.Clear();
    // ensure no canvas exists since these tests set it up
    Core.Canvas = null;
  }

  // --------------------------------------------------
  // init
  // --------------------------------------------------

  // Nope. Not the needful.

  [TestMethod]
  public void Init_ThrowsErrorIfNoCanvas() {
    ICanvas canvas = Core.Canvas!;
    Action func = () => { Core.Init(canvas); };
    func.Should().Throw<ArgumentNullException>();
  }


  // it should set the canvas when passed no arguments test
  // Nope. It will just error

  // it should set the canvas when passed an id
  // Nope. Canvas is required.

  [TestMethod]
  public void Init_SetsTheCanvasWhenPassedACanvasElement() {
    var canvas = Mock.Of<ICanvas>();
    Core.Init(canvas);
    Core.Canvas.Should().Be(canvas);
  }

  // Nope. Not the needful.

  [TestMethod]
  public void Init_EmitsTheInitEvent() {
    var done = Mock.Of<Action>();
    Events.On("init", done);

    Core.Init(Mock.Of<ICanvas>());
    Mock.Get(done).Verify(d => d(), Times.Once);
  }

  [TestMethod]
  public void Init_ReturnsTheCanvas() {
    var canvas = Mock.Of<ICanvas>();
    var result = Core.Init(canvas);
    result.Should().Be(canvas);
  }

  //it should allow contextless option
  // Nope. Not the needful.

}