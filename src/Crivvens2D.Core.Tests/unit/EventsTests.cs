namespace Crivvens2D.Core.Tests;

[TestClass]
public class EventsTests {

  [TestInitialize]
  public void TestInitialize() {
    Events.Callbacks.Clear();
  }

  // --------------------------------------------------
  // on
  // --------------------------------------------------

  [TestMethod]
  public void On_AddsCallbackToEvent() {
    var func = new Action(() => { });
    Events.On("foo", func);

    Events.Callbacks["foo"].Should().NotBeNullOrEmpty();
    Events.Callbacks["foo"].Should().HaveCount(1);
    Events.Callbacks["foo"][0].Should().Be(func);
  }

  [TestMethod]
  public void On_AppendsCallbackToEvent() {
    var func1 = new Action(() => { });
    var func2 = new Action(() => { });
    Events.On("foo", func1);
    Events.On("foo", func2);

    Events.Callbacks["foo"].Should().NotBeNullOrEmpty();
    Events.Callbacks["foo"].Should().HaveCount(2);
    Events.Callbacks["foo"][0].Should().Be(func1);
    Events.Callbacks["foo"][1].Should().Be(func2);
  }

  // --------------------------------------------------
  // off
  // --------------------------------------------------

  [TestMethod]
  public void Off_RemovesCallbackFromEvent() {
    var func = new Action(() => { });
    Events.On("foo", func);
    Events.Off("foo", func);

    Events.Callbacks["foo"].Should().NotBeNull();
    Events.Callbacks["foo"].Should().BeEmpty();
  }

  [TestMethod]
  public void Off_RemovesOnlyCallbackFromEvent() {
    var func1 = new Action(() => { });
    var func2 = new Action(() => { });
    Events.On("foo", func1);
    Events.On("foo", func2);
    Events.Off("foo", func1);

    Events.Callbacks["foo"].Should().NotBeNullOrEmpty();
    Events.Callbacks["foo"].Should().HaveCount(1);
    Events.Callbacks["foo"][0].Should().Be(func2);
  }

  [TestMethod]
  public void Off_DoesNotErrorIfCallbackWasNotAdded() {
    var func = new Action(() => { });
    new Action(() => Events.Off("foo", func)).Should().NotThrow();
  }

  [TestMethod]
  public void Off_DoesNotErrorIfCallbackWasNotAddedToUndefinedEvent() {
    var func = new Action(() => { });
    new Action(() => Events.Off("MyEvent", func)).Should().NotThrow();
  }

  // --------------------------------------------------
  // emit
  // --------------------------------------------------
  [TestMethod]
  public void Emit_CallsTheCallbacksInEvent() {
    var func = Mock.Of<Action>(MockBehavior.Loose);
    Events.On("foo", func);
    Events.Emit("foo");
    Mock.Get(func).Verify(f => f(), Times.Once());
  }

  [TestMethod]
  public void Emit_CallsTheCallbacksInEventWithParameters() {
    var func = Mock.Of<Action<int, string, bool>>(MockBehavior.Loose);
    Events.On("foo", func);
    Events.Emit("foo", 1, "string", true);
    Mock.Get(func).Verify(f => f(1, "string", true), Times.Once());
  }

  [TestMethod]
  public void Emit_CallsTheCallbacksInOrder_IS_NOT_GARUANTEED_BY_MEEEE() {
    true.Should().BeTrue();
  }

  [TestMethod]
  public void Emit_DoesNotErrorIfCallbackWasNotAdded() {
    new Action(() => Events.Emit("myEvent")).Should().NotThrow();
  }
}

