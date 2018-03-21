using UnityEngine;

namespace Assets.Scripts.Data {

    /// <summary>
    /// Initialize the Data classes, should be run when the game launch once only
    /// </summary>
    public class DataInit : MonoBehaviour {

        /// <summary>
        /// Boolean stating whether the data has already been initialized
        /// </summary>
        private static bool initialized = false;

        /// <summary>
        /// Initialize data classes and then self destruct
        /// </summary>
        public void Start() {
            // Initialize if not yet do so
            if (!initialized) {
                Games.LoadGame();
                Skills.LoadSkill();
                Members.LoadMember();
                initialized = true;
            }
            Destroy(this);
        }

    }

}
