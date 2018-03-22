using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
        /// UI text component of the shield incidator
        /// </summary>
        public Text shieldText;
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
        /// Skill that can defeat everything
        /// </summary>
        private readonly string debugSkill = "c1bd09cd-1e8c-4e63-92c0-358d71bd9b8d";

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
            if (spritePath == "Enemy/ae03f6d8-6ec8-4ec8-8002-9f5cea909e27") {
                // Special processing code for this special enemy which has 4 possible type
                Core.Orb.GetRandomNumber(1, 5); // Fixed identical output of 2
                int randomType = Core.Orb.GetRandomNumber(1, 5);
                spritePath += "_" + randomType.ToString();
                minimumCombo = randomType;
            }
            else {
                // Load normally
                minimumCombo = minCombo;
            }
            switchImage.SwitchImage(spritePath);
            acceptedSkill = (string[]) skillEffective.Clone();
            shieldText.transform.parent.gameObject.SetActive(true);
            shieldText.text = minimumCombo.ToString();
        }

        /// <summary>
        /// Attack this enemy with a given skill and combo
        /// </summary>
        /// <param name="skillUsed">Skill ID used against this enemy</param>
        /// <param name="combo">Number of combo used against the enemy</param>
        /// <returns>Boolean stating whether the enemy is killed</returns>
        public bool Attack(string skillUsed, int combo) {
            if ((acceptedSkill.Contains(skillUsed) || skillUsed == debugSkill) && combo >= minimumCombo) {
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
            shieldText.transform.parent.gameObject.SetActive(false);
            animationEvent.OnAnimationCompleted(EventArgs.Empty);
        }

    }

}
