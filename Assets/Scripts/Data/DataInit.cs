using UnityEngine;

namespace Assets.Scripts.Data {

    /// <summary>
    /// Initialize the Data classes, should be run when the game launch once only
    /// </summary>
    public class DataInit : MonoBehaviour {

        /// <summary>
        /// Initialize data classes and then self destruct
        /// </summary>
        public void Start() {
            Games.LoadGame();
            Skills.LoadSkill();
            Members.LoadMember();
            Destroy(this);
        }

    }

}
