namespace Crivvens2D.Core;

/**
 * A simple 2d vector object. Takes either separate `x` and `y` coordinates or a Vector-like object.
 *
 * ```js
 * import { Vector } from 'kontra';
 *
 * let vector = Vector(100, 200);
 * let vector2 = Vector({x: 100, y: 200});
 * ```
 * @class Vector
 *
 * @param {Number|{x: number, y: number}} [x=0] - X coordinate of the vector or a Vector-like object. If passing an object, the `y` param is ignored.
 * @param {Number} [y=0] - Y coordinate of the vector.
 */
public class Vector : IPoint {

  public Vector(IPoint x, Vector? clamp = null) : this(x.X, x.Y, clamp) { }

  public Vector(double x = 0, double y = 0, Vector? clamp = null) {

    this.X = x;
    this.Y = y;

    // @ifdef VECTOR_CLAMP
    // preserve vector clamping when creating new vectors
    if (clamp?._isClamped ?? false) {
      var rx = this.X;
      var ry = this.Y;
      this.Clamp(clamp._xMinClamped, clamp._yMinClamped, clamp._xMaxClamped, clamp._yMaxClamped);

      // reset x and y so clamping takes effect
      this.X = rx;
      this.Y = ry;
    }
    // @endif
  }

  /**
   * Set the x and y coordinate of the vector.
   * @memberof Vector
   * @function set
   *
   * @param {Vector|{x: number, y: number}} vector - Vector to set coordinates from.
   */
  public void Set(IPoint vec) {
    this.X = vec.X;
    this.Y = vec.Y;
  }

  /**
   * Calculate the addition of the current vector with the given vector.
   * @memberof Vector
   * @function add
   *
   * @param {Vector|{x: number, y: number}} vector - Vector to add to the current Vector.
   *
   * @returns {Vector} A new Vector instance whose value is the addition of the two vectors.
   */
  public Vector Add(Vector vec) {
    return new Vector(this.X + vec.X, this.Y + vec.Y, clamp: this);
  }

  // @ifdef VECTOR_SUBTRACT
  /**
   * Calculate the subtraction of the current vector with the given vector.
   * @memberof Vector
   * @function subtract
   *
   * @param {Vector|{x: number, y: number}} vector - Vector to subtract from the current Vector.
   *
   * @returns {Vector} A new Vector instance whose value is the subtraction of the two vectors.
   */
  public Vector Subtract(Vector vec) {
    return new Vector(this.X - vec.X, this.Y - vec.Y, clamp: this);
  }
  // @endif

  // @ifdef VECTOR_SCALE
  /**
   * Calculate the multiple of the current vector by a value.
   * @memberof Vector
   * @function scale
   *
   * @param {Number} value - Value to scale the current Vector.
   *
   * @returns {Vector} A new Vector instance whose value is multiplied by the scalar.
   */
  public Vector Scale(double value) {
    return new Vector(this.X * value, this.Y * value);
  }
  // @endif

  // @ifdef VECTOR_NORMALIZE
  /**
   * Calculate the normalized value of the current vector. Requires the Vector [length](api/vector#length) function.
   * @memberof Vector
   * @function normalize
   *
   * @returns {Vector} A new Vector instance whose value is the normalized vector.
   */
  // @see https://github.com/jed/140bytes/wiki/Byte-saving-techniques#use-placeholder-arguments-instead-of-var
  public Vector Normalize() => Normalize(this.Length());
  public Vector Normalize(double length) {
    var calcLength = (length == 0 ? 1 : length);
    return new Vector((double) (this.X / calcLength), (double) (this.Y / calcLength));
  }
  // @endif

  // @ifdef VECTOR_DOT||VECTOR_ANGLE
  /**
   * Calculate the dot product of the current vector with the given vector.
   * @memberof Vector
   * @function dot
   *
   * @param {Vector|{x: number, y: number}} vector - Vector to dot product against.
   *
   * @returns {Number} The dot product of the vectors.
   */
  public double Dot(Vector vec) {
    return this.X * vec.X + this.Y * vec.Y;
  }
  // @endif

  // @ifdef VECTOR_LENGTH||VECTOR_NORMALIZE||VECTOR_ANGLE
  /**
   * Calculate the length (magnitude) of the Vector.
   * @memberof Vector
   * @function length
   *
   * @returns {Number} The length of the vector.
   */
  private static double Hypot(double x, double y)
    => Math.Sqrt((x * x) + (y * y));
  public double Length() {
    return Hypot(this.X, this.Y);
  }
  // @endif

  // @ifdef VECTOR_DISTANCE
  /**
   * Calculate the distance between the current vector and the given vector.
   * @memberof Vector
   * @function distance
   *
   * @param {Vector|{x: number, y: number}} vector - Vector to calculate the distance between.
   *
   * @returns {Number} The distance between the two vectors.
   */
  public double Distance(Vector vec) {
    return Hypot(this.X - vec.X, this.Y - vec.Y);
  }
  // @endif

  // @ifdef VECTOR_ANGLE
  /**
   * Calculate the angle (in radians) between the current vector and the given vector. Requires the Vector [dot](api/vector#dot) and [length](api/vector#length) functions.
   * @memberof Vector
   * @function angle
   *
   * @param {Vector} vector - Vector to calculate the angle between.
   *
   * @returns {Number} The angle (in radians) between the two vectors.
   */
  public double Angle(Vector vec) {
    return Math.Acos(this.Dot(vec) / (this.Length() * vec.Length()));
  }
  // @endif

  // @ifdef VECTOR_DIRECTION
  /**
   * Calculate the angle (in radians) of the current vector.
   * @memberof Vector
   * @function direction
   *
   * @returns {Number} The angle (in radians) of the vector.
   */
  public double Direction() {
    return Math.Atan2(this.Y, this.X);
  }
  // @endif

  // @ifdef VECTOR_CLAMP
  /**
   * Clamp the Vector between two points, preventing `x` and `y` from going below or above the minimum and maximum values. Perfect for keeping a sprite from going outside the game boundaries.
   *
   * ```js
   * import { Vector } from 'kontra';
   *
   * let vector = Vector(100, 200);
   * vector.clamp(0, 0, 200, 300);
   *
   * vector.x += 200;
   * console.log(vector.x);  //=> 200
   *
   * vector.y -= 300;
   * console.log(vector.y);  //=> 0
   *
   * vector.add({x: -500, y: 500});
   * console.log(vector);    //=> {x: 0, y: 300}
   * ```
   * @memberof Vector
   * @function clamp
   *
   * @param {Number} xMin - Minimum x value.
   * @param {Number} yMin - Minimum y value.
   * @param {Number} xMax - Maximum x value.
   * @param {Number} yMax - Maximum y value.
   */
  private bool _isClamped = false;
  private double _xMinClamped = 0;
  private double _yMinClamped = 0;
  private double _xMaxClamped = 0;
  private double _yMaxClamped = 0;
  public void Clamp(double xMin, double yMin, double xMax, double yMax) {
    _isClamped = true;
    _xMinClamped = xMin;
    _yMinClamped = yMin;
    _xMaxClamped = xMax;
    _yMaxClamped = yMax;
  }

  /**
   * X coordinate of the vector.
   * @memberof Vector
   * @property {Number} x
   */
  private double _x = 0; // Waiting for c# 13 with field
  public double X {
    get => _x;
    set {
      _x = _isClamped ? Helpers.Clamp(_xMinClamped, _xMaxClamped, value) : value;
    }
  }

  /**
   * Y coordinate of the vector.
   * @memberof Vector
   * @property {Number} y
   */

  private double _y = 0;// Waiting for C# 13 with field
  public double Y {
    get => _y;
    set => _y = _isClamped ? Helpers.Clamp(_yMinClamped, _yMaxClamped, value) : value;
  }

  // @endif
}

