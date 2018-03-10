using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Orbs.Canvas {

    public class Canvas : MonoBehaviour {

        private int comboCounter = 1;

        // Combo text template
        public GameObject comboTemplate;
        // Store the instance of Canvas script to allow static access from all other classes
        public static Canvas instance = null;

        // List storing all ComboText spawned during this round
        List<ComboText> combo = new List<ComboText>();

        /// <summary>
        /// Instantiate Canvas instance
        /// </summary>
        public void Start() {
            // Store the instance for static external access
            Canvas.instance = this;
        }

        // Update is called once per frame
        public void Update() {

        }

        public void PrintNewCombo(Vector3 position, float delayAppearence) {
            combo.Add(ComboText.Factory(comboTemplate, this.transform, position, delayAppearence, "Combo " + comboCounter));
            Sound.SoundSystem.instance.playComboSFX(comboCounter, delayAppearence);
            comboCounter++;
        }

        public void DestructCombo() {
            foreach (ComboText ct in combo) {
                ct.endAnimation();
            }
            combo.Clear();
            comboCounter = 1;
        }

    }

}
