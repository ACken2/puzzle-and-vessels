using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Data {

    /// <summary>
    /// Data class for loading and giving away skill data
    /// </summary>
    public static class Skills {

        /// <summary>
        /// Dictionary for storing a reflection for retrieving skills form UUID
        /// </summary>
        private static Dictionary<string, SkillCore> skillDict = new Dictionary<string, SkillCore>();

        /// <summary>
        /// File path of the skill data file
        /// </summary>
        private static readonly string skillDataJson = "skill.json";

        /// <summary>
        /// Load all skills in skill.json
        /// </summary>
        public static void LoadSkill() {
            // Compute absolute file path
            string filePath = Path.Combine(Application.streamingAssetsPath, skillDataJson);
            // Check if file existed
            if (File.Exists(filePath)) {
                // Read the json file and convert to SkillJson object
                string json = File.ReadAllText(filePath);
                SkillJson loadedData = JsonUtility.FromJson<SkillJson>(json);
                // Loop through the array and add it to the dictionary
                foreach (Skill skill in loadedData.skills) {
                    skillDict.Add(skill.uuid, skill.skill);
                }
            }
            else {
                // Log warning if the skill is not found
                Debug.LogWarning("ERROR: skill.json missing!");
            }
        }

        /// <summary>
        /// Retrieve a skill based on its UUID
        /// </summary>
        /// <param name="uuid">UUID of that skill</param>
        /// <returns></returns>
        public static SkillCore GetSkill(string uuid) {
            return skillDict[uuid];
        }

    }

    /// <summary>
    /// Describe the JSON format stored in skill.json
    /// </summary>
    [Serializable]
    public struct SkillJson {

        /// <summary>
        /// Skill array that contain all the skill
        /// </summary>
        public Skill[] skills;

    }

    /// <summary>
    /// Descibe format of 1 object in the array of skill.json
    /// </summary>
    [Serializable]
    public struct Skill {

        /// <summary>
        /// UUID for that skill
        /// </summary>
        public string uuid;
        /// <summary>
        /// Core skill object
        /// </summary>
        public SkillCore skill;

    }

    /// <summary>
    /// Describe the actual skill
    /// </summary>
    [Serializable]
    public struct SkillCore {

        /// <summary>
        /// Title of skill
        /// </summary>
        public string title;
        /// <summary>
        /// Description of the skill
        /// </summary>
        public string description;

    }

}
