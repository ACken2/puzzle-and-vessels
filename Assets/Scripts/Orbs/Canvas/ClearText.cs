using UnityEngine;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class for controlling the ClearText
    /// </summary>
    public class ClearText : MonoBehaviour {

        /// <summary>
        /// Allow static reference
        /// </summary>
        public static ClearText instance;

        /// <summary>
        /// Gameobject reference of the core game object
        /// </summary>
        private GameObject core;

        /// <summary>
        /// Initialize references
        /// </summary>
        public void Start() {
            instance = this;
            core = transform.GetChild(0).gameObject;
        }

        /// <summary>
        /// Activate the clear text
        /// </summary>
        public void DisplayClearText() {
            core.SetActive(true);
        }

    }

}
