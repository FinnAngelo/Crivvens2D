using Delegates = System.Collections.Generic.List<System.Delegate>;
namespace Crivvens2D.Core;
/**
 * A simple event system. Allows you to hook into Kontra lifecycle events or create your own, such as for [Plugins](api/plugin).
 *
 * ```js
 * import { on, off, emit } from 'kontra';
 *
 * function callback(a, b, c) {
 *   console.log({a, b, c});
 * });
 *
 * on('myEvent', callback);
 * emit('myEvent', 1, 2, 3);  //=> {a: 1, b: 2, c: 3}
 * off('myEvent', callback);
 * ```
 * @sectionName Events
 */
public static class Events {
  // expose for testing
  internal static IDictionary<string, Delegates> Callbacks { get; } = new Dictionary<string, Delegates>();

  /**
   * There are currently only three lifecycle events:
   * - `init` - Emitted after `kontra.init()` is called.
   * - `tick` - Emitted every frame of [GameLoop](api/gameLoop) before the loops `update()` and `render()` functions are called.
   * - `assetLoaded` - Emitted after an asset has fully loaded using the asset loader. The callback function is passed the asset and the url of the asset as parameters.
   * @sectionName Lifecycle Events
   */

  /**
   * Register a callback for an event to be called whenever the event is emitted. The callback will be passed all arguments used in the `emit` call.
   * @function on
   *
   * @param {String} event - Name of the event.
   * @param {Function} callback - Function that will be called when the event is emitted.
   */
  public static void On(string @event, Delegate callback) {
    if (Callbacks.ContainsKey(@event)) {
      Callbacks[@event].Add(callback);
    }
    else {
      Callbacks.Add(@event, new Delegates { callback });
    }
  }

  /**
   * Remove a callback for an event.
   * @function off
   *
   * @param {String} event - Name of the event.
   * @param {Function} callback - The function that was passed during registration.
   */
  public static void Off(string @event, Delegate callback) {
    if (Callbacks.ContainsKey(@event)) {
      Callbacks[@event].Remove(callback);
    }
  }

  /**
   * Call all callback functions for the event. All arguments will be passed to the callback functions.
   * @function emit
   *
   * @param {String} event - Name of the event.
   * @param {...*} args - Comma separated list of arguments passed to all callbacks.
   */
  public static void Emit(string @event, params object?[]? args) {
    if (Callbacks.ContainsKey(@event)) {
      foreach (var callback in Callbacks[@event]) {
        callback.DynamicInvoke(args);
      }
    }
  }
}