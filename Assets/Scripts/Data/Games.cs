using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Data {

    /// <summary>
    /// Data class for loading and giving away game data involving stages
    /// </summary>
    public static class Games {

        /// <summary>
        /// Array storing all loaded game
        /// </summary>
        private static Game[] loadedGames;
        /// <summary>
        /// File path to the data file
        /// </summary>
        private static readonly string gameDataJson = "game.json";

        /// <summary>
        /// Load all game data in game.json
        /// </summary>
        public static void LoadGame() {
            // Compute absolute file path
            string filePath = Path.Combine(Application.streamingAssetsPath, gameDataJson);
            // Check if file existed
            if (File.Exists(filePath)) {
                // Read the json file and convert to SkillJson object
                string json = File.ReadAllText(filePath);
                GameJson loadedData = JsonUtility.FromJson<GameJson>(json);
                // Store the loaded array
                loadedGames = loadedData.games;
            }
            else {
                // Log warning if the skill is not found
                Debug.LogWarning("ERROR: game.json missing!");
            }
        }

        /// <summary>
        /// Get the name of the gameIndex-th game
        /// </summary>
        /// <param name="gameIndex">Index of the game interested</param>
        /// <returns>Name of the game</returns>
        public static string GetGameName(int gameIndex) {
            return loadedGames[gameIndex].gameName;
        }

        /// <summary>
        /// Get the number of stage in gameIndex-th game
        /// </summary>
        /// <param name="gameIndex">Index of the game interested</param>
        /// <returns>Number of stage of the game</returns>
        public static int GetMaxStage(int gameIndex) {
            return loadedGames[gameIndex].stages.Length;
        }

        /// <summary>
        /// Get the stageIndex-th stage of gameIndex-th game
        /// </summary>
        /// <param name="gameIndex">Index of the game interested</param>
        /// <param name="stageIndex">Index of the stage in the game interested</param>
        /// <returns>Stage data</returns>
        public static Stage GetStage(int gameIndex, int stageIndex) {
            return loadedGames[gameIndex].stages[stageIndex];
        }

        /// <summary>
        /// Get the maximum round allowed on gameIndex-th game
        /// </summary>
        /// <param name="gameIndex">Index of the game interested</param>
        /// <returns>Maximum round allowed on the game</returns>
        public static int GetGameMaxRound(int gameIndex) {
            return loadedGames[gameIndex].maxAllowedTurn;
        }

        /// <summary>
        /// Get the message to be showed to the user when player finish a game
        /// </summary>
        /// <param name="gameIndex">Index of the game interested</param>
        /// <returns>End game message</returns>
        public static string GetEndMessage(int gameIndex) {
            return loadedGames[gameIndex].endGameMessage;
        }

    }

    /// <summary>
    /// Describe the JSON format stored in game.json
    /// </summary>
    [Serializable]
    public struct GameJson {

        /// <summary>
        /// Game array that contain all possible game
        /// </summary>
        public Game[] games;

    }

    /// <summary>
    /// Describe the format for 1 specific game
    /// </summary>
    [Serializable]
    public struct Game {

        /// <summary>
        /// Game name to be displayed
        /// </summary>
        public string gameName;
        /// <summary>
        /// Max number of turn allowed in this round
        /// </summary>
        public int maxAllowedTurn;
        /// <summary>
        /// Detail of stages that occur in the game
        /// </summary>
        public Stage[] stages;
        /// <summary>
        /// Message to be showed at end game
        /// </summary>
        public string endGameMessage;

    }

    /// <summary>
    /// Describe the format for 1 specific Stage in a game
    /// </summary>
    [Serializable]
    public struct Stage {

        /// <summary>
        /// Sprite ID for the enemy
        /// </summary>
        public string spriteId;
        /// <summary>
        /// Array of skill that would be effective against this enemy
        /// </summary>
        public string[] effectiveSkill;
        /// <summary>
        /// Required number of combo to kill this enemy
        /// </summary>
        public int combo;

    }

}
