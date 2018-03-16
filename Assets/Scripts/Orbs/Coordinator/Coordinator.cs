using System;
using System.Collections.Generic;

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
        /// Boolean storing whether a dialog box is currently active
        /// </summary>
        private static bool dialogActive = false;

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
        public static void NotifySkillsActivation() {
            skillUsedInCurrentRound = true;
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
            // Decrement timer if it is a valid round
            // A round can be invalid if the player only click on an orb without moving it
            if (validRound) {
                Canvas.HealthBar.instance.OnRoundEnded();
            }
            // Reset variables as round end
            roundProgressing = false;
            skillUsedInCurrentRound = false;
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
        /// Get whether player can now move orb
        /// </summary>
        /// <returns>Boolean stating whether player movement on Orb should be entertained now</returns>
        public static bool GetOrbMovable() {
            // False if there is an active dialog box
            return !dialogActive;
        }

    }

}
