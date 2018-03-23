using System;
using UnityEngine;

namespace Assets.Scripts.Orbs.Core {

    /// <summary>
    /// Script attached to dummy orb that are used for fade out or translation animation
    /// </summary>
    public class DummyOrb : MonoBehaviour {

        /// <summary>
        /// Event raised when the animation is completed
        /// </summary>
        public event EventHandler AnimationDone;

        /// <summary>
        /// Sprite to be displayed for this dummy orb
        /// </summary>
        private SpriteRenderer sprite = null;

        /// <summary>
        /// Delay for starting the fade out animation
        /// </summary>
        private float delayFadeout = -1;
        /// <summary>
        /// Count up timer unil we reach the delay in fade out animation
        /// </summary>
        private float delayFadeoutTimer = 0;
        /// <summary>
        /// Changes in opacity per update during the fade out animation
        /// </summary>
        private Color fadePerUpdate = new Color(0, 0, 0, 0.1f);

        /// <summary>
        /// Store the Vector3 for the target destination of the translation animation
        /// </summary>
        private Vector3 translateDest = Vector3.zero;
        /// <summary>
        /// Magnitude of translation per update
        /// </summary>
        private Vector3 translatePerUpdate;
        /// <summary>
        /// Number of frame that the translate animation shoule take
        /// </summary>
        private float translateConstant = 18f;

        /// <summary>
        /// Static method for building a DummyOrb instance
        /// </summary>
        /// <param name="position">Position of the DummyOrb instance</param>
        /// <param name="sprite">Sprite that the DummyOrb should display</param>
        /// <param name="scale">Scale ratio that the DummyOrb should display the Sprite with</param>
        /// <returns></returns>
        public static DummyOrb Factory(Vector3 position, Sprite sprite, Vector3 scale) {
            // Spawn dummyOrb at the same location with the orb with identical sprite
            GameObject dummyOrb = new GameObject();
            dummyOrb.transform.position = position;
            dummyOrb.transform.localScale = scale;
            SpriteRenderer sr = dummyOrb.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;
            // Attaching DummyOrb script to dummyOrb and Add the dummyOrb into dummyOrbs array
            return dummyOrb.AddComponent<DummyOrb>();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        public void Start() {
            sprite = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Call method to do animation if appropriate
        /// </summary>
        public void Update() {
            if (delayFadeout >= 0 && delayFadeoutTimer <= delayFadeout) {
                // Fade out animation is requested, but with delay set that has not been reached
                // Increment the count up timer
                delayFadeoutTimer += Time.deltaTime;
            }
            else if (delayFadeout >= 0 && delayFadeoutTimer > delayFadeout) {
                // Do fade out animation
                fade();
            }
            if (translateDest.magnitude > 0) {
                // Do translate animation
                translate();
            }
        }

        /// <summary>
        /// Request this instance to do a fade out animation
        /// </summary>
        /// <param name="delay">Delay in miliseconds that the Orb should wait before beginning the animation</param>
        public void StartFadeOut(float delay) {
            delayFadeout = delay;
        }

        /// <summary>
        /// Request this instance to do translation animation
        /// </summary>
        /// <param name="destination">Destination position the DummyOrb should travel to</param>
        public void StartTranslate(Vector3 destination) {
            translateDest = destination;
            translatePerUpdate = (translateDest - transform.position) / 20f;
        }

        /// <summary>
        /// Handle the fade out animation
        /// </summary>
        private void fade() {
            if (sprite.color.a >= 0) {
                // Continue fade out animation
                sprite.color -= fadePerUpdate;
            }
            else {
                // Fade out animation done
                delayFadeout = -1;
                delayFadeoutTimer = 0;
                // Raise AnimationDone event
                OnAnimationDone(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handle the translate animation
        /// </summary>
        private void translate() {
            Vector3 seperation = transform.position - translateDest;
            if (seperation.magnitude > translatePerUpdate.magnitude) {
                // Translate toward the destination if we haven't reached it yet
                transform.Translate(translatePerUpdate);
            }
            else {
                // Animation done
                // Set the DummyOrb to be on exactly the destination location
                transform.position = translateDest;
                // Reset translate animation destination
                translateDest = Vector3.zero;
                // Raise AnimationDone event
                OnAnimationDone(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raise event when animation is completed
        /// </summary>
        /// <param name="e">Empty event arguments</param>
        protected virtual void OnAnimationDone(EventArgs e) {
            EventHandler handler = AnimationDone;
            if (handler != null) {
                handler(this, e);
            }
        }

    }

}
