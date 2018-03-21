using UnityEngine;

namespace Assets.Scripts.Orbs.Canvas {

    public class Fader : MonoBehaviour {

        /// <summary>
        /// Allow static access
        /// </summary>
        public static Fader instance;

        /// <summary>
        /// Animator of Fader gameobject
        /// </summary>
        private Animator animator;
        
        /// <summary>
        /// Initialize references
        /// </summary>
        public void Start() {
            instance = this;
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Begin fade out animation of the scene, called by Coordinator to prepare for scene change
        /// </summary>
        public void FadeOut() {
            animator.SetBool("fadeOut", true);
        }

        /// <summary>
        /// Triggered when fade out animation is completed
        /// </summary>
        public void OnFadeOutDone() {
            Coordinator.Coordinator.NotifyFadedOut(); // Notify Coordinator
        }

    }

}
