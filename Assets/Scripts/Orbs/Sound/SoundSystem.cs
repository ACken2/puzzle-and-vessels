using UnityEngine;

namespace Assets.Scripts.Orbs.Sound {

    /// <summary>
    /// Control scrip for the SoundSystem GameObject in scene
    /// </summary>
    public class SoundSystem: MonoBehaviour {

        /// <summary>
        /// Allow static access
        /// </summary>
        public static SoundSystem instance;

        /// <summary>
        /// Sound player of the background music
        /// </summary>
        public AudioSource bgm;

        /// <summary>
        /// Template for the OrbEliminationTemplateSFX sound player
        /// </summary>
        public OrbEliminationSFX orbEliminationTemplate;
        /// <summary>
        /// Starting pitch for the elimination sound
        /// </summary>
        public int startingPitch = 1;
        /// <summary>
        /// Increment in pitch per combo
        /// </summary>
        public float incrementPitch = 0.1f;

        /// <summary>
        /// Sound player for OrbMovementSFX
        /// </summary>
        public AudioSource orbMovement;

        /// <summary>
        /// Sound player for SkillReadySFX
        /// </summary>
        public AudioSource skillReady;
        /// <summary>
        /// Sound player for SkillUseSFX
        /// </summary>
        public AudioSource skillUse;
        /// <summary>
        /// Sound player for AttackSFX
        /// </summary>
        public AudioSource attack;
        /// <summary>
        /// Sound player for StageTransitionSFX
        /// </summary>
        public AudioSource stageTransition;
        /// <summary>
        /// Sound player for StageClearSFX
        /// </summary>
        public AudioSource stageClear;
        /// <summary>
        /// Sound player for StageFailedSFX
        /// </summary>
        public AudioSource stageFailed;
        /// <summary>
        /// Sound player for movement timeout
        /// </summary>
        public AudioSource movementTimeout;

        /// <summary>
        /// Initialize the class
        /// </summary>
        public void Start() {
            // Set static access
            instance = this;
            // Set the template to have eternal life
            orbEliminationTemplate.SetEternal();
            // Play BGM after 4 second
            bgm.PlayDelayed(4);
        }

        /// <summary>
        /// Play a orb elimination sound
        /// </summary>
        /// <param name="combo">Number of combo this sound represent</param>
        /// <param name="delay">Delay in second for the sound to be played</param>
        public void playComboSFX(int combo, float delay) {
            // Clone the template, since you cannot play multiple sound at the same time with just 1 single player
            OrbEliminationSFX oesfx = Instantiate(orbEliminationTemplate);
            // Play the SFX at certain pitch and delay on the cloned sound system
            oesfx.playSound(startingPitch + incrementPitch * combo, delay);
        }

        /// <summary>
        /// Play the SFX for player movement
        /// </summary>
        public void playMovementSFX() {
            orbMovement.Play();
        }

        /// <summary>
        /// Play the SFX for skill ready
        /// </summary>
        public void playSkillReady() {
            skillReady.Play();
        }

        /// <summary>
        /// Play the SFX for skill use
        /// </summary>
        public void playSkillUse() {
            skillUse.Play();
        }

        /// <summary>
        /// Play the SFX for stage transition and attack
        /// </summary>
        public void playTransition() {
            attack.Play();
            stageTransition.PlayDelayed(1); // Play transition 1 second later
        }

        /// <summary>
        /// Play the SFX for stage clear and pause BGM
        /// </summary>
        public void playStageClear() {
            bgm.Pause();
            stageClear.Play();
        }

        /// <summary>
        /// Play the SFX for stage failed and pause BGM
        /// </summary>
        public void playStageFail() {
            bgm.Pause();
            stageFailed.Play();
        }

        /// <summary>
        /// Play the SFX for movement failure
        /// </summary>
        public void playMovementTimeout() {
            movementTimeout.Play();
        }

    }

}
