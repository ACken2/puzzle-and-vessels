using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Orbs.Core {

    /// <summary>
    /// Class that contain the algorithm for rearranging orbs after some are being eliminated, and animate the "falling animation" of the newly created orb
    /// </summary>
    public class AlgoRearrangement {

        // Event raised when Orbs are rearranged and all translation animation is completed with DummyOrb destroyed
        public event EventHandler RearrangementCompleted;
        // List of DummyOrb spawned for animation
        private List<DummyOrb> dummyOrbs = new List<DummyOrb>();
        // List of DummyOrb spawned with no animation
        private List<DummyOrb> staticDummyOrbs = new List<DummyOrb>();

        /// <summary>
        /// Initiate a rearrangement of Orb where some or all Orb are eliminated
        /// </summary>
        /// <param name="unarranged">Raw Orb 2D array with some Orb eliminated</param>
        public void rearrangeOrb(Orb[,] unarranged) {
            // Calculate the distance in y-axis in world space between each Orb
            float distanceBetweenOrb = unarranged[0, 0].transform.position.y - unarranged[1, 0].transform.position.y;
            // Loop through all Orbs, column by column
            for (int i=0; i<unarranged.GetLength(1); i++) {
                // Loop through each column from bottom to top (i.e. from j = 4 to j = 0)
                for (int j=unarranged.GetLength(0)-1; j>=0; j--) {
                    Orb orb = unarranged[j, i];
                    if (orb.getType() == -1) {
                        // Orb are eliminated
                        // Loop through all Orbs above this orb in the same column and look for any orb above it to fall down to this position
                        // Suppose j = 4, then we have to check from k = 3 to k = 0
                        for (int k=j-1; k>=0; k--) {
                            // Check if that orb has type not equals to -1 (i.e. not eliminated)
                            if (unarranged[k, i].getType() != -1) {
                                // If not eliminated, that orb should fall to the current location
                                // Set tht current orb to have that type
                                orb.setType(unarranged[k, i].getType());
                                // Eliminate the Orb that has now fall to some position below
                                unarranged[k, i].eliminate();
                                // Spawn DummyOrb that do the drop animation, and store it in the list
                                DummyOrb d = DummyOrb.Factory(unarranged[k, i].transform.position, unarranged[k, i].GetComponent<SpriteRenderer>().sprite, unarranged[k, i].transform.localScale);
                                d.StartTranslate(orb.transform.position);
                                dummyOrbs.Add(d);
                                // We are done with this orb
                                break;
                            }
                            // This Orb is also eliminated, look for 1 higher Orb then
                        }
                        // Triggered if there are non-eliminated Orb above this current Orb
                        if (orb.getType() == -1) {
                            // No orbs are above this orb to fall down to this position
                            // Loop through all Orbs above this Orb
                            for (int k=j; k>=0; k--) {
                                // Set the current Orb and every Orb above this Orb to a random type
                                int newType = Orb.GetRandomNumber(1, 4);
                                unarranged[k, i].setType(newType);
                                // Spawn a DummyOrb that translate to the position for that animation
                                // Calculate the Orb position, it should be just above the Orb itself seperated by the distance between each normal Orb
                                Vector3 dummyOrbPos = unarranged[k, i].transform.position;
                                dummyOrbPos.y += distanceBetweenOrb;
                                // Spawn DummyOrb that do the drop animation, and store it in the list
                                DummyOrb d = DummyOrb.Factory(dummyOrbPos, unarranged[k, i].GetComponent<SpriteRenderer>().sprite, unarranged[k, i].transform.localScale);
                                d.StartTranslate(unarranged[k, i].transform.position);
                                dummyOrbs.Add(d);
                            }
                            // We are done with the entire column at this point, no point to run this loop any further since all Orb within this column is set
                            break;
                        }
                    }
                    else {
                        // Orbs are not eliminated, but we are still spawning a non-moving dummyOrb for that
                        // This only applies to Orb at the bottom that are not eliminated
                        staticDummyOrbs.Add(DummyOrb.Factory(orb.transform.position, orb.GetComponent<SpriteRenderer>().sprite, orb.transform.localScale));
                    }
                }
            }
            // Listen to OnAnimaionDone on the the LAST dummyOrb object
            // Since all animation should be completed at the same time, and the event is triggered last for the last dummyOrb object, we are just listening to that object
            dummyOrbs[dummyOrbs.Count - 1].AnimationDone += animationCallback;
        }

        /// <summary>
        /// Callback passed to DummyOrb so they will callback once their animation is done, where the DummyOrbs would be destoryed
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Empty event arguments</param>
        private void animationCallback(object sender, EventArgs e) {
            // Destroy Animated DummyOrb
            for (int i=0; i<dummyOrbs.Count; i++) {
                UnityEngine.Object.Destroy(dummyOrbs[i].gameObject);
            }
            // Destroy Static DummyOrb
            for (int i=0; i<staticDummyOrbs.Count; i++) {
                UnityEngine.Object.Destroy(staticDummyOrbs[i].gameObject);
            }
            // Raise RearrangementCompleted event
            OnRearrangementCompleted(EventArgs.Empty);
        }

        /// <summary>
        /// Raise RearrangementCompleted event once the rearrangement is completed and animation is also completed
        /// </summary>
        /// <param name="e">Empty event arguments</param>
        protected virtual void OnRearrangementCompleted(EventArgs e) {
            EventHandler handler = RearrangementCompleted;
            if (handler != null) {
                handler(this, e);
            }
        }

    }

}
