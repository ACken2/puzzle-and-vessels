using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI script for controling text that display the number of Combo
    /// </summary>
    public class ComboText : MonoBehaviour {

        /// <summary>
        /// Delay in seconds for the animation to begin
        /// </summary>
        private float delaySet = -1;
        /// <summary>
        /// Delay counter to increment till the animation has begun
        /// </summary>
        private float delayTimer = 0;

        /// <summary>
        /// Animator instance of this ComboText instance
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Factory method for building a ComboText instance
        /// </summary>
        /// <param name="template">Template GameObject to based on</param>
        /// <param name="parent">Transform of the Canvas parent</param>
        /// <param name="position">Position of the ComboText instance</param>
        /// <param name="delay">Delay in second before the animation begin</param>
        /// <param name="text">Text to be displayed</param>
        /// <returns>Built ComboText instance</returns>
        public static ComboText Factory(GameObject template, Transform parent, Vector3 position, float delay, string text) {
            GameObject cloned = Instantiate(template);
            cloned.transform.SetParent(parent, false);
            cloned.transform.position = position;
            cloned.transform.localScale = template.transform.localScale;
            ComboText clonedComboText = cloned.GetComponentInChildren<ComboText>();
            clonedComboText.setDelay(delay);
            clonedComboText.GetComponent<Text>().text = text;
            cloned.SetActive(true);
            return clonedComboText;
        }

        /// <summary>
        /// Instiantiate the ComboText instance
        /// </summary>
        public void Start() {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Called every frame, used only before the animation is activated
        /// </summary>
        public void Update() {
            // Look for any delay set on this ComboText
            if (delaySet != -1) {
                // Waiting for animation to be activated
                if (delayTimer < delaySet) {
                    delayTimer += Time.deltaTime;
                }
                else {
                    // Delay reached, activate animation
                    animator.SetBool("Activated", true);
                    // Reset delaySet
                    delaySet = -1;
                }
            }
        }

        /// <summary>
        /// Set the delay in second that this instance should wait before starting the animation
        /// </summary>
        /// <param name="delay">Delay in second</param>
        public void setDelay(float delay) {
            delaySet = delay;
        }

        /// <summary>
        /// End the animation
        /// </summary>
        public void endAnimation() {
            animator.SetBool("End", true);
        }

        /// <summary>
        /// Event called when the End animation finished, used to destroy the ComboText
        /// </summary>
        public void OnAnimationEnded() {
            Destroy(transform.parent.gameObject);
        }

    }

}
