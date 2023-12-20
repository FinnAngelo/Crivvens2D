//import * as helpers from '../../src/helpers.js';
//import Sprite from '../../src/sprite.js';
//import TileEngine from '../../src/tileEngine.js';
namespace Crivvens2D.Core.Tests;
// --------------------------------------------------
// helpers
// --------------------------------------------------
[TestClass]
public class HelpersTests {

  [TestMethod]
  public void Helpers_ExportsApi() {
    nameof(Helpers.DegToRad).Should().NotBeNullOrEmpty();
    nameof(Helpers.RadToDeg).Should().NotBeNullOrEmpty();
    nameof(Helpers.AngleToTarget).Should().NotBeNullOrEmpty();
    nameof(Helpers.RotatePoint).Should().NotBeNullOrEmpty();
    nameof(Helpers.MovePoint).Should().NotBeNullOrEmpty();
    nameof(Helpers.RandInt).Should().NotBeNullOrEmpty();
    nameof(Helpers.Lerp).Should().NotBeNullOrEmpty();
    nameof(Helpers.InverseLerp).Should().NotBeNullOrEmpty();
    nameof(Helpers.Clamp).Should().NotBeNullOrEmpty();
    //nameof(Helpers.GetStoreItem).Should().NotBeNullOrEmpty();
    //nameof(Helpers.SetStoreItem).Should().NotBeNullOrEmpty();
    //nameof(Helpers.Collides).Should().NotBeNullOrEmpty();
    //nameof(Helpers.GetWorldRect).Should().NotBeNullOrEmpty();
    //nameof(Helpers.DepthSort).Should().NotBeNullOrEmpty();
  }

  // --------------------------------------------------
  // degToRad
  // --------------------------------------------------
  [TestMethod]
  public void DegToRad_ConvertsDegreesToRadians() {
    Helpers.DegToRad(22.35).Should().BeApproximately(0.39, 0.01);
  }

  // --------------------------------------------------
  // radToDeg
  // --------------------------------------------------
  [TestMethod]
  public void RadToDeg_ConvertsRadiansToDegrees() {
    Helpers.RadToDeg(0.39).Should().BeApproximately(22.35, 0.01);
  }

  // --------------------------------------------------
  // angleToTarget
  // --------------------------------------------------
  [TestMethod]
  public void AngleToTarget_ReturnsTheAngleToTheTarget() {
    var source = new Point(300, 300);
    var target = new Point(100, 100);
    Helpers.AngleToTarget(source, target).Should().BeApproximately(-Math.PI * 3 / 4, 0.01);
    Helpers.AngleToTarget(target, source).Should().BeApproximately(Math.PI / 4, 0.01);
  }

  // --------------------------------------------------
  // rotatePoint
  // --------------------------------------------------
  [TestMethod]
  public void RotatePoint_ReturnsTheNewXAndYAfterRotation() {
    var point = new Point(300, 300);
    var angle = Helpers.DegToRad(35);
    var newPoint = Helpers.RotatePoint(point, angle);
    newPoint.X.Should().BeApproximately(73.67, 0.01);
    newPoint.Y.Should().BeApproximately(417.82, 0.01);
  }

  // --------------------------------------------------
  // movePoint
  // --------------------------------------------------
  [TestMethod]
  public void MovePoint_ReturnsTheNewXAndYAfterMove() {
    var point = new Point(300, 300);
    var newPoint = Helpers.MovePoint(point, -Math.PI * 3 / 4, 141.421);
    newPoint.X.Should().BeApproximately(200, 0.1);
    newPoint.Y.Should().BeApproximately(200, 0.1);

    newPoint = Helpers.MovePoint(point, Math.PI / 4, 141.421);
    newPoint.X.Should().BeApproximately(400, 0.1);
    newPoint.Y.Should().BeApproximately(400, 0.1);

    newPoint = Helpers.MovePoint(point, Math.PI, 100);
    newPoint.X.Should().BeApproximately(200, 0.1);
    newPoint.Y.Should().BeApproximately(300, 0.1);
  }

  // --------------------------------------------------
  // randInt
  // --------------------------------------------------
  [TestMethod]
  public void RandInt_ReturnsRandomIntegerBetweenRange() {
    var rand = new Random(1);
    Helpers.RandInt(2, 10, rand).Should().Be(3);
  }

  // --------------------------------------------------
  // seedRand
  // --------------------------------------------------
  [TestMethod]
  public void SeedRand_SeedARandomNumberGenerator() {
    var rand = Helpers.SeedRand("kontra");
    rand.NextDouble().Should().BeApproximately(0.49865471920867205, 0.01);
    for (var i = 0; i < 20; i++) {
      _ = rand.NextDouble();
    }
    rand.NextDouble().Should().BeApproximately(0.06069048683191207, 0.01);
  }

  // --------------------------------------------------
  // lerp
  // --------------------------------------------------
  [TestMethod]
  public void Lerp_CalculatesTheLinearInterpolation() {
    Helpers.Lerp(10, 20, 0.5).Should().Be(15);
  }

  [TestMethod]
  public void Lerp_HandlesNegativeNumbers() {
    Helpers.Lerp(-10, 20, 0.5).Should().Be(5);
  }

  [TestMethod]
  public void Lerp_HandlesPercentagesGreaterThan1() {
    Helpers.Lerp(10, 20, 2).Should().Be(30);
  }

  [TestMethod]
  public void Lerp_HandlesNegativePercentages() {
    Helpers.Lerp(10, 20, -1).Should().Be(0);
  }

  // --------------------------------------------------
  // inverseLerp
  // --------------------------------------------------
  [TestMethod]
  public void InverseLerp_CalculatesTheInverseLinearInterpolation() {
    Helpers.InverseLerp(10, 20, 15).Should().Be(0.5);
  }

  [TestMethod]
  public void InverseLerp_HandlesNegativeNumbers() {
    Helpers.InverseLerp(-10, 20, 5).Should().Be(0.5);
  }

  [TestMethod]
  public void InverseLerp_HandlesPercentagesGreaterThan1() {
    Helpers.InverseLerp(10, 20, 30).Should().Be(2);
  }

  [TestMethod]
  public void InverseLerp_HandlesNegativePercentages() {
    Helpers.InverseLerp(10, 20, 0).Should().Be(-1);
  }

  // --------------------------------------------------
  // clamp
  // --------------------------------------------------
  [TestMethod]
  public void Clamp_ClampsTheValueWhenBelowMin() {
    Helpers.Clamp(10, 20, 5).Should().Be(10);
  }

  [TestMethod]
  public void Clamp_ClampsTheValueWhenAboveMax() {
    Helpers.Clamp(10, 20, 30).Should().Be(20);
  }

  [TestMethod]
  public void Clamp_DoesNotClampTheValueWhenBetweenMinAndMax() {
    Helpers.Clamp(10, 20, 15).Should().Be(15);
  }

  // --------------------------------------------------
  // store
  // --------------------------------------------------
  //describe('store', () => {
  //  it('should be able to save all data types to local storage', () => {
  //    localStorage.clear();

  //    var fn = function () {
  //      helpers.setStoreItem('boolean', true);
  //      helpers.setStoreItem('null', null);
  //      helpers.setStoreItem('undefined', undefined);
  //      helpers.setStoreItem('number', 1);
  //      helpers.setStoreItem('string', 'hello');
  //      helpers.setStoreItem('object', { foo: 'bar' });
  //      helpers.setStoreItem('array', [1, 2]);
  //    };

  //    expect(fn).to.not.throw(Error);
  //  });

  //  it('should be able to read all data types out of local storage', () => {
  //    expect(helpers.getStoreItem('boolean')).to.equal(true);
  //    expect(helpers.getStoreItem('number')).to.equal(1);
  //    expect(helpers.getStoreItem('string')).to.equal('hello');
  //    expect(helpers.getStoreItem('object')).to.deep.equal({
  //      foo: 'bar'
  //    });
  //    expect(helpers.getStoreItem('array')).to.deep.equal([1, 2]);
  //  });

  //  it('should remove a key from local storage using the set function when passed undefined', () => {
  //    helpers.setStoreItem('number', undefined);

  //    expect(helpers.getStoreItem('number')).to.not.be.true;
  //  });
  //});

  // --------------------------------------------------
  // collides
  // --------------------------------------------------
  //describe('collides', () => {
  //  it('should correctly detect collision between two objects', () => {
  //    let sprite1 = Sprite({
  //      x: 10,
  //      y: 20,
  //      width: 10,
  //      height: 20
  //    });

  //    let sprite2 = Sprite({
  //      x: 19,
  //      y: 39,
  //      width: 10,
  //      height: 20
  //    });

  //    expect(helpers.collides(sprite1, sprite2)).to.be.true;

  //    sprite2.x = 10;
  //    sprite2.y = 20;

  //    expect(helpers.collides(sprite1, sprite2)).to.be.true;

  //    sprite2.x = 1;
  //    sprite2.y = 1;

  //    expect(helpers.collides(sprite1, sprite2)).to.be.true;

  //    sprite2.x = 20;
  //    sprite2.y = 40;

  //    expect(helpers.collides(sprite1, sprite2)).to.be.false;

  //    sprite2.x = 0;
  //    sprite2.y = 0;

  //    expect(helpers.collides(sprite1, sprite2)).to.be.false;
  //  });

  //  it('should take into account sprite.anchor', () => {
  //    let sprite1 = Sprite({
  //      x: 10,
  //      y: 20,
  //      width: 10,
  //      height: 20,
  //      anchor: { x: 0.5, y: 0.5 }
  //    });

  //    let sprite2 = Sprite({
  //      x: 10,
  //      y: 20,
  //      width: 10,
  //      height: 20
  //    });

  //    expect(helpers.collides(sprite1, sprite2)).to.be.true;

  //    sprite1.anchor = { x: 1, y: 0 };

  //    expect(helpers.collides(sprite1, sprite2)).to.be.false;

  //    sprite2.anchor = { x: 1, y: 0 };

  //    expect(helpers.collides(sprite1, sprite2)).to.be.true;
  //  });

  //  it('should take into account sprite.scale', () => {
  //    let sprite1 = Sprite({
  //      x: 5,
  //      y: 20,
  //      width: 10,
  //      height: 20,
  //      scaleX: 1,
  //      scaleY: 1
  //    });

  //    let sprite2 = Sprite({
  //      x: 10,
  //      y: 20,
  //      width: 10,
  //      height: 20
  //    });

  //    expect(helpers.collides(sprite1, sprite2)).to.be.true;

  //    sprite1.scaleX = 0.5;
  //    sprite1.scaleY = 0.5;

  //    expect(helpers.collides(sprite1, sprite2)).to.be.false;

  //    sprite1.scaleX = 2;
  //    sprite1.scaleY = 2;

  //    expect(helpers.collides(sprite1, sprite2)).to.be.true;
  //  });

  //  it('should work for non-sprite objects', () => {
  //    let sprite1 = Sprite({
  //      x: 10,
  //      y: 20,
  //      width: 10,
  //      height: 20
  //    });

  //    let obj = {
  //      x: 10,
  //      y: 20,
  //      width: 10,
  //      height: 20
  //    };

  //    expect(helpers.collides(sprite1, obj)).to.be.true;
  //    expect(helpers.collides(obj, sprite1)).to.be.true;
  //  });
  //});

  // --------------------------------------------------
  // getWorldRect
  // --------------------------------------------------
  //describe('getWorldRect', () => {
  //  it('should return world x, y, width, and height', () => {
  //    let sprite = Sprite({
  //      x: 40,
  //      y: 40,
  //      width: 10,
  //      height: 10
  //    });
  //    let rect = helpers.getWorldRect(sprite);

  //    expect(rect.x).to.equal(40);
  //    expect(rect.y).to.equal(40);
  //    expect(rect.width).to.equal(10);
  //    expect(rect.height).to.equal(10);
  //  });

  //  it('should take into account negative scale', () => {
  //    let sprite = Sprite({
  //      x: 40,
  //      y: 40,
  //      width: 10,
  //      height: 20,
  //      scaleX: -2,
  //      scaleY: -2
  //    });
  //    let rect = helpers.getWorldRect(sprite);

  //    expect(rect.x).to.equal(20);
  //    expect(rect.y).to.equal(0);
  //    expect(rect.width).to.equal(20);
  //    expect(rect.height).to.equal(40);
  //  });

  //  it('should take into account anchor', () => {
  //    let sprite = Sprite({
  //      x: 40,
  //      y: 40,
  //      width: 10,
  //      height: 10,
  //      anchor: { x: 0.5, y: 0.5 }
  //    });
  //    let rect = helpers.getWorldRect(sprite);

  //    expect(rect.x).to.equal(35);
  //    expect(rect.y).to.equal(35);

  //    sprite.anchor = { x: 1, y: 1 };
  //    rect = helpers.getWorldRect(sprite);

  //    expect(rect.x).to.equal(30);
  //    expect(rect.y).to.equal(30);
  //  });

  //  it('should use objects world x, y, width, and height', () => {
  //    let sprite = {
  //      x: 40,
  //      y: 40,
  //      width: 10,
  //      height: 10,
  //      world: {
  //        x: 10,
  //        y: 20,
  //        width: 20,
  //        height: 30
  //      }
  //    };
  //    let rect = helpers.getWorldRect(sprite);

  //    expect(rect.x).to.equal(10);
  //    expect(rect.y).to.equal(20);
  //    expect(rect.width).to.equal(20);
  //    expect(rect.height).to.equal(30);
  //  });

  //  it('should work for tileEngine', () => {
  //    let tileEngine = TileEngine({
  //      width: 10,
  //      height: 12,
  //      tilewidth: 32,
  //      tileheight: 32,
  //      tilesets: []
  //    });
  //    let rect = helpers.getWorldRect(tileEngine);

  //    expect(rect.x).to.equal(0);
  //    expect(rect.y).to.equal(0);
  //    expect(rect.width).to.equal(320);
  //    expect(rect.height).to.equal(384);
  //  });
  //});

  // --------------------------------------------------
  // depthSort
  // --------------------------------------------------
  //  describe('depthSort', () => {
  //    it('should return the difference between the y props', () => {
  //      let sprite1 = Sprite({
  //        x: 10,
  //        y: 20,
  //        width: 10,
  //        height: 20
  //      });

  //      let sprite2 = Sprite({
  //        x: 19,
  //        y: 39,
  //        width: 10,
  //        height: 20
  //      });

  //      let value = helpers.depthSort(sprite1, sprite2);

  //      expect(value).to.equal(-19);
  //    });

  //    it('should take into account anchor', () => {
  //      let sprite1 = Sprite({
  //        x: 40,
  //        y: 40,
  //        width: 10,
  //        height: 10,
  //        anchor: { x: 0.5, y: 0.5 }
  //      });

  //      let sprite2 = Sprite({
  //        x: 40,
  //        y: 40,
  //        width: 10,
  //        height: 10,
  //        anchor: { x: 1, y: 1 }
  //      });

  //      let value = helpers.depthSort(sprite1, sprite2);

  //      expect(value).to.equal(5);
  //    });

  //    it('should use objects world x, y, width, and height', () => {
  //      let sprite1 = {
  //        x: 40,
  //        y: 40,
  //        width: 10,
  //        height: 10,
  //        world: {
  //          x: 10,
  //          y: 20,
  //          width: 20,
  //          height: 30
  //        }
  //      };

  //      let sprite2 = Sprite({
  //        x: 19,
  //        y: 39,
  //        width: 10,
  //        height: 20
  //      });

  //      let value = helpers.depthSort(sprite1, sprite2);

  //      expect(value).to.equal(-19);
  //    });

  //    it('should accept different prop to compare', () => {
  //      let sprite1 = Sprite({
  //        x: 10,
  //        y: 20,
  //        width: 10,
  //        height: 20
  //      });

  //      let sprite2 = Sprite({
  //        x: 20,
  //        y: 39,
  //        width: 10,
  //        height: 20
  //      });

  //      let value = helpers.depthSort(sprite1, sprite2, 'x');

  //      expect(value).to.equal(-10);
  //    });
  //  });
  //});

}