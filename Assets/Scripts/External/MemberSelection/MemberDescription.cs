using UnityEngine;

namespace Assets.Scripts.External.MemberSelection {

    /// <summary>
    /// UI class for controlling the description of the members
    /// </summary>
    public class MemberDescription : MonoBehaviour {

        /// <summary>
        /// Allowed static access
        /// </summary>
        public static MemberDescription instance;

        /// <summary>
        /// Box collider for the description
        /// </summary>
        private BoxCollider2D bc2d;
        /// <summary>
        /// Core gameobject of the description
        /// </summary>
        private GameObject core;
        /// <summary>
        /// Image instance that actually show the description
        /// </summary>
        private Orbs.Canvas.SwitchableImage description;

        /// <summary>
        /// Initialize references
        /// </summary>
        public void Start() {
            instance = this;
            core = transform.GetChild(0).gameObject;
            description = transform.GetChild(0).GetComponentInChildren<Orbs.Canvas.SwitchableImage>();
            bc2d = GetComponent<BoxCollider2D>();
            core.SetActive(false);
        }

        /// <summary>
        /// Show the description image
        /// </summary>
        /// <param name="uuid">UUID of the member to be shown in description</param>
        public void ShowDescription(string uuid) {
            bc2d.enabled = true; // Enable box collider to allow user to click away
            core.SetActive(true);
            description.SwitchImage("CharacterDescription/" + uuid);
        }

        /// <summary>
        /// Triggered when the description image is clicked on, which return to the member selection screen
        /// </summary>
        public void OnMouseUp() {
            // Play SFX
            Common.SoundSystem.instance.PlayTapBackSFX();
            core.SetActive(false);
            bc2d.enabled = false; // Disable box collider once we are done
        }

    }

}
