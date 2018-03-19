using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class that supports UI image to be swapped
    /// </summary>
    public class SwitchableImage : MonoBehaviour {

        /// <summary>
        /// UI image component for this component
        /// </summary>
        private Image image;

        /// <summary>
        /// Initialize this class
        /// </summary>
        public void Start() {
            image = GetComponent<Image>();
        }

        /// <summary>
        /// Load a sprite in resources folder and change the sprite to the loaded image
        /// </summary>
        /// <param name="spritePath">File path to the desired sprite</param>
        public void SwitchImage(string spritePath) {
            Sprite loadedSprite = Resources.Load<Sprite>(spritePath);
            if (loadedSprite != null) {
                image.sprite = loadedSprite;
            }
            else {
                image.sprite = Resources.Load<Sprite>("MISSING_SPRITE");
            }
        }

    }

}
