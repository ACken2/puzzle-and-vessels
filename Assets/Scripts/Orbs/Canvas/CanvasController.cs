using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class for controlling the main canvas
    /// </summary>
    public class CanvasController : MonoBehaviour {

        /// <summary>
        /// Combo text template
        /// </summary>
        public GameObject comboTemplate;
        /// <summary>
        /// Store the instance of Canvas script to allow static access from all other classes
        /// </summary>
        public static CanvasController instance = null;

        /// <summary>
        /// List storing all ComboText spawned during this round
        /// </summary>
        List<ComboText> combo = new List<ComboText>();

        /// <summary>
        /// Instantiate Canvas instance
        /// </summary>
        public void Start() {
            // Store the instance for static external access
            CanvasController.instance = this;
        }

        /// <summary>
        /// Print a new Combo text at a given position with a given delay
        /// </summary>
        /// <param name="position">Vector3 position that this ComboText should be located</param>
        /// <param name="delayAppearence">Delay in second that this ComboText should be visible and SFX is played</param>
        public void PrintNewCombo(Vector3 position, float delayAppearence) {
            combo.Add(ComboText.Factory(comboTemplate, this.transform, position, delayAppearence, "Combo " + Coordinator.Coordinator.GetCombo()));
            Sound.SoundSystem.instance.playComboSFX(Coordinator.Coordinator.GetCombo(), delayAppearence);
        }

        /// <summary>
        /// Destroy all ComboText
        /// </summary>
        public void DestructCombo() {
            foreach (ComboText ct in combo) {
                ct.endAnimation();
            }
            combo.Clear();
        }

    }

}
