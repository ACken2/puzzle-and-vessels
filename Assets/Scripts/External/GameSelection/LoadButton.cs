using UnityEngine;

namespace Assets.Scripts.External.GameSelection {

    /// <summary>
    /// UI class for controlling the game selection button
    /// </summary>
    public class LoadButton : MonoBehaviour {

        /// <summary>
        /// Game index to be loaded when this button is clicked
        /// </summary>
        public int gameIndex = 0;

        /// <summary>
        /// GameObject for the cleared text
        /// </summary>
        public GameObject clearText;

        /// <summary>
        /// GameObject for selecting game
        /// </summary>
        public GameObject gameSelect;
        /// <summary>
        /// GameObject for selecting member
        /// </summary>
        public GameObject memberSelect;

        /// <summary>
        /// Function triggered when the button is clicked
        /// </summary>
        public void OnMouseUp() {
            // Play SFX
            Common.SoundSystem.instance.PlayTapSFX();
            // Set game index to be loaded
            Orbs.Coordinator.StageManager.gameIndex = gameIndex;
            // Show member selection object and hide game select object
            memberSelect.SetActive(true);
            gameSelect.SetActive(false);
        }

        /// <summary>
        /// Show the clear text for this game
        /// </summary>
        public void ShowClearText() {
            clearText.SetActive(true);
        }

    }

}
