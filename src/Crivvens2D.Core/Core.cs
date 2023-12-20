using static Crivvens2D.Core.Events;

namespace Crivvens2D.Core;

/**
 * Functions for initializing the Kontra library and getting the canvas and context
 * objects.
 *
 * ```js
 * import { getCanvas, getContext, init } from 'kontra';
 *
 * let { canvas, context } = init();
 *
 * // or can get canvas and context through functions
 * canvas = getCanvas();
 * context = getContext();
 * ```
 * @sectionName Core
 */
public static class Core {

  // allow contextless environments, such as using ThreeJS as the main
  // canvas, by proxying all canvas context calls
  // Nope. Not the needful.

  /**
   * Return the canvas element.
   * @function getCanvas
   *
   * @returns {HTMLCanvasElement} The canvas element for the game.
   */
    public static ICanvas? Canvas { get; internal set; }

  /**
   * Return the context object.
   * @function getContext
   *
   * @returns {CanvasRenderingContext2D} The context object the game draws to.
   */
  // Nope. Not the needful.

  /**
   * Initialize the library and set up the canvas. Typically you will call `init()` as the first thing and give it the canvas to use. This will allow all Kontra objects to reference the canvas when created.
   *
   * ```js
   * import { init } from 'kontra';
   *
   * let { canvas, context } = init('game');
   * ```
   * @function init
   *
   * @param {String|HTMLCanvasElement} [canvas] - The canvas for Kontra to use. Can either be the ID of the canvas element or the canvas element itself. Defaults to using the first canvas element on the page.
   * @param {Object} [options] - Game options.
   * @param {Boolean} [options.contextless=false] - If the game will run in an contextless environment. A contextless environment uses a [Proxy](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Proxy) for the `canvas` and `context` so all property and function calls will noop.
   *
   * @returns {{canvas: HTMLCanvasElement, context: CanvasRenderingContext2D}} An object with properties `canvas` and `context`. `canvas` it the canvas element for the game and `context` is the context object the game draws to.
   */
  public static ICanvas Init(ICanvas canvas) {
    // check if canvas is a string first, an element next, or default to
    // getting first canvas on page

    // Nope.

    // @ifdef DEBUG
    ArgumentNullException.ThrowIfNull(canvas);
    // @endif

    Canvas = canvas;

    Emit("init");

    return canvas;
  }

  // expose for testing
  // nope. not the needful.
}