using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Orbs.Core {

    /// <summary>
    /// Static class that controls the Orb panel and Orbs
    /// </summary>
    public static class OrbPanel {

        /// <summary>
        /// Boolean storing whether the orb has been swapped during a drag
        /// </summary>
        private static bool orbSwapped = false;

        /// <summary>
        /// Z-index that the trackingOrb is occupying
        /// </summary>
        private static readonly int trackingZ = 1;

        /// <summary>
        /// Store the Orb 2D array
        /// </summary>
        private static Orb[,] orbs = new Orb[5, 6];
        /// <summary>
        /// Store the instance of the currently selected Orb
        /// </summary>
        private static Orb selectedOrb = null;
        /// <summary>
        /// Store the instance of the tracker Orb that follow mouse movement
        /// </summary>
        private static GameObject currentTracker = null;
        /// <summary>
        /// Store the type of the originally selected Orb
        /// </summary>
        private static int originalSelectedType = -1;
        /// <summary>
        /// Boolean storing whether the timer has been activated
        /// </summary>
        private static bool timerActivated = false;

        /// <summary>
        /// Each Orb instance should register here at Start() method
        /// </summary>
        /// <param name="orb">Orb instance</param>
        /// <param name="row">Row number of the Orb that starts at 0</param>
        /// <param name="column">Column number of the Orb that starts at 0</param>
        public static void regOrb(Orb orb, int row, int column) {
            orbs[row, column] = orb;
        }

        /// <summary>
        /// Called by Orb that has beginning to get dragged
        /// </summary>
        /// <param name="orb">Orb instance that get drag</param>
        /// <param name="row">Row number of the Orb instance</param>
        /// <param name="column">Column number of the Orb instance</param>
        public static void OnBeginDrag(Orb orb, int row, int column) {
            // Check if player movement should be processed
            if (Coordinator.Coordinator.GetOrbMovable()) {
                spawnTrackingOrb(orb);
                orb.OnSelectedOrb();
                selectedOrb = orb;
                originalSelectedType = orb.getType();
                Coordinator.Coordinator.NotifyRoundStarted();
                foreach (Orb o in orbs) {
                    o.ActivateExtendedHitbox();
                }
            }
        }

        /// <summary>
        /// Called continuously during the drag to swap Orb if required
        /// </summary>
        /// <param name="ev">Current pointer data</param>
        public static void OnDrag(PointerEventData ev) {
            // Check if player movement should be processed
            if (Coordinator.Coordinator.GetOrbMovable()) {
                // Activate timer if not done so already
                if (!timerActivated) {
                    // Attach event listener
                    Canvas.HealthBar.instance.TimeReached += PostTimerReached;
                    // Activate timer
                    Canvas.HealthBar.instance.RequestCountdown();
                    // Set flag
                    timerActivated = true;
                }
                // Update tracker position
                Vector3 pointerPos = Camera.main.ScreenToWorldPoint(new Vector3(ev.position.x, ev.position.y, 10));
                pointerPos.z = trackingZ;
                currentTracker.transform.position = pointerPos;
                // Check if any orbs has to be swapped
                // Do a ray cast from the pointer position and try to find a game object
                RaycastHit2D hit;
                hit = Physics2D.Raycast(pointerPos, Vector2.zero);
                // Set raycaster object to be the collider or null (if nothing was raycasted to)
                GameObject raycastedObj = hit.collider == null ? null : hit.collider.gameObject;
                if (raycastedObj != null) {
                    Orb newSelectedOrb = raycastedObj.GetComponent<Orb>();
                    if (newSelectedOrb != null && newSelectedOrb != selectedOrb) {
                        // Swap the type of the 2 orb
                        selectedOrb.OnSwap(newSelectedOrb.getType());
                        newSelectedOrb.OnSwap(selectedOrb.getType());
                        // Select the new orb
                        selectedOrb.OnDeselectOrb();
                        newSelectedOrb.OnSelectedOrb();
                        selectedOrb = newSelectedOrb;
                        // Set orb swapped in this drag
                        orbSwapped = true;
                        // Play movement sound
                        Sound.SoundSystem.instance.playMovementSFX();
                    }
                }
            }
        }

        /// <summary>
        /// Called when the drag has ended to process changes required
        /// </summary>
        /// <param name="nodrag">Pass true if there are no 'actual' dragging involved (pure click onto the Orb)</param>
        public static void OnEndDrag(bool nodrag) {
            // Check if player movement should be processed
            if (Coordinator.Coordinator.GetOrbMovable()) {
                // Stop the timer (if necessary as deemed by HealthBar)
                Canvas.HealthBar.instance.StopCountdown();
                timerActivated = false;
                // Swap the currently selectedOrb with the type of the initally selected Orb
                selectedOrb.OnSwap(originalSelectedType);
                selectedOrb.OnDeselectOrb();
                // Reset the variable related to selected orb
                originalSelectedType = -1;
                selectedOrb = null;
                // Destroy the tracker
                UnityEngine.Object.Destroy(currentTracker);
                // Deactivate hitbox
                foreach (Orb o in orbs) {
                    o.DeactivateExtendedHitbox();
                }
                if (!nodrag) {
                    // Initiate AlgoPostDrag and let it handle post-drag events if any dragging occured
                    AlgoPostDrag apd = new AlgoPostDrag(orbs);
                    // Attach event listener to APD
                    apd.PostDragOpDone += PostDragDone;
                    // Process begin
                    apd.process();
                }
            }
        }

        /// <summary>
        /// Reset all static variable in this classs
        /// </summary>
        public static void Reset() {
            orbSwapped = false;
            orbs = new Orb[5, 6];
            selectedOrb = null;
            currentTracker = null;
            originalSelectedType = -1;
        }

        /// <summary>
        /// Triggered after AlgoPostDrag has completed
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Empty event arguments</param>
        private static void PostDragDone(object sender, EventArgs e) {
            // Tell coordinator that the round processing is done
            Coordinator.Coordinator.NotifyRoundEnded(orbSwapped);
            orbSwapped = false;
        }

        /// <summary>
        /// Spawn a tracking Orb that track the current mouse position
        /// </summary>
        /// <param name="original">Original Orb instance that the tracking Orb should be based on</param>
        private static void spawnTrackingOrb(Orb original) {
            GameObject trackingObj = new GameObject();
            SpriteRenderer sr = trackingObj.AddComponent<SpriteRenderer>();
            sr.sprite = original.GetComponent<SpriteRenderer>().sprite;
            Vector3 originalPos = original.transform.position;
            originalPos.x += 0.1f;
            originalPos.y += 0.1f;
            originalPos.z = trackingZ;
            trackingObj.transform.position = originalPos;
            trackingObj.transform.localScale = new Vector3(0.48f, 0.48f, 1);
            currentTracker = trackingObj;
        }

        /// <summary>
        /// Triggered after TimeReached event is raised
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="e">Empty event arguments</param>
        private static void PostTimerReached(object sender, EventArgs e) {
            // Force drag done
            OnEndDrag(false);
        }

    }

}
