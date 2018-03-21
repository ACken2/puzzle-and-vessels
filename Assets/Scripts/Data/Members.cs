using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data {

    /// <summary>
    /// Data class for loading and giving away member data
    /// </summary>
    public static class Members {

        /// <summary>
        /// Array storing a reference for the skill that a member will have
        /// </summary>
        private static Dictionary<string, string> memberSkillDict = new Dictionary<string, string>();
        /// <summary>
        /// File path to the data file
        /// </summary>
        private static readonly string memberDataJson = "member";

        /// <summary>
        /// Load all game data in member.json
        /// </summary>
        public static void LoadMember() {
            // Load Json
            TextAsset jsonAsset = Resources.Load("Data/" + memberDataJson) as TextAsset;
            string json = jsonAsset.ToString();
            MemberJson loadedData = JsonUtility.FromJson<MemberJson>(json);
            // Store the skill reference
            foreach (Member mem in loadedData.members) {
                memberSkillDict.Add(mem.uuid, mem.skillUUID);
            }
        }

        /// <summary>
        /// Get the skill UUID of a team member
        /// </summary>
        /// <param name="memUUID">UUID of the team member</param>
        /// <returns>Skill UUID of that team member</returns>
        public static string GetSkill(string memUUID) {
            return memberSkillDict[memUUID];
        }

    }

    /// <summary>
    /// Describe the JSON format stored in members.json
    /// </summary>
    [Serializable]
    public struct MemberJson {

        /// <summary>
        /// Array of possible team member
        /// </summary>
        public Member[] members;

    }

    /// <summary>
    /// Describe the format for 1 specific member
    /// </summary>
    [Serializable]
    public struct Member {

        /// <summary>
        /// UUID that uniquely identify this member
        /// </summary>
        public string uuid;
        /// <summary>
        /// UUID of the skill that h=this character can use
        /// </summary>
        public string skillUUID;

    }

}
