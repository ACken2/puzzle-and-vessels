using UnityEngine;

namespace Assets.Scripts.External.Common {

    /// <summary>
    /// UI class for controlling the Fader in GameExternal
    /// </summary>
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
        /// Begin fade out animation of the GameExternal scene
        /// </summary>
        public void FadeOut() {
            animator.SetBool("fadeOut", true);
        }

    }

}
