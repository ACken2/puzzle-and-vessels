using UnityEngine;

namespace Assets.Scripts.External.MemberSelection {

    /// <summary>
    /// UI class for controlling the back button
    /// </summary>
    public class Back : MonoBehaviour {

        /// <summary>
        /// GameObject for selecting game
        /// </summary>
        public GameObject gameSelect;
        /// <summary>
        /// GameObject for selecting member
        /// </summary>
        public GameObject memberSelect;

        /// <summary>
        /// Go back to game selection screen
        /// </summary>
        public void OnClick() {
            // Play SFX
            Common.SoundSystem.instance.PlayTapBackSFX();
            // Go back to game selection screen
            memberSelect.SetActive(false);
            gameSelect.SetActive(true);
        }

    }

}
