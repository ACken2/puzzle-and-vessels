using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.External.MemberSelection {

    /// <summary>
    /// UI class controlling the OK button in member selection
    /// </summary>
    public class Confirm : MonoBehaviour {

        /// <summary>
        /// Load the selected game with the selected team member
        /// </summary>
        public void OnClick() {
            // Final check that at least 1 member is chosen
            if (Orbs.Coordinator.TeamManager.GetMembers().Count != 0) {
                // Load Game
                // Play SFX
                Common.SoundSystem.instance.PlayTapSFX();
                // Fade the scene out
                Common.Fader.instance.FadeOut();
                // Load scene async
                StartCoroutine(LoadGameCore());
            }
            else {
                // Play SFX
                Common.SoundSystem.instance.PlayTapErrorSFX();
            }
        }

        /// <summary>
        /// Load GameCore in async manner
        /// </summary>
        /// <returns>Null when the scene is loaded</returns>
        IEnumerator LoadGameCore() {
            // Loads the Scene GameCore in the background at the same time as the current Scene.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameCore");
            //Wait until the last operation fully loads to return anything
            while (!asyncLoad.isDone) {
                yield return null;
            }
        }

    }

}
