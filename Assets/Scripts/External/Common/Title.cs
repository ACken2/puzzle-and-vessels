using UnityEngine;

namespace Assets.Scripts.External.Common {

    /// <summary>
    /// UI class for controlling the title page
    /// </summary>
    public class Title : MonoBehaviour {

        /// <summary>
        /// Boolean that keep track of whether the title screen has already been shown
        /// </summary>
        public static bool titleShown = false;

        /// <summary>
        /// Destroy this game object if the title has already been shown
        /// </summary>
        public void Start() {
            if (titleShown) {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Triggered when the title art is clicked, begin fade out aniamtion
        /// </summary>
        public void OnTitleClicked() {
            GetComponent<Animator>().SetBool("clicked", true);
        }

        /// <summary>
        /// Triggered when the fade out animation was completed
        /// </summary>
        public void OnFaded() {
            titleShown = true;
            Destroy(gameObject);
        }

    }

}
