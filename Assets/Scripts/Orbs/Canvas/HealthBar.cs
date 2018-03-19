using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Orbs.Canvas {

    /// <summary>
    /// UI class for controlling the HP bar
    /// </summary>
    public class HealthBar: MonoBehaviour {

        /// <summary>
        /// Reference to the Clock logo left to the health bar
        /// </summary>
        public Image barLogo;

        /// <summary>
        /// Allow static access to the health bar
        /// </summary>
        public static HealthBar instance;

        /// <summary>
        /// Reference to the SimpleHealthBar from the plugin
        /// </summary>
        public SimpleHealthBar healthBar;

        /// <summary>
        /// Current health
        /// </summary>
        private float health = 30;

        /// <summary>
        /// Maximum health
        /// </summary>
        private float maxHealth = 30;
        /// <summary>
        /// Health Bar Color for upper 1/3 of health
        /// </summary>
        private readonly Color high = Color.green;
        /// <summary>
        /// Health Bar Color for middle 1/3 of health
        /// </summary>
        private readonly Color mid = Color.yellow;
        /// <summary>
        /// Health Bar Color for lower 1/3 of health
        /// </summary>
        private readonly Color low = Color.red;

        /// <summary>
        /// Initiailize the health bar to the set maxHealth
        /// </summary>
        public void Start() {
            // Initialize variables
            instance = this;
            barLogo = transform.parent.GetChild(2).GetComponent<Image>();
            // Get maximum health
            maxHealth = Coordinator.StageManager.instance.getMaxRound();
            health = maxHealth;
            // Update health bar
            healthBar.UpdateBar(health, maxHealth);
        }

        /// <summary>
        /// Call this method whenever a round end to reduce 1 HP and update the color of the health bar and its logo
        /// </summary>
        public void OnRoundEnded() {
            health -= 1;
            // Update health bar
            healthBar.UpdateBar(health, maxHealth);
            // Update color
            if (health / maxHealth >= 0.666f) {
                healthBar.UpdateColor(high);
                barLogo.color = high;
            }
            else if (health / maxHealth >= 0.333f) {
                healthBar.UpdateColor(mid);
                barLogo.color = mid;
            }
            else {
                healthBar.UpdateColor(low);
                barLogo.color = low;
            }
        }

    }

}
