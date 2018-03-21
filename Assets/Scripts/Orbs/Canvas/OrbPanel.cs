using UnityEngine;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class for Orb Panel
    /// </summary>
    public class OrbPanel : MonoBehaviour {

        /// <summary>
        /// Allow static access
        /// </summary>
        public static OrbPanel instance;

        /// <summary>
        /// Animator of OrbPanel
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
        /// Play the failed animation of orb falling down
        /// </summary>
        public void PlayFailAnimation() {
            animator.SetBool("failed", true);
        }

    }

}
