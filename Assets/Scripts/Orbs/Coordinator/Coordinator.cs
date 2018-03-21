using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Orbs.Coordinator {

    /// <summary>
    /// Coordinate all in-game event
    /// </summary>
    public static class Coordinator {

        /// <summary>
        /// List of Character instance currently active in the scene
        /// </summary>
        private static List<Canvas.Character> characters = new List<Canvas.Character>();
        /// <summary>
        /// Boolean storing whether the round is currently progressing (aka player beginning dragging)
        /// </summary>
        private static bool roundProgressing = false;
        /// <summary>
        /// Boolean storing a skill has already been used in this round
        /// </summary>
        private static bool skillUsedInCurrentRound = false;
        /// <summary>
        /// String storing the skill that is used
        /// </summary>
        private static string skillUsed;
        /// <summary>
        /// Boolean storing whether a dialog box is currently active
        /// </summary>
        private static bool dialogActive = false;
        /// <summary>
        /// Counter for the number of Combo to this instance
        /// </summary>
        private static int comboCounter = 1;

        /// <summary>
        /// All Character instance should call this method to register in the Coordinator
        /// </summary>
        /// <param name="character">Character instance</param>
        public static void RegisterCharacter(Canvas.Character character) {
            characters.Add(character);
        }

        /// <summary>
        /// Get whether a skill can be used now
        /// </summary>
        /// <returns>Boolean stating whether a skill can be used now</returns>
        public static bool RequestSkillsActivation() {
            // False if either player is already moving Orb or if a skill has already been used this round
            return !roundProgressing && !skillUsedInCurrentRound;
        }

        /// <summary>
        /// Notify Coordinator that a skill has been activated
        /// </summary>
        public static void NotifySkillsActivation(string skillId) {
            skillUsedInCurrentRound = true;
            skillUsed = skillId;
        }

        /// <summary>
        /// Notify Coordinator that player has begin their round
        /// </summary>
        public static void NotifyRoundStarted() {
            roundProgressing = true;
        }

        /// <summary>
        /// Notify Coordinator that the round has ended
        /// </summary>
        /// <param name="validRound">Valid round is a round where user has moved an orb</param>
        public static void NotifyRoundEnded(bool validRound) {
            // Check valid round
            // A round can be invalid if the player only click on an orb without moving it
            if (validRound) {
                // Decrement timer if it is a valid round
                Canvas.HealthBar.instance.OnRoundEnded();
                // Reactivate used skill if any
                foreach (Canvas.Character character in characters) {
                    character.ReactivateSkill();
                }
                // Attack the enemy and see if it dies
                bool dead = Canvas.Enemy.instance.Attack(skillUsed, comboCounter);
                // Load next stage if dead
                if (dead) {
                    if (StageManager.instance.NotifyNextStage()) {
                        // Play SFX
                        Sound.SoundSystem.instance.playStageClear();
                        // Game ended
                        Canvas.ClearText.instance.DisplayClearText();
                        Canvas.GameClearDialogBox.instance.EndGame(StageManager.instance.getEndGameMessage());
                    }
                    else {
                        // Next stage
                        // Play SFX
                        Sound.SoundSystem.instance.playTransition();
                    }
                }
            }
            // Reset variables as round end
            roundProgressing = false;
            skillUsedInCurrentRound = false;
            comboCounter = 1;
        }

        /// <summary>
        /// Notify Coordinator that a dialog box is currently active
        /// </summary>
        public static void NotifyDialogActive() {
            dialogActive = true;
        }

        /// <summary>
        /// Notify Coordinator that a dialog box is resolved
        /// </summary>
        public static void NotifyDialogDeactive() {
            dialogActive = false;
        }

        /// <summary>
        /// Notify Coordinator that a new combo has been found
        /// </summary>
        public static void NotifyIncrementCombo() {
            comboCounter += 1;
        }

        public static void NotifyEndGame() {
            Debug.Log("THE END");
        }

        /// <summary>
        /// Get whether player can now move orb
        /// </summary>
        /// <returns>Boolean stating whether player movement on Orb should be entertained now</returns>
        public static bool GetOrbMovable() {
            // False if there is an active dialog box
            return !dialogActive;
        }

        /// <summary>
        /// Get the current combo counter
        /// </summary>
        /// <returns>Current combo count</returns>
        public static int GetCombo() {
            return comboCounter;
        }

        /// <summary>
        /// Get the character list
        /// </summary>
        /// <returns>List of characters in the scene</returns>
        public static List<Canvas.Character> GetCharacters() {
            return characters;
        }

    }

}
