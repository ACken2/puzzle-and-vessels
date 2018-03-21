using UnityEngine;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class for controlling the lost dialog box
    /// </summary>
    public class LostDialogBox : MonoBehaviour {

        /// <summary>
        /// Allow static access
        /// </summary>
        public static LostDialogBox instance;

        /// <summary>
        /// Gameobject reference to the actual text box
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
        /// Show the end game dialog box
        /// </summary>
        /// <param name="finalMessage">Final message before player leave the game</param>
        public void EndGame() {
            core.SetActive(true);
            Coordinator.Coordinator.NotifyDialogActive(); // Block further input from player
        }

        /// <summary>
        /// Triggered when player click the confirm button
        /// </summary>
        public void OnConfirmEndGame() {
            Coordinator.Coordinator.NotifyEndGame();
        }

    }

}
