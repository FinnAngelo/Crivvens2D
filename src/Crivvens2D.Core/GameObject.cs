// import { getContext } from './core.js';
// import Updatable from './updatable.js';
// import { on } from './events.js';
// import { rotatePoint, clamp } from './helpers.js';
// import { noop, removeFromArray } from './utils.js';
using static Crivvens2D.Core.Events;
namespace Crivvens2D.Core;

// /**
//  * The base class of most renderable classes. Handles things such as position, rotation, anchor, and the update and render life cycle.
//  *
//  * Typically you don't create a GameObject directly, but rather extend it for new classes.
//  * @class GameObject
//  *
//  * @param {Object} [properties] - Properties of the game object.
//  * @param {Number} [properties.x] - X coordinate of the position vector.
//  * @param {Number} [properties.y] - Y coordinate of the position vector.
//  * @param {Number} [properties.width] - Width of the game object.
//  * @param {Number} [properties.height] - Height of the game object.
//  *
//  * @param {CanvasRenderingContext2D} [properties.context] - The context the game object should draw to. Defaults to [core.getContext()](api/core#getContext).
//  *
//  * @param {Number} [properties.dx] - X coordinate of the velocity vector.
//  * @param {Number} [properties.dy] - Y coordinate of the velocity vector.
//  * @param {Number} [properties.ddx] - X coordinate of the acceleration vector.
//  * @param {Number} [properties.ddy] - Y coordinate of the acceleration vector.
//  * @param {Number} [properties.ttl=Infinity] - How many frames the game object should be alive. Used by [Pool](api/pool).
//  *
//  * @param {{x: Number, y: Number}} [properties.anchor={x:0,y:0}] - The x and y origin of the game object. {x:0, y:0} is the top left corner of the game object, {x:1, y:1} is the bottom right corner.
//  * @param {GameObject[]} [properties.children] - Children to add to the game object.
//  * @param {Number} [properties.opacity=1] - The opacity of the game object.
//  * @param {Number} [properties.rotation=0] - The rotation around the anchor in radians.
//  * @param {Number} [properties.scaleX=1] - The x scale of the game object.
//  * @param {Number} [properties.scaleY=1] - The y scale of the game object.
//  *
//  * @param {(dt?: Number) => void} [properties.update] - Function called every frame to update the game object.
//  * @param {Function} [properties.render] - Function called every frame to render the game object.
//  *
//  * @param {...*} properties.props - Any additional properties you need added to the game object. For example, if you pass `gameObject({type: 'player'})` then the game object will also have a property of the same name and value. You can pass as many additional properties as you want.
//  */
public abstract partial class GameObject : Updatable {
  //   /**
  //    * @docs docs/api_docs/gameObject.js
  //    */

  //   /**
  //    * Use this function to reinitialize a game object. It takes the same properties object as the constructor. Useful it you want to repurpose a game object.
  //    * @memberof GameObject
  //    * @function init
  //    *
  //    * @param {Object} properties - Properties of the game object.
  //    */
  public GameObject(
    // --------------------------------------------------
    // defaults
    // --------------------------------------------------

    /**
     * The width of the game object. Represents the local width of the object as opposed to the [world](api/gameObject#world) width.
     * @memberof GameObject
     * @property {Number} width
     */
    double width,// = 0,

    /**
     * The height of the game object. Represents the local height of the object as opposed to the [world](api/gameObject#world) height.
     * @memberof GameObject
     * @property {Number} height
     */
    double height,// = 0,

    /**
     * The context the game object will draw to.
     * @memberof GameObject
     * @property {CanvasRenderingContext2D} context
     */
    Context context, // = getContext(),

    Action render,// = this.draw,
    Action<double> update,// = this.advance,

    // --------------------------------------------------
    // optionals
    // --------------------------------------------------

    // @ifdef GAMEOBJECT_GROUP
    /**
     * The game objects parent object.
     * @memberof GameObject
     * @property {GameObject|null} parent
     */

    /**
     * The game objects children objects.
     * @memberof GameObject
     * @property {GameObject[]} children
     */
    List<GameObject> children,// = [],
                              // @endif

    // @ifdef GAMEOBJECT_ANCHOR
    /**
     * The x and y origin of the game object. {x:0, y:0} is the top left corner of the game object, {x:1, y:1} is the bottom right corner.
     * @memberof GameObject
     * @property {{x: Number, y: Number}} anchor
     *
     * @example
     * // exclude-code:start
     * let { GameObject } = kontra;
     * // exclude-code:end
     * // exclude-script:start
     * import { GameObject } from 'kontra';
     * // exclude-script:end
     *
     * let gameObject = GameObject({
     *   x: 150,
     *   y: 100,
     *   width: 50,
     *   height: 50,
     *   color: 'red',
     *   // exclude-code:start
     *   context: context,
     *   // exclude-code:end
     *   render: function() {
     *     this.context.fillStyle = this.color;
     *     this.context.fillRect(0, 0, this.height, this.width);
     *   }
     * });
     *
     * function drawOrigin(gameObject) {
     *   gameObject.context.fillStyle = 'yellow';
     *   gameObject.context.beginPath();
     *   gameObject.context.arc(gameObject.x, gameObject.y, 3, 0, 2*Math.PI);
     *   gameObject.context.fill();
     * }
     *
     * gameObject.render();
     * drawOrigin(gameObject);
     *
     * gameObject.anchor = {x: 0.5, y: 0.5};
     * gameObject.x = 300;
     * gameObject.render();
     * drawOrigin(gameObject);
     *
     * gameObject.anchor = {x: 1, y: 1};
     * gameObject.x = 450;
     * gameObject.render();
     * drawOrigin(gameObject);
     */
    Point anchor, // = { x: 0, y: 0 },
                  // @endif

    // @ifdef GAMEOBJECT_OPACITY
    /**
     * The opacity of the object. Represents the local opacity of the object as opposed to the [world](api/gameObject#world) opacity.
     * @memberof GameObject
     * @property {Number} opacity
     */
    double opacity,// = 1,
                   // @endif

    // @ifdef GAMEOBJECT_ROTATION
    /**
     * The rotation of the game object around the anchor in radians. Represents the local rotation of the object as opposed to the [world](api/gameObject#world) rotation.
     * @memberof GameObject
     * @property {Number} rotation
     */
    double rotation,// = 0,
                    // @endif

    // @ifdef GAMEOBJECT_SCALE
    /**
     * The x scale of the object. Represents the local x scale of the object as opposed to the [world](api/gameObject#world) x scale.
     * @memberof GameObject
     * @property {Number} scaleX
     */
    double scaleX,// = 1,

    /**
     * The y scale of the object. Represents the local y scale of the object as opposed to the [world](api/gameObject#world) y scale.
     * @memberof GameObject
     * @property {Number} scaleY
     */
    double scaleY // = 1
                  // @endif

  //...props
  ) {
    if(context == null) throw new ArgumentException(nameof(context));
    ArgumentNullException.ThrowIfNull(context, nameof(context));
    // @ifdef GAMEOBJECT_GROUP
    this._c = [];
    // @endif

    // by setting defaults to the parameters and passing them into
    // the init, we can ensure that a parent class can set overriding
    // defaults and the GameObject won't undo it (if we set
    // `this.width` then no parent could provide a default value for
    // width)
    //super.init({
    Width = width;
    Height = height;
    Context = context;

    // @ifdef GAMEOBJECT_ANCHOR
    Anchor = anchor;
    // @endif

    // @ifdef GAMEOBJECT_OPACITY
    Opacity = opacity;
    // @endif

    // @ifdef GAMEOBJECT_ROTATION
    Rotation = rotation;
    // @endif

    // @ifdef GAMEOBJECT_SCALE
    ScaleX = scaleX;
    ScaleY = scaleY;
    // @endif

    //  ...props
    //});

    // di = done init
    this._di = true;
    this._uw();

    // @ifdef GAMEOBJECT_GROUP
    this.addChild(children);
    // @endif

    // rf = render function
    this._rf = render;

    // uf = update function
    this._uf = update;

    On("init", () => {
      //this.context ??= getContext();
    });
  }

  /**
   * Update all children
   */
  public virtual void update(double dt) {
    this._uf(dt);

    // @ifdef GAMEOBJECT_GROUP
    this.children.ForEach(child => child?.update(dt));
    // @endif
  }

  /**
   * Render the game object and all children. Calls the game objects [draw()](api/gameObject#draw) function.
   * @memberof GameObject
   * @function render
   */
  public void render() {
    var context = this.Context;
    context.Save();

    // 1) translate to position
    //
    // it's faster to only translate if one of the values is non-zero
    // rather than always translating
    // @see https://jsperf.com/translate-or-if-statement/2
    if (this.X !=0 || this.Y !=0) {
      context.Translate(this.X, this.Y);
    }

    // @ifdef GAMEOBJECT_ROTATION
    // 3) rotate around the anchor
    //
    // it's faster to only rotate when set rather than always rotating
    // @see https://jsperf.com/rotate-or-if-statement/2
    if (this.Rotation != 0) {
      context.Rotate(this.Rotation);
    }
    // @endif

    // @ifdef GAMEOBJECT_SCALE
    // 4) scale after translation to position so object can be
    // scaled in place (rather than scaling position as well).
    //
    // it's faster to only scale if one of the values is not 1
    // rather than always scaling
    // @see https://jsperf.com/scale-or-if-statement/4
    if (this.ScaleX != 1 || this.ScaleY != 1) {
      context.Scale(this.ScaleX, this.ScaleY);
    }
    // @endif

    // @ifdef GAMEOBJECT_ANCHOR
    // 5) translate to the anchor so (0,0) is the top left corner
    // for the render function
    var anchorX = -this.width * this.Anchor.X;
    var anchorY = -this.height * this.Anchor.Y;

    if (anchorX !=0|| anchorY != 0) {
      context.Translate(anchorX, anchorY);
    }
    // @endif

    // @ifdef GAMEOBJECT_OPACITY
    // it's not really any faster to gate the global alpha
    // @see https://jsperf.com/global-alpha-or-if-statement/1
    this.Context.GlobalAlpha = this.Opacity;
    // @endif

    this._rf();

    // @ifdef GAMEOBJECT_ANCHOR
    // 7) translate back to the anchor so children use the correct
    // x/y value from the anchor
    if (anchorX != 0 || anchorY != 0) {
      context.Translate(-anchorX, -anchorY);
    }
    // @endif

    // @ifdef GAMEOBJECT_GROUP
    // perform all transforms on the parent before rendering the
    // children
    var children = this.children;
    children.ForEach(child => child?.render());
    // @endif

    context.Restore();
  }

    /**
     * Draw the game object at its X and Y position, taking into account rotation, scale, and anchor.
     *
     * Do note that the canvas has been rotated and translated to the objects position (taking into account anchor), so {0,0} will be the top-left corner of the game object when drawing.
     *
     * If you override the game objects `render()` function with your own render function, you can call this function to draw the game object normally.
     *
     * ```js
     * let { GameObject } = kontra;
     *
     * let gameObject = GameObject({
     *  x: 290,
     *  y: 80,
     *  width: 20,
     *  height: 40,
     *
     *  render: function() {
     *    // draw the game object normally (perform rotation and other transforms)
     *    this.draw();
     *
     *    // outline the game object
     *    this.context.strokeStyle = 'yellow';
     *    this.context.lineWidth = 2;
     *    this.context.strokeRect(0, 0, this.width, this.height);
     *  }
     * });
     *
     * gameObject.render();
     * ```
     * @memberof GameObject
     * @function draw
     */
  public virtual void draw() { }

    /**
     * Sync property changes from the parent to the child
     */
  public void _pc() {
      this._uw();

      // @ifdef GAMEOBJECT_GROUP
      this.children.ForEach(child => child._pc());
      // @endif
    }

  /**
   * X coordinate of the position vector.
   * @memberof GameObject
   * @property {Number} x
   */
  public double X {
    get => this.Position.X;
    set {
      this.Position.X = value;

      // pc = property changed
      this._pc();
    }
  }

  /**
   * Y coordinate of the position vector.
   * @memberof GameObject
   * @property {Number} y
   */
  public double Y {
    get => this.Position.Y;
    set {
      this.Position.Y = value;
      this._pc();
    }
  }

public double Width {
  // w = width
  get => this._w;
  set {
    this._w = value;
    this._pc();
  }
}

public double Height {
  // h = height
  get => this._h;
  set {
    this._h = value;
    this._pc();
  }
}

    /**
     * Update world properties
     */
    public override void _uw() {
      // don't update world properties until after the init has finished
      if (!this._di) return;

      // @ifdef GAMEOBJECT_GROUP||GAMEOBJECT_OPACITY||GAMEOBJECT_ROTATION||GAMEOBJECT_SCALE
      double _wx = 0,
        _wy = 0,

        // @ifdef GAMEOBJECT_OPACITY
        _wo = 1,
        // @endif

        // @ifdef GAMEOBJECT_ROTATION
        _wr = 0,
        // @endif

        // @ifdef GAMEOBJECT_SCALE
        _wsx = 1,
        _wsy = 1;
        // @endif
      // TODO: } = this.parent || {};
      // @endif

      // wx = world x, wy = world y
      this._wx = this.x;
      this._wy = this.y;

      // ww = world width, wh = world height
      this._ww = this.width;
      this._wh = this.height;

      // @ifdef GAMEOBJECT_OPACITY
      // wo = world opacity
      this._wo = _wo * this.opacity;
      // @endif

      // @ifdef GAMEOBJECT_SCALE
      // wsx = world scale x, wsy = world scale y
      this._wsx = _wsx * this.scaleX;
      this._wsy = _wsy * this.scaleY;

      this._wx = this._wx * _wsx;
      this._wy = this._wy * _wsy;
      this._ww = this.width * this._wsx;
      this._wh = this.height * this._wsy;
      // @endif

      // @ifdef GAMEOBJECT_ROTATION
      // wr = world rotation
      this._wr = _wr + this.rotation;

      let { x, y } = rotatePoint({ x: this._wx, y: this._wy }, _wr);
      this._wx = x;
      this._wy = y;
      // @endif

      // @ifdef GAMEOBJECT_GROUP
      this._wx += _wx;
      this._wy += _wy;
      // @endif
    }

  //   /**
  //    * The world position, width, height, opacity, rotation, and scale. The world property is the true position, width, height, etc. of the object, taking into account all parents.
  //    *
  //    * The world property does not adjust for anchor or scale, so if you set a negative scale the world width or height could be negative. Use [getWorldRect](/api/helpers#getWorldRect) to get the world position and size adjusted for anchor and scale.
  //    * @property {{x: Number, y: Number, width: Number, height: Number, opacity: Number, rotation: Number, scaleX: Number, scaleY: Number}} world
  //    * @memberof GameObject
  //    */
  //   get world() {
  //     return {
  //       x: this._wx,
  //       y: this._wy,
  //       width: this._ww,
  //       height: this._wh,

  //       // @ifdef GAMEOBJECT_OPACITY
  //       opacity: this._wo,
  //       // @endif

  //       // @ifdef GAMEOBJECT_ROTATION
  //       rotation: this._wr,
  //       // @endif

  //       // @ifdef GAMEOBJECT_SCALE
  //       scaleX: this._wsx,
  //       scaleY: this._wsy
  //       // @endif
  //     };
  //   }

  //   // --------------------------------------------------
  //   // group
  //   // --------------------------------------------------

  //   // @ifdef GAMEOBJECT_GROUP
  //   set children(value) {
  //     this.removeChild(this._c);
  //     this.addChild(value);
  //   }

  //   get children() {
  //     return this._c;
  //   }

  //   /**
  //    * Add an object as a child to this object. The objects position, size, and rotation will be relative to the parents position, size, and rotation. The childs [world](api/gameObject#world) property will be updated to take into account this object and all of its parents.
  //    * @memberof GameObject
  //    * @function addChild
  //    *
  //    * @param {...(GameObject|GameObject[])[]} objects - Object to add as a child. Can be a single object, an array of objects, or a comma-separated list of objects.
  //    *
  //    * @example
  //    * // exclude-code:start
  //    * let { GameObject } = kontra;
  //    * // exclude-code:end
  //    * // exclude-script:start
  //    * import { GameObject } from 'kontra';
  //    * // exclude-script:end
  //    *
  //    * function createObject(x, y, color, size = 1) {
  //    *   return GameObject({
  //    *     x,
  //    *     y,
  //    *     width: 50 / size,
  //    *     height: 50 / size,
  //    *     anchor: {x: 0.5, y: 0.5},
  //    *     color,
  //    *     // exclude-code:start
  //    *     context: context,
  //    *     // exclude-code:end
  //    *     render: function() {
  //    *       this.context.fillStyle = this.color;
  //    *       this.context.fillRect(0, 0, this.height, this.width);
  //    *     }
  //    *   });
  //    * }
  //    *
  //    * let parent = createObject(300, 100, 'red');
  //    *
  //    * // create a child that is 25px to the right and
  //    * // down from the parents position
  //    * let child = createObject(25, 25, 'yellow', 2);
  //    *
  //    * parent.addChild(child);
  //    *
  //    * parent.render();
  //    */
  //   addChild(...objects) {
  //     objects.flat().map(child => {
  //       this.children.push(child);
  //       child.parent = this;
  //       child._pc = child._pc || noop;
  //       child._pc();
  //     });
  //   }

  //   /**
  //    * Remove an object as a child of this object. The removed objects [world](api/gameObject#world) property will be updated to not take into account this object and all of its parents.
  //    * @memberof GameObject
  //    * @function removeChild
  //    *
  //    * @param {...(GameObject|GameObject[])[]} objects - Object to remove as a child. Can be a single object, an array of objects, or a comma-separated list of objects.
  //    */
  //   removeChild(...objects) {
  //     objects.flat().map(child => {
  //       if (removeFromArray(this.children, child)) {
  //         child.parent = null;
  //         child._pc();
  //       }
  //     });
  //   }
  //   // @endif

  //   // --------------------------------------------------
  //   // opacity
  //   // --------------------------------------------------

  //   // @ifdef GAMEOBJECT_OPACITY
  //   get opacity() {
  //     return this._opa;
  //   }

  //   set opacity(value) {
  //     this._opa = clamp(0, 1, value);
  //     this._pc();
  //   }
  //   // @endif

  //   // --------------------------------------------------
  //   // rotation
  //   // --------------------------------------------------

  //   // @ifdef GAMEOBJECT_ROTATION
  //   get rotation() {
  //     return this._rot;
  //   }

  //   set rotation(value) {
  //     this._rot = value;
  //     this._pc();
  //   }
  //   // @endif

    // --------------------------------------------------
    // scale
    // --------------------------------------------------

    // @ifdef GAMEOBJECT_SCALE
    /**
     * Set the x and y scale of the object. If only one value is passed, both are set to the same value.
     * @memberof GameObject
     * @function setScale
     *
     * @param {Number} x - X scale value.
     * @param {Number} [y=x] - Y scale value.
     */
    public void setScale(double scale)
      => setScale(x: scale, y:scale);
    public void setScale(double x, double y) {
      this.scaleX = x;
      this.scaleY = y;
    }

    public double ScaleX {
      get => this._scx;
      set => {
        this._scx = value;
        this._pc();
      }
    }

    public double ScaleY {
      get => this._scy;
      set => {
        this._scy = value;
        this._pc();
      }
    }
    
    // @endif
}

// export default function factory() {
//   return new GameObject(...arguments);
// }
// export { GameObject as GameObjectClass };

public abstract partial class GameObject {
  private object[] _c;
  private double Width { get; init; }
  private double Height { get; init; }
  private Context Context { get; init; }
  private Point Anchor { get; init; }
  private double Opacity { get; init; }
  private double Rotation { get; init; }
  private double ScaleX { get; init; }
  private double ScaleY { get; init; }


  private readonly bool _di;
  private List<GameObject> children { get; init; } = [];

  public abstract void addChild(IEnumerable<GameObject> children);

  public readonly Action _rf;
  public readonly Action<double> _uf;
}