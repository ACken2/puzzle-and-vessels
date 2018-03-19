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
        /// Number of particle to be spawned when a skill is usd
        /// </summary>
        private readonly int numParticle = 15;

        /// <summary>
        /// Initialize the instance
        /// </summary>
        public void Start() {
            // Initialize private variable and grab components needed
            particle = transform.parent.GetComponent<ParticleSystem>();
            coreAnimator = GetComponent<Animator>();
            bgAnimator = transform.GetChild(0).GetComponentInChildren<Animator>();
            // Register
            Coordinator.Coordinator.RegisterCharacter(this);
            // Start all character with skill ready
            SkillReady();
        }

        /// <summary>
        /// Activated when the character sprite is clicked which trigger skill activation
        /// </summary>
        public void OnMouseUpAsButton() {
            // Check if skill is ready to be used, and if Coordinator allow us to use skill
            if (skillReady && Coordinator.Coordinator.RequestSkillsActivation()) {
                SkillDialogBox.instance.ConfirmSkill += SkillActivate;
                SkillDialogBox.instance.displaySkill(skill);
            }
        }

        /// <summary>
        /// Called on every end turn to try to reactivate the skill of this character
        /// </summary>
        public void ReactivateSkill() {
            if (!skillReady) {
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
            // Notify skill activation to Coordinator
            Coordinator.Coordinator.NotifySkillsActivation(skill);
            // Set skill ready to false
            skillReady = false;
            // Play the SFX
            Sound.SoundSystem.instance.playSkillUse();
        }

    }

}
