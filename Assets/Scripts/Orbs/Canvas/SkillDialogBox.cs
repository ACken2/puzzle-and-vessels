using System;
using UnityEngine;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class handling the SkillDialogBox
    /// </summary>
    public class SkillDialogBox: MonoBehaviour {

        /// <summary>
        /// Event raised when player click confirm
        /// </summary>
        public event EventHandler ConfirmSkill;

        /// <summary>
        /// Allow static access
        /// </summary>
        public static SkillDialogBox instance;

        /// <summary>
        /// Core GameObject that holds the actual dialog box
        /// </summary>
        private GameObject core;
        /// <summary>
        /// Image component of the Description
        /// </summary>
        private SwitchableImage description;

        /// <summary>
        /// Initialize this class
        /// </summary>
        public void Start() {
            // Setup references
            instance = this;
            core = transform.GetChild(0).gameObject;
            description = core.GetComponentInChildren<SwitchableImage>();
        }

        /// <summary>
        /// Display the skill activation dialog for a specific skill
        /// </summary>
        /// <param name="uuid">UUID of the skill</param>
        public void displaySkill(string uuid) {
            // Notify the dialog is now active
            Coordinator.Coordinator.NotifyDialogActive();
            // Show the dialog box with the switched image
            core.SetActive(true);
            description.SwitchImage("SkillDescription/" + uuid);
        }

        /// <summary>
        /// OnClick for the confirm button
        /// </summary>
        public void OnConfirm() {
            // Play SFX
            Sound.SoundSystem.instance.PlayTapSFX();
            // Deactivate the Core object
            core.SetActive(false);
            // Raise Event ConfirmSkill
            OnConfirmSkill(EventArgs.Empty);
            // Reset event listener
            ConfirmSkill = null;
            // Notify the dialog is removed
            Coordinator.Coordinator.NotifyDialogDeactive();
        }

        /// <summary>
        /// OnClick for the cancel button
        /// </summary>
        public void OnCancel() {
            // Play SFX
            Sound.SoundSystem.instance.PlayTapBackSFX();
            // Deactivate the Core object
            core.SetActive(false);
            // Reset event listener
            ConfirmSkill = null;
            // Notify the dialog is removed
            Coordinator.Coordinator.NotifyDialogDeactive();
        }

        /// <summary>
        /// Raise ConfirmSkill event once we are done
        /// </summary>
        /// <param name="e">Empty event arguments</param>
        protected virtual void OnConfirmSkill(EventArgs e) {
            EventHandler handler = ConfirmSkill;
            if (handler != null) {
                handler(this, e);
            }
        }

    }

}
