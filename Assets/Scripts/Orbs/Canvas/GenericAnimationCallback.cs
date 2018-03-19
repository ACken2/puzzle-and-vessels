using System;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// Generic class that allow a class to raise event AnimationCompleted once an animation is completed
    /// </summary>
    /// <note>
    /// This class should be used as a composite in MonoBehaviour-inherited classes
    /// </note>
    public class GenericAnimationCallback {

        /// <summary>
        /// Event raised when the transition is completed
        /// </summary>
        public event EventHandler AnimationCompleted;

        /// <summary>
        /// Boolean storing whether event listener should be erased once the event is raised
        /// </summary>
        private bool clearOnRaise;

        /// <summary>
        /// Instantiate this instance
        /// </summary>
        /// <param name="clearListener">If true, all listener is reset every time the event is raised</param>
        public GenericAnimationCallback(bool clearListener) {
            clearOnRaise = clearListener;
        }

        /// <summary>
        /// Raise AnimationCompleted event once the animation is completed
        /// </summary>
        /// <param name="e">Empty event arguments</param>
        public virtual void OnAnimationCompleted(EventArgs e) {
            EventHandler handler = AnimationCompleted;
            if (handler != null) {
                handler(this, e);
            }
            if (clearOnRaise) {
                AnimationCompleted = null; // Clear listener
            }
        }

    }

}
