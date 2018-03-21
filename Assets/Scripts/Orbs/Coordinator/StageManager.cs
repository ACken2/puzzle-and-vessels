using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Orbs.Coordinator {

    /// <summary>
    /// Control class responsible for in-game stage management
    /// </summary>
    public class StageManager : MonoBehaviour {

        /// <summary>
        /// Static variable declaring the game index to be loaded, MUST be set before actually loading the game
        /// </summary>
        public static int gameIndex = 0;
        /// <summary>
        /// Allowing static access to this class
        /// </summary>
        public static StageManager instance;

        /// <summary>
        /// Variable that keep track of the current stage
        /// </summary>
        private static int stageIndex = 0;
        /// <summary>
        /// Maximum number of stage in the current game
        /// </summary>
        private static int maxStage = 0;

        /// <summary>
        /// Ran FIRST before all script when a game is loaded, initialize enemy and team member chosen
        /// </summary>
        public void Start() {
            // Static access
            instance = this;
            // Initialize stage variable
            maxStage = Data.Games.GetMaxStage(gameIndex);
            // Load character sprite and setting
            List<Canvas.Character> characters = Coordinator.GetCharacters();
            characters.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x)); // Sort object from left to right
            List<string> selectedCharacter = TeamManager.GetMembers();
            for (int i=0; i<characters.Count; i++) {
                if (i < selectedCharacter.Count) {
                    characters[i].InitSprite(selectedCharacter[i]); // Init team member sprite
                    characters[i].skill = Data.Members.GetSkill(selectedCharacter[i]); // Set the skill of the character
                }
                else {
                    characters[i].InitSprite(null); // Disable unused character
                }
            }
            // Load the stage
            LoadStage();
        }

        /// <summary>
        /// Get the maximum allowable round for the current game
        /// </summary>
        /// <returns>Maximum allowable round</returns>
        public int getMaxRound() {
            return Data.Games.GetGameMaxRound(gameIndex);
        }

        /// <summary>
        /// Get the end game message for the current game
        /// </summary>
        /// <returns>End game message for the current game</returns>
        public string getEndGameMessage() {
            return Data.Games.GetEndMessage(gameIndex);
        }

        /// <summary>
        /// Increment stage counter by 1 and enter next stage
        /// </summary>
        /// <returns>True if the game is already over, or false if more stage is loaded</returns>
        public bool NotifyNextStage() {
            stageIndex += 1;
            if (stageIndex < maxStage) {
                // Animate enemy fading out
                Canvas.Enemy.instance.animationEvent.AnimationCompleted += LoadStagePre;
                Canvas.Enemy.instance.FadeOut();
                return false;
            }
            else {
                // Animate enemy fading out
                Canvas.Enemy.instance.FadeOut();
                return true;
            }
        }

        /// <summary>
        /// Triggered after enemy has been faded out and it is ready to transition into the next stage
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Empty event arguments</param>
        private void LoadStagePre(object sender, EventArgs e) {
            LoadStage();
        }

        /// <summary>
        /// Load the current stage according to stageIndex
        /// </summary>
        private void LoadStage() {
            LoadStageTransition();
        }

        /// <summary>
        /// Animate the transition text between stage
        /// </summary>
        private void LoadStageTransition() {
            Canvas.StageText.instance.animationEvent.AnimationCompleted += LoadStagePost;
            Canvas.StageText.instance.Transition(stageIndex, maxStage);
        }

        /// <summary>
        /// Triggered after stage transition is completed
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Empty event arguments</param>
        private void LoadStagePost(object sender, EventArgs e) {
            // Load enemy data
            Data.Stage stage = Data.Games.GetStage(gameIndex, stageIndex);
            Canvas.Enemy.instance.InitEnemy("Enemy/" + stage.spriteId, stage.effectiveSkill, stage.combo);
            // Fade in the enemy
            Canvas.Enemy.instance.FadeIn();
        }

    }

}
