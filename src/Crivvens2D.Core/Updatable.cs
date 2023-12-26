using Vector = Crivvens2D.Core.Vector;
namespace Crivvens2D.Core;

/**
 * This is a private class that is used just to help make the GameObject class more manageable and smaller.
 *
 * It maintains everything that can be changed in the update function:
 * position
 * velocity
 * acceleration
 * ttl
 */
public abstract class Updatable {
  public Vector Position { get; set; }
  public Vector Velocity { get; set; }
  public Vector Acceleration { get; set; }
  public int TTL { get; set; } // Time to Live

  public abstract void _pc();

  // Nope

  public Updatable(Vector? position = null, Vector? velocity = null, Vector? acceleration = null, int? ttl = null) {
    // --------------------------------------------------
    // defaults
    // --------------------------------------------------

    /**
     * The game objects position vector. Represents the local position of the object as opposed to the [world](api/gameObject#world) position.
     * @property {Vector} position
     * @memberof GameObject
     * @page GameObject
     */
    this.Position = position ?? new Vector();

    // --------------------------------------------------
    // optionals
    // --------------------------------------------------

    // @ifdef GAMEOBJECT_VELOCITY
    /**
     * The game objects velocity vector.
     * @memberof GameObject
     * @property {Vector} velocity
     * @page GameObject
     */
    this.Velocity = velocity ?? new Vector();
    // @endif

    // @ifdef GAMEOBJECT_ACCELERATION
    /**
     * The game objects acceleration vector.
     * @memberof GameObject
     * @property {Vector} acceleration
     * @page GameObject
     */
    this.Acceleration = acceleration ?? new Vector();
    // @endif

    // @ifdef GAMEOBJECT_TTL
    /**
     * How may frames the game object should be alive.
     * @memberof GameObject
     * @property {Number} ttl
     * @page GameObject
     */
    this.TTL = ttl ?? int.MaxValue;
    // @endif

    // add all properties to the object, overriding any defaults
    //Object.assign(this, properties);
  }

  /**
   * Update the position of the game object and all children using their velocity and acceleration. Calls the game objects [advance()](api/gameObject#advance) function.
   * @memberof GameObject
   * @function update
   * @page GameObject
   *
   * @param {Number} [dt] - Time since last update.
   */
  public void Update(double dt) {
    this.Advance(dt);
  }

  /**
   * Move the game object by its acceleration and velocity. If you pass `dt` it will multiply the vector and acceleration by that number. This means the `dx`, `dy`, `ddx` and `ddy` should be how far you want the object to move in 1 second rather than in 1 frame.
   *
   * If you override the game objects [update()](api/gameObject#update) function with your own update function, you can call this function to move the game object normally.
   *
   * ```js
   * import { GameObject } from 'kontra';
   *
   * let gameObject = GameObject({
   *   x: 100,
   *   y: 200,
   *   width: 20,
   *   height: 40,
   *   dx: 5,
   *   dy: 2,
   *   update: function() {
   *     // move the game object normally
   *     this.advance();
   *
   *     // change the velocity at the edges of the canvas
   *     if (this.x < 0 ||
   *         this.x + this.width > this.context.canvas.width) {
   *       this.dx = -this.dx;
   *     }
   *     if (this.y < 0 ||
   *         this.y + this.height > this.context.canvas.height) {
   *       this.dy = -this.dy;
   *     }
   *   }
   * });
   * ```
   * @memberof GameObject
   * @function advance
   * @page GameObject
   *
   * @param {Number} [dt] - Time since last update.
   *
   */
  public virtual void Advance(double dt) {
    // @ifdef GAMEOBJECT_VELOCITY
    // @ifdef GAMEOBJECT_ACCELERATION
    var acceleration = this.Acceleration;

    // @ifdef VECTOR_SCALE
    if (dt != 0) {
      acceleration = acceleration.Scale(dt);
    }
    // @endif

    this.Velocity = this.Velocity.Add(acceleration);
    // @endif
    // @endif

    // @ifdef GAMEOBJECT_VELOCITY
    var velocity = this.Velocity;

    // @ifdef VECTOR_SCALE
    if (dt != 0) {
      velocity = velocity.Scale(dt);
    }
    // @endif

    this.Position = this.Position.Add(velocity);
    this._pc();
    // @endif

    // @ifdef GAMEOBJECT_TTL
    this.TTL--;
    // @endif
  }

  // --------------------------------------------------
  // velocity
  // --------------------------------------------------

  // @ifdef GAMEOBJECT_VELOCITY
  /**
   * X coordinate of the velocity vector.
   * @memberof GameObject
   * @property {Number} dx
   * @page GameObject
   */
  public double dx {
    get => this.Velocity.X;
    set => this.Velocity.X = value;
  }

  /**
   * Y coordinate of the velocity vector.
   * @memberof GameObject
   * @property {Number} dy
   * @page GameObject
   */
  public double dy {
    get => this.Velocity.Y;
    set => this.Velocity.Y = value;
  }
  // @endif

  // --------------------------------------------------
  // acceleration
  // --------------------------------------------------

  // @ifdef GAMEOBJECT_ACCELERATION
  /**
   * X coordinate of the acceleration vector.
   * @memberof GameObject
   * @property {Number} ddx
   * @page GameObject
   */
  public double ddx {
    get => this.Acceleration.X;
    set => this.Acceleration.X = value;
  }

  /**
   * Y coordinate of the acceleration vector.
   * @memberof GameObject
   * @property {Number} ddy
   * @page GameObject
   */
  public double ddy {
    get => this.Acceleration.Y;
    set => this.Acceleration.Y = value;
  }
  // @endif

  // --------------------------------------------------
  // ttl
  // --------------------------------------------------

  // @ifdef GAMEOBJECT_TTL
  /**
   * Check if the game object is alive.
   * @memberof GameObject
   * @function isAlive
   * @page GameObject
   *
   * @returns {Boolean} `true` if the game objects [ttl](api/gameObject#ttl) property is above `0`, `false` otherwise.
   */
  public bool IsAlive {
    get => this.TTL > 0;
  }
  // @endif

  //_pc() { }
}

