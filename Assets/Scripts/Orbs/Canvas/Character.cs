using System;
using UnityEngine;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class responsible for the character to be attached to the 'Core' object of the character
    /// </summary>
    public class Character: MonoBehaviour {

        /// <summary>
        /// Skill UUID that this character can use
        /// </summary>
        public string skill;

        /// <summary>
        /// Boolean that store whether the character can use a skill
        /// </summary>
        private bool skillReady = false;

        /// <summary>
        /// Particle system for spawning particle when a skill is used
        /// </summary>
        private ParticleSystem particle;
        /// <summary>
        /// Animator for moving the sprite up or down as skill become available or unavailable
        /// </summary>
        private Animator coreAnimator;
        /// <summary>
        /// Animator for flashing background when skill become available
        /// </summary>
        private Animator bgAnimator;
        /// <summary>
        /// Switchable image for the character sprite
        /// </summary>
        private SwitchableImage switchableImage;

        /// <summary>
        /// Number of particle to be spawned when a skill is usd
        /// </summary>
        private readonly int numParticle = 15;
        /// <summary>
        /// UUID of a special skill that prolong movement to 12 seconds
        /// </summary>
        private readonly string christopherSkill = "85ea6797-f2bd-4082-b9f3-fdbc9b9e2ec6";

        /// <summary>
        /// Initialize the instance
        /// </summary>
        public void Start() {
            // Initialize private variable and grab components needed
            particle = transform.parent.GetComponent<ParticleSystem>();
            coreAnimator = GetComponent<Animator>();
            bgAnimator = transform.GetChild(0).GetComponentInChildren<Animator>();
            switchableImage = transform.GetChild(0).GetChild(0).GetComponentInChildren<SwitchableImage>();
            // Register
            Coordinator.Coordinator.RegisterCharacter(this);
            // Start all character with skill ready
            SkillReady();
        }

        /// <summary>
        /// Initialize this character sprite and skill
        /// </summary>
        /// <param name="uuid">UUID of the character that this instance should represent</param>
        public void InitSprite(string uuid) {
            if (uuid != null) {
                // Switch image to that of the desired sprite if given
                switchableImage.SwitchImage("Character/" + uuid);
            }
            else {
                // Switch off this instance as we don't need them
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Activated when the character sprite is clicked which trigger skill activation
        /// </summary>
        public void OnMouseUpAsButton() {
            // Check if skill is ready to be used, and if Coordinator allow us to use skill
            // Special skill can bypass the limitation
            if (skillReady && (Coordinator.Coordinator.RequestSkillsActivation() || skill == christopherSkill)) {
                // Play SFX
                Sound.SoundSystem.instance.PlayTapSFX();
                // Register with ConfirmSkill event
                SkillDialogBox.instance.ConfirmSkill += SkillActivate;
                SkillDialogBox.instance.displaySkill(skill);
            }
            else {
                // Play SFX
                Sound.SoundSystem.instance.PlayTapErrorSFX();
            }
        }

        /// <summary>
        /// Called on every end turn to try to reactivate the skill of this character
        /// </summary>
        public void ReactivateSkill() {
            // No reactivation if the skill is a special skill
            if (!skillReady && skill != christopherSkill) {
                SkillReady();
            }
        }

        /// <summary>
        /// Function for making a character's skill ready to be used
        /// </summary>
        public void SkillReady() {
            // Activate skills available animation
            coreAnimator.SetBool("skillAvailable", true);
            bgAnimator.SetBool("skillAvailable", true);
            // Set skills ready
            skillReady = true;
            // Play the SFX
            Sound.SoundSystem.instance.playSkillReady();
        }

        /// <summary>
        /// Triggered after Confirm button is clicked on the dialog box
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Empty event arguments</param>
        public void SkillActivate(object sender, EventArgs e) {
            // Activate skills used animation
            coreAnimator.SetBool("skillAvailable", false);
            bgAnimator.SetBool("skillAvailable", false);
            for (int i = 0; i < numParticle; i++) {
                // Setup emit parameter of the particle
                ParticleSystem.EmitParams param = new ParticleSystem.EmitParams();
                // Generate a random point for the particle to spawn, this position is a relative position relative to the game object
                Vector2 randomPosVector = UnityEngine.Random.insideUnitCircle * 1.25f;
                Vector3 particlePos = new Vector3(randomPosVector.x, randomPosVector.y, 0);
                param.position = particlePos;
                // Set the velocity to be toward the center of the game object and reach in 1 second
                param.velocity = -particlePos * 1.5f;
                // Spawn 1 particle
                particle.Emit(param, 1);
            }
            if (skill != christopherSkill) {
                // Notify skill activation to Coordinator
                Coordinator.Coordinator.NotifySkillsActivation(skill);
            }
            else {
                // Prolong next movement time to 12 seconds
                HealthBar.instance.RequestProlong();
            }
            // Set skill ready to false
            skillReady = false;
            // Play the SFX
            Sound.SoundSystem.instance.playSkillUse();
        }

    }

}
