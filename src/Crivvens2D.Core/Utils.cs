public static class Utils {

  public static readonly Action noop = () => { };

  // style used for DOM nodes needed for screen readers
  // DOTNETIFY: No Dom

  /**
   * Append a node directly after the canvas and as the last element of other kontra nodes.
   *
   * @param {HTMLElement} node - Node to append.
   * @param {HTMLCanvasElement} canvas - Canvas to append after.
   */
  // DOTNETIFY: No Dom

  /**
   * Remove an item from an array.
   *
   * @param {*[]} array - Array to remove from.
   * @param {*} item - Item to remove.
   *
   * @returns {Boolean|undefined} True if the item was removed.
   */
  public static bool RemoveFromArray<T>(ICollection<T> array, T item) {
    return array.Remove(item);
  }
}
