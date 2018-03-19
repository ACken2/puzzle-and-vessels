using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class responsible for displaying the stage transition text
    /// </summary>
    public class StageText : MonoBehaviour {

        /// <summary>
        /// Allow static access
        /// </summary>
        public static StageText instance;
        /// <summary>
        /// Allow EventListener to be attached to AnimationCompleted event
        /// </summary>
        public GenericAnimationCallback animationEvent = new GenericAnimationCallback(true);

        /// <summary>
        /// UI text component of this instance
        /// </summary>
        private Text text;
        /// <summary>
        /// Animator of this instance
        /// </summary>
        private Animator textAnimator;

        /// <summary>
        /// Initialize references
        /// </summary>
        public void Start() {
            instance = this;
            text = GetComponent<Text>();
            textAnimator = GetComponent<Animator>();
        }

        /// <summary>
        /// Play the text transition event
        /// </summary>
        /// <param name="currentStage">Current stage number</param>
        /// <param name="maxStage">Maximum number of stage</param>
        public void Transition(int currentStage, int maxStage) {
            // Set the appropriate text to be displayed
            text.text = "Stage " + (currentStage + 1).ToString() + "/" + maxStage.ToString();
            // Activate the animation
            textAnimator.SetBool("active", true);
        }

        /// <summary>
        /// Triggered when animation has been completed
        /// </summary>
        public void OnAnimationDone() {
            // Deactivate animation
            textAnimator.SetBool("active", false);
            // Raise event
            animationEvent.OnAnimationCompleted(EventArgs.Empty);
        }

        

    }

}
