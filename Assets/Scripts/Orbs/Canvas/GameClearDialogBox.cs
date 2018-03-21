using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class for controlling the game clear dialog box
    /// </summary>
    public class GameClearDialogBox : MonoBehaviour {

        /// <summary>
        /// Allow static access
        /// </summary>
        public static GameClearDialogBox instance;

        /// <summary>
        /// Gameobject reference to the actual text box
        /// </summary>
        private GameObject core;
        /// <summary>
        /// UI text that shows the description of the clear message box
        /// </summary>
        private Text description;

        /// <summary>
        /// Initialize references
        /// </summary>
        public void Start() {
            instance = this;
            core = transform.GetChild(0).gameObject;
            description = core.GetComponentsInChildren<Text>()[1];
        }

        /// <summary>
        /// Show the end game dialog box
        /// </summary>
        /// <param name="finalMessage">Final message before player leave the game</param>
        public void EndGame(string finalMessage) {
            description.text = finalMessage;
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
