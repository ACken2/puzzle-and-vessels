using UnityEngine;

namespace Assets.Scripts.Orbs.Sound {

    public class SoundSystem: MonoBehaviour {

        public static SoundSystem instance;

        public OrbEliminationSFX orbEliminationTemplate;
        public int startingPitch = 1;
        public float incrementPitch = 0.1f;

        public AudioSource orbMovement;

        public void Start() {
            instance = this;
            orbEliminationTemplate.SetEternal();
        }

        public void playComboSFX(int combo, float delay) {
            OrbEliminationSFX oesfx = Instantiate(orbEliminationTemplate);
            oesfx.playSound(startingPitch + incrementPitch * combo, delay);
        }

        public void playMovementSFX() {
            orbMovement.Play();
        }

    }

}
