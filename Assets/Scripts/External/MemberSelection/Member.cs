using UnityEngine;

namespace Assets.Scripts.External.MemberSelection {

    /// <summary>
    /// UI class for controlling the a selectable member in the panel
    /// </summary>
    public class Member : MonoBehaviour {

        /// <summary>
        /// UUID that this instance represent
        /// </summary>
        public string memberUUID;
        /// <summary>
        /// Mask that will be displayed if this character is chosen
        /// </summary>
        public GameObject selectMask;

        /// <summary>
        /// Float that keep track of the time that player click down onto the member
        /// </summary>
        private float mouseDownTime = -1;

        /// <summary>
        /// Called every frame to detect long-press
        /// </summary>
        public void Update() {
            // Check if OnMouseDown is called and 1s has elapsed since then without OnMouseUp
            if (mouseDownTime != -1 && Time.time - mouseDownTime >= 1) {
                // Play SFX
                Common.SoundSystem.instance.PlayTapSFX();
                // Long-pressed detected, show description
                MemberDescription.instance.ShowDescription(memberUUID);
                // Reset mouseDownTime
                mouseDownTime = -1;
            }
        }

        /// <summary>
        /// Called when player click on the member
        /// </summary>
        public void OnMouseDown() {
            // Record the time that the player first click on the member
            mouseDownTime = Time.time;
        }

        /// <summary>
        /// Called when player release click
        /// </summary>
        public void OnMouseUp() {
            // Check if Update already reset the mouseDownTime
            if (mouseDownTime != -1) {
                // If not, it is considered a click
                if (selectMask.activeSelf) {
                    // Play SFX
                    Common.SoundSystem.instance.PlayTapSFX();
                    // Disable selection
                    Orbs.Coordinator.TeamManager.RemoveMember(memberUUID);
                    // Disable selected mask if chosen
                    selectMask.SetActive(false);
                }
                else {
                    // Try to add the member to team member selection
                    if (Orbs.Coordinator.TeamManager.AddMember(memberUUID)) {
                        // Play SFX
                        Common.SoundSystem.instance.PlayTapSFX();
                        // Enable selected mask if successful
                        selectMask.SetActive(true);
                    }
                    else {
                        // Play SFX
                        Common.SoundSystem.instance.PlayTapErrorSFX();
                    }
                }
                // Reset mouseDownTime
                mouseDownTime = -1;
            }
        }

    }

}
