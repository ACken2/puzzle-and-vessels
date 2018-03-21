using System.Collections.Generic;

namespace Assets.Scripts.Orbs.Coordinator {

    /// <summary>
    /// Static class responsible for tracking the current team member selected
    /// </summary>
    public static class TeamManager {

        /// <summary>
        /// Array storing the UUID of the member selected
        /// </summary>
        private static List<string> members = new List<string>();

        /// <summary>
        /// Add a new member to the selection
        /// </summary>
        /// <param name="member">UUID of the new team member</param>
        /// <returns>Boolean stating whether the addition is successful or not</returns>
        public static bool AddMember(string member) {
            if (members.Count >= 6) {
                // Reject if there is already 6 chosen member
                return false;
            }
            else {
                // Accept addition request
                members.Add(member);
                return true;
            }
        }

        /// <summary>
        /// Remove a member from the selection
        /// </summary>
        /// <param name="member">Member to be removed</param>
        public static void RemoveMember(string member) {
            members.Remove(member);
        }

        /// <summary>
        /// Return the current list of selected member
        /// </summary>
        /// <returns>List of string that detail the currently selected member</returns>
        public static List<string> GetMembers() {
            List<string> members = new List<string>();
            members.Add("ce42489e-2b75-472e-be02-5a8e4b4747d5");
            return members;
            //return members;
        }

        /// <summary>
        /// Reset all static variable in this classs
        /// </summary>
        public static void Reset() {
            members.Clear();
        }

    }

}
