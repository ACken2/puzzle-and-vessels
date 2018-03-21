using UnityEngine;
using UnityEngine.SceneManagement;

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
        /// Function triggered when the button is clicked
        /// </summary>
        public void OnClick() {
            // Set game index to be loaded
            Orbs.Coordinator.StageManager.gameIndex = gameIndex;
            // Fade the scene out
            Common.Fader.instance.FadeOut();
            // Moved to the next game core scene, and ask it to load as soon as possible
            SceneManager.LoadSceneAsync("GameCore").allowSceneActivation = true;
        }

    }

}
