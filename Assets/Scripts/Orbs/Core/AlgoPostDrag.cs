using System;
using UnityEngine;

namespace Assets.Scripts.Orbs.Core {

    /// <summary>
    /// Handle all operations after OnEndDrag, and raise PostDragOpDone event once everything is settled
    /// </summary>
    public class AlgoPostDrag {

        /// <summary>
        /// Event raised when everything is completed
        /// </summary>
        public event EventHandler PostDragOpDone;
        /// <summary>
        /// 2D orb array
        /// </summary>
        private Orb[,] orbs;

        /// <summary>
        /// Construct AlgoPostDrag
        /// </summary>
        /// <param name="postDragOrbs">2D orb array</param>
        public AlgoPostDrag(Orb[,] postDragOrbs) {
            orbs = postDragOrbs;
        }

        /// <summary>
        /// Raise PostDragOpDone event once we are done
        /// </summary>
        /// <param name="e">Empty event arguments</param>
        protected virtual void OnPostDragOpDone(EventArgs e) {
            EventHandler handler = PostDragOpDone;
            if (handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Process the 2D orb array
        /// </summary>
        public void process() {
            // Check for if there are match in the 2D orb array that we have to process
            if (new AlgoMatching().checkOrbToMatch(orbs)) {
                // Match to handle, run the cycle
                PreMatching();
            }
            else {
                switchOrb(true);
                // Raise completed event
                OnPostDragOpDone(EventArgs.Empty);
            }
        }

        /// <summary>
        /// First step of processing the 2D orb array - matching and elimination pair
        /// </summary>
        private void PreMatching() {
            // Switch all Orb instances off
            switchOrb(false);
            // Match pairs of Orb instances that should be eliminated and process them
            AlgoMatching am = new AlgoMatching();
            am.MatchingCompleted += PostMatching;
            am.matchOrb(orbs);
        }

        /// <summary>
        /// Triggered after AlgoMatching is finished to begin rearrangment and spawning of new orb
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Empty event arguments</param>
        private void PostMatching(object sender, EventArgs e) {
            AlgoRearrangement ar = new AlgoRearrangement();
            ar.RearrangementCompleted += PostRearrangement;
            ar.rearrangeOrb(orbs);
        }

        /// <summary>
        /// Triggered after AlgoRearrangement is finished, will either re-run the process if there are additional pair to process, or trigger PostDragDone event
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Empty event arguments</param>
        private void PostRearrangement(object sender, EventArgs e) {
            // Check for if there are match in the updated 2D orb array that we have to process
            if (new AlgoMatching().checkOrbToMatch(orbs)) {
                // Additional match to handle, run the cycle again
                PreMatching();
            }
            else {
                // Despawn all ComboText
                Canvas.CanvasController.instance.DestructCombo();
                // Re-enable gameplay
                switchOrb(true);
                // Raise completed event
                OnPostDragOpDone(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Switch on or off all Orb instances
        /// </summary>
        /// <param name="on">Whether to turn on or off Orb instances</param>
        private void switchOrb(bool on) {
            // Loop through the Orbs array
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 6; j++) {
                    Orb orb = orbs[i, j];
                    orb.gameObject.SetActive(on);
                    if (on) {
                        // Make Orb instance visisble again
                        SpriteRenderer orbSprite = orb.GetComponent<SpriteRenderer>();
                        Color orbColor = orbSprite.color;
                        orbColor.a = 1;
                        orbSprite.color = orbColor;
                    }
                    else {
                        // Making Orb invisible
                        SpriteRenderer orbSprite = orb.GetComponent<SpriteRenderer>();
                        orbSprite.color -= new Color(0, 0, 0, 1);
                    }
                }
            }
        }

    }

}
