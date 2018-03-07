using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Orbs {

    /// <summary>
    /// Static class that controls the Orb panel and Orbs
    /// </summary>
    public static class OrbPanel {

        private static readonly int trackingZ = 1;

        private static Orb[,] orbs = new Orb[5, 6];
        private static Orb selectedOrb = null;
        private static GameObject currentTracker = null;
        private static int originalSelectedType = -1;

        /// <summary>
        /// Each Orb instance should register here at Start() method
        /// </summary>
        /// <param name="orb">Orb instance</param>
        /// <param name="row">Row number of the Orb that starts at 0</param>
        /// <param name="column">Column number of the Orb that starts at 0</param>
        public static void regOrb(Orb orb, int row, int column) {
            orbs[row, column] = orb;
        }

        public static void OnBeginDrag(Orb orb, int row, int column) {
            spawnTrackingOrb(orb);
            orb.OnSelectedOrb();
            selectedOrb = orb;
            originalSelectedType = orb.getType();
        }

        public static void OnDrag(PointerEventData ev) {
            // Update tracker position
            Vector3 pointerPos = Camera.main.ScreenToWorldPoint(new Vector3(ev.position.x, ev.position.y, 10));
            pointerPos.z = trackingZ;
            currentTracker.transform.position = pointerPos;
            // Check if any orbs has to be swapped
            GameObject raycastedObj = ev.pointerCurrentRaycast.gameObject;
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
                }
            }
        }

        public static void OnEndDrag() {
            selectedOrb.OnSwap(originalSelectedType);
            selectedOrb.OnDeselectOrb();
            originalSelectedType = -1;
            selectedOrb = null;
            UnityEngine.Object.Destroy(currentTracker);
            List<List<Orb>> matches = MatchingAlgo.matchOrb(orbs);
            foreach (List<Orb> lo in matches) {
                String a = "";
                foreach (Orb o in lo) {
                    a += o;
                }
                Debug.Log(a);
            }
        }

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

    }

}
