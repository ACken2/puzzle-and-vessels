using UnityEngine;

namespace Assets.Scripts.External.Common {

    /// <summary>
    /// Classes for controlling the sound system in GameExternal scene
    /// </summary>
    public class SoundSystem : MonoBehaviour {

        /// <summary>
        /// Allowed static access
        /// </summary>
        public static SoundSystem instance;

        /// <summary>
        /// Audio source for tap SFX
        /// </summary>
        public AudioSource tap;
        /// <summary>
        /// Audio source for tap back SFX
        /// </summary>
        public AudioSource tapBack;
        /// <summary>
        /// Audio source for tap error
        /// </summary>
        public AudioSource tapError;

        /// <summary>
        /// Setup references
        /// </summary>
        public void Start() {
            instance = this;
        }

        /// <summary>
        /// Play the tap SFX
        /// </summary>
        public void PlayTapSFX() {
            tap.Play();
        }

        /// <summary>
        /// Play the tap back SFX
        /// </summary>
        public void PlayTapBackSFX() {
            tapBack.Play();
        }

        /// <summary>
        /// Play the error SFX
        /// </summary>
        public void PlayTapErrorSFX() {
            tapError.Play();
        }

    }

}
