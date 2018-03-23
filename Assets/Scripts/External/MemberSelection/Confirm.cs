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
                // Moved to the next game core scene, and ask it to load as soon as possible
                SceneManager.LoadSceneAsync("GameCore").allowSceneActivation = true;
            }
        }

    }

}
