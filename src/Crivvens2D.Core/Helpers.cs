namespace Crivvens2D.Core;
/**
 * A group of helpful functions that are commonly used for game development. Includes things such as converting between radians and degrees and getting random integers.
 *
 * ```js
 * import { degToRad } from 'kontra';
 *
 * let radians = degToRad(180);  // => 3.14
 * ```
 * @sectionName Helpers
 */
public static class Helpers {

  /**
   * Convert degrees to radians.
   * @function degToRad
   *
   * @param {Number} deg - Degrees to convert.
   *
   * @returns {Number} The value in radians.
   */

  public static double DegToRad(double deg) {
    return (deg * Math.PI) / 180;
  }

  /**
   * Convert radians to degrees.
   * @function radToDeg
   *
   * @param {Number} rad - Radians to convert.
   *
   * @returns {Number} The value in degrees.
   */

  public static double RadToDeg(double rad) {
    return (rad * 180) / Math.PI;
  }

  /**
   * Return the angle in radians from one point to another point.
   *
   * ```js
   * import { angleToTarget, Sprite } from 'kontra';
   *
   * let sprite = Sprite({
   *   x: 10,
   *   y: 10,
   *   width: 20,
   *   height: 40,
   *   color: 'blue'
   * });
   *
   * sprite.rotation = angleToTarget(sprite, {x: 100, y: 30});
   *
   * let sprite2 = Sprite({
   *   x: 100,
   *   y: 30,
   *   width: 20,
   *   height: 40,
   *   color: 'red',
   * });
   *
   * sprite2.rotation = angleToTarget(sprite2, sprite);
   * ```
   * @function angleToTarget
   *
   * @param {{x: Number, y: Number}} source - The {x,y} source point.
   * @param {{x: Number, y: Number}} target - The {x,y} target point.
   *
   * @returns {Number} Angle (in radians) from the source point to the target point.
   */
  public static double AngleToTarget(Point source, Point target) {
    return Math.Atan2(target.Y - source.Y, target.X - source.X);
  }

  /**
   * Rotate a point by an angle.
   * @function rotatePoint
   *
   * @param {{x: Number, y: Number}} point - The {x,y} point to rotate.
   * @param {Number} angle - Angle (in radians) to rotate.
   *
   * @returns {{x: Number, y: Number}} The new x and y coordinates after rotation.
   */
  public static Point RotatePoint(Point point, double angle) {
    var sin = Math.Sin(angle);
    var cos = Math.Cos(angle);

    return new Point(
      X: point.X * cos - point.Y * sin,
      Y: point.X * sin + point.Y * cos
    );
  }

  /**
   * Move a point by an angle and distance.
   * @function movePoint
   *
   * @param {{x: Number, y: Number}} point - The {x,y} point to move.
   * @param {Number} angle - Angle (in radians) to move.
   * @param {Number} distance - Distance to move.
   *
   * @returns {{x: Number, y: Number}} The new x and y coordinates after moving.
   */
  public static Point MovePoint(Point point, double angle, double distance) {
    return new Point(
      X: point.X + Math.Cos(angle) * distance,
      Y: point.Y + Math.Sin(angle) * distance
      );
  }

  /**
   * Return a random integer between a minimum (inclusive) and maximum (inclusive) integer.
   * @see https://stackoverflow.com/a/1527820/2124254
   * @function randInt
   *
   * @param {Number} min - Min integer.
   * @param {Number} max - Max integer.
   *
   * @returns {Number} Random integer between min and max values.
   */
  public static int RandInt(int min, int max, Random? random = null) {
    random ??= new Random();
    return random.Next(min, max);
  }

  /**
   * Create a seeded random number generator.
   *
   * ```js
   * import { seedRand } from 'kontra';
   *
   * let rand = seedRand('kontra');
   * console.log(rand());  // => always 0.33761959057301283
   * ```
   * @see https://stackoverflow.com/a/47593316/2124254
   * @see https://github.com/bryc/code/blob/master/jshash/PRNGs.md
   *
   * @function seedRand
   *
   * @param {String} str - String to seed the random number generator.
   *
   * @returns {() => Number} Seeded random number generator function.
   */
  public static Random SeedRand(string str) {
    var bytes = Encoding.UTF8.GetBytes(str);
    var seed = BitConverter.ToInt32(bytes, 0);
    return new Random(seed);
  }

  /**
   * Linearly interpolate between two values. The function calculates the number between two values based on a percent. Great for smooth transitions.
   *
   * ```js
   * import { lerp } from 'kontra';
   *
   * console.log( lerp(10, 20, 0.5) );  // => 15
   * console.log( lerp(10, 20, 2) );  // => 30
   * ```
   * @function lerp
   *
   * @param {Number} start - Start value.
   * @param {Number} end - End value.
   * @param {Number} percent - Percent to interpolate.
   *
   * @returns {Number} Interpolated number between the start and end values
   */
  public static double Lerp(double start, double end, double percent) {
    return start * (1 - percent) + end * percent;
  }

  /**
   * Return the linear interpolation percent between two values. The function calculates the percent between two values of a given value.
   *
   * ```js
   * import { inverseLerp } from 'kontra';
   *
   * console.log( inverseLerp(10, 20, 15) );  // => 0.5
   * console.log( inverseLerp(10, 20, 30) );  // => 2
   * ```
   * @function inverseLerp
   *
   * @param {Number} start - Start value.
   * @param {Number} end - End value.
   * @param {Number} value - Value between start and end.
   *
   * @returns {Number} Percent difference between the start and end values.
   */
  public static double InverseLerp(double start, double end, double value) {
    return (value - start) / (end - start);
  }

  /**
   * Clamp a number between two values, preventing it from going below or above the minimum and maximum values.
   * @function clamp
   *
   * @param {Number} min - Min value.
   * @param {Number} max - Max value.
   * @param {Number} value - Value to clamp.
   *
   * @returns {Number} Value clamped between min and max.
   */
  public static double Clamp(double min, double max, double value) {
    return Math.Min(Math.Max(min, value), max);
  }

  /**
   * Save an item to localStorage. A value of `undefined` will remove the item from localStorage.
   * @function setStoreItem
   *
   * @param {String} key - The name of the key.
   * @param {*} value - The value to store.
   */
  // TODO:
  //export function setStoreItem(key, value) {
  //  if (value == undefined) {
  //    localStorage.removeItem(key);
  //  }
  //  else {
  //    localStorage.setItem(key, JSON.stringify(value));
  //  }
  //}

  /**
   * Retrieve an item from localStorage and convert it back to its original type.
   *
   * Normally when you save a value to LocalStorage it converts it into a string. So if you were to save a number, it would be saved as `"12"` instead of `12`. This function enables the value to be returned as `12`.
   * @function getStoreItem
   *
   * @param {String} key - Name of the key of the item to retrieve.
   *
   * @returns {*} The retrieved item.
   */
  // TODO:
  //export function getStoreItem(key) {
  //  let value = localStorage.getItem(key);

  //  try {
  //    value = JSON.parse(value);
  //  }
  //  catch (e) {
  //    // do nothing
  //  }

  //  return value;
  //}

  /**
   * Check if two objects collide. Uses a simple [Axis-Aligned Bounding Box (AABB) collision check](https://developer.mozilla.org/en-US/docs/Games/Techniques/2D_collision_detection#Axis-Aligned_Bounding_Box). Takes into account the objects [anchor](api/gameObject#anchor) and [scale](api/gameObject#scale).
   *
   * **NOTE:** Does not take into account object rotation. If you need collision detection between rotated objects you will need to implement your own `collides()` function. I suggest looking at the Separate Axis Theorem.
   *
   * ```js
   * import { Sprite, collides } from 'kontra';
   *
   * let sprite = Sprite({
   *   x: 100,
   *   y: 200,
   *   width: 20,
   *   height: 40
   * });
   *
   * let sprite2 = Sprite({
   *   x: 150,
   *   y: 200,
   *   width: 20,
   *   height: 20
   * });
   *
   * collides(sprite, sprite2);  //=> false
   *
   * sprite2.x = 115;
   *
   * collides(sprite, sprite2);  //=> true
   * ```
   * @function collides
   *
   * @param {{x: Number, y: Number, width: Number, height: Number}|{world: {x: Number, y: Number, width: Number, height: Number}}} obj1 - Object reference.
   * @param {{x: Number, y: Number, width: Number, height: Number}|{world: {x: Number, y: Number, width: Number, height: Number}}} obj2 - Object to check collision against.
   *
   * @returns {Boolean} `true` if the objects collide, `false` otherwise.
   */
  // TODO:
  //export function collides(obj1, obj2) {
  //  [obj1, obj2] = [obj1, obj2].map(obj => getWorldRect(obj));

  //  return (
  //    obj1.x < obj2.x + obj2.width &&
  //    obj1.x + obj1.width > obj2.x &&
  //    obj1.y < obj2.y + obj2.height &&
  //    obj1.y + obj1.height > obj2.y
  //  );
  //}

  /**
   * Return the world rect of an object. The rect is the world position of the top-left corner of the object and its size. Takes into account the objects anchor and scale.
   * @function getWorldRect
   *
   * @param {{x: Number, y: Number, width: Number, height: Number}|{world: {x: Number, y: Number, width: Number, height: Number}}|{mapwidth: Number, mapheight: Number}} obj - Object to get world rect of.
   *
   * @returns {{x: Number, y: Number, width: Number, height: Number}} The world `x`, `y`, `width`, and `height` of the object.
   */
  // TODO:
  //export function getWorldRect(obj) {
  //  let { x = 0, y = 0, width, height } = obj.world || obj;

  //  // take into account tileEngine
  //  if (obj.mapwidth) {
  //    width = obj.mapwidth;
  //    height = obj.mapheight;
  //  }

  //  // @ifdef GAMEOBJECT_ANCHOR
  //  // account for anchor
  //  if (obj.anchor) {
  //    x -= width * obj.anchor.x;
  //    y -= height * obj.anchor.y;
  //  }
  //  // @endif

  //  // @ifdef GAMEOBJECT_SCALE
  //  // account for negative scales
  //  if (width < 0) {
  //    x += width;
  //    width *= -1;
  //  }
  //  if (height < 0) {
  //    y += height;
  //    height *= -1;
  //  }
  //  // @endif

  //  return {
  //    x,
  //  y,
  //  width,
  //  height
  //  };
  //}

  /**
   * Compare two objects world rects to determine how to sort them. Is used as the `compareFunction` to [Array.prototype.sort](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/sort).
   * @function depthSort
   *
   * @param {{x: Number, y: Number, width: Number, height: Number}|{world: {x: Number, y: Number, width: Number, height: Number}}} obj1 - First object to compare.
   * @param {{x: Number, y: Number, width: Number, height: Number}|{world: {x: Number, y: Number, width: Number, height: Number}}} obj2 - Second object to compare.
   * @param {String} [prop='y'] - Objects [getWorldRect](/api/helpers#getWorldRect) property to compare.
   *
   * @returns {Number} The difference between the objects compare property.
   */
  //export function depthSort(obj1, obj2, prop = 'y') {
  //  [obj1, obj2] = [obj1, obj2].map(getWorldRect);
  //  return obj1[prop] - obj2[prop];
  //}
}