using UnityEngine;

namespace Assets.Scripts.Orbs.Sound {

    /// <summary>
    /// Sound script that used to play the combo soundtrack
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class OrbEliminationSFX: MonoBehaviour {

        /// <summary>
        /// Timer for self destruct
        /// </summary>
        private float selfdestruct = 0;
        /// <summary>
        /// Maximum lifespan of this instance
        /// </summary>
        private float selfdestructLimit = 1;

        /// <summary>
        /// Set this instacne to not self destruct
        /// </summary>
        public void SetEternal() {
            selfdestructLimit = float.MaxValue;
        }

        /// <summary>
        /// Check for self destruction per frame till destructed
        /// </summary>
        public void Update() {
            if (selfdestruct < selfdestructLimit) {
                selfdestruct += Time.deltaTime;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Play the combo sound
        /// </summary>
        /// <param name="pitch">Calculated pitch to be played</param>
        /// <param name="delay">Delay in second before the sound is played</param>
        public void playSound(float pitch, float delay) {
            GetComponent<AudioSource>().pitch = pitch;
            GetComponent<AudioSource>().PlayDelayed(delay);
            selfdestructLimit += delay; // Prolong this instance for delay seconds
        }

    }

}
