using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class for controlling the enemy
    /// </summary>
    public class Enemy : MonoBehaviour {

        /// <summary>
        /// Allow static access to this script
        /// </summary>
        public static Enemy instance;
        /// <summary>
        /// Allow EventListener to be attached to AnimationCompleted event
        /// </summary>
        public GenericAnimationCallback animationEvent = new GenericAnimationCallback(true);

        /// <summary>
        /// SwitchableImage instance on the Enemy game object
        /// </summary>
        private SwitchableImage switchImage;
        /// <summary>
        /// Animator associated with the Enemy game object
        /// </summary>
        private Animator animator;
        /// <summary>
        /// Array of acceptable skill that would be effective against the current enemy
        /// </summary>
        private string[] acceptedSkill;
        /// <summary>
        /// Minimum combo required to defeat this enemy
        /// </summary>
        private int minimumCombo;

        /// <summary>
        /// Initialize references
        /// </summary>
        public void Start() {
            instance = this;
            switchImage = GetComponent<SwitchableImage>();
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Initialize the current enemy
        /// </summary>
        /// <param name="spritePath">Path to the desired sprite</param>
        /// <param name="skillEffective">Array of skill ID that would be effective in killing this enemy</param>
        /// <param name="minCombo">Minimum combo required to kill this enemy</param>
        public void InitEnemy(string spritePath, string[] skillEffective, int minCombo) {
            switchImage.SwitchImage(spritePath);
            acceptedSkill = (string[]) skillEffective.Clone();
            minimumCombo = minCombo;
        }

        /// <summary>
        /// Attack this enemy with a given skill and combo
        /// </summary>
        /// <param name="skillUsed">Skill ID used against this enemy</param>
        /// <param name="combo">Number of combo used against the enemy</param>
        /// <returns>Boolean stating whether the enemy is killed</returns>
        public bool Attack(string skillUsed, int combo) {
            if (acceptedSkill.Contains(skillUsed) && combo >= minimumCombo) {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Fade in the enemy
        /// </summary>
        public void FadeIn() {
            animator.SetBool("active", true);
        }

        /// <summary>
        /// Fade out the enemy
        /// </summary>
        public void FadeOut() {
            animator.SetBool("active", false);
        }

        /// <summary>
        /// Raise AnimationCompleted event when fade out is done
        /// </summary>
        public void OnFadeOutCompleted() {
            animationEvent.OnAnimationCompleted(EventArgs.Empty);
        }

    }

}
