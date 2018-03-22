using UnityEngine;

namespace Assets.Scripts.External.GameSelection {

    /// <summary>
    /// UI class for controlling all load button in unison
    /// </summary>
    public class LoadButtonMaster : MonoBehaviour {

        /// <summary>
        /// Static variable storing the current completed stage, must be set before scene load
        /// </summary>
        public static int currentProgress = 0;

        /// <summary>
        /// Reference to the button that load stage 1
        /// </summary>
        public LoadButton stage1;
        /// <summary>
        /// Reference to the button that load stage 2
        /// </summary>
        public LoadButton stage2;
        /// <summary>
        /// Reference to the button that load stage 3
        /// </summary>
        public LoadButton stage3;

        /// <summary>
        /// Initialize the LoadButton according to the current setting
        /// </summary>
        public void Start() {
            if (currentProgress == 0) {
                // Activate only stage 1
                stage1.transform.position = stage3.transform.position;
                stage1.gameObject.SetActive(true);
            }
            else if (currentProgress == 1) {
                // Activate stage 2 and stage 1 button
                stage1.transform.position = stage2.transform.position;
                stage2.transform.position = stage3.transform.position;
                stage1.gameObject.SetActive(true);
                stage2.gameObject.SetActive(true);
            }
            else {
                // Activate all buttons
                stage1.gameObject.SetActive(true);
                stage2.gameObject.SetActive(true);
                stage3.gameObject.SetActive(true);
            }
        }

    }

}
