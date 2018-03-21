using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Orbs.Core {

    /// <summary>
    /// Class controlling the Orb game object
    /// </summary>
    public class Orb : MonoBehaviour {

        /// <summary>
        /// Row number of this Orb instance
        /// </summary>
        public int row;
        /// <summary>
        /// Column number of this Orb instance
        /// </summary>
        public int column;
        /// <summary>
        /// Sprite for type 1 orb
        /// </summary>
        public Sprite typeOneOrb;
        /// <summary>
        /// Sprire for type 2 orb
        /// </summary>
        public Sprite typeTwoOrb;
        /// <summary>
        /// Sprite for type 3 orb
        /// </summary>
        public Sprite typeThreeOrb;

        /// <summary>
        /// Current type of this Orb instance
        /// </summary>
        private int type = 1;

        /// <summary>
        /// Boolean storing whether the OnMouseUp should call the OnEndDrag function
        /// </summary>
        private bool mouseDown = false;
        /// <summary>
        /// Alpha value that is subtracted when this Orb instance is selected
        /// </summary>
        private const float selectedAlpha = 0.8f;

        /// <summary>
        /// Sprite renderer of this Orb instance
        /// </summary>
        private SpriteRenderer sprite;

        /// <summary>
        /// Extended hitbox of this instance when the player begin orb movment
        /// </summary>
        private BoxCollider2D[] extendedHitbox;

        /// <summary>
        /// Random instance shared among all Orb via a static reference to prevent re-initializing Random multiple times which mess it up
        /// </summary>
        private static readonly System.Random getrandom = new System.Random();

        // Use this for initialization
        public void Start() {
            // Setup listener to BeginDrag, Drag and EndDrag
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry dragEntry = new EventTrigger.Entry();
            dragEntry.eventID = EventTriggerType.Drag;
            dragEntry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
            EventTrigger.Entry endDragEntry = new EventTrigger.Entry();
            endDragEntry.eventID = EventTriggerType.EndDrag;
            endDragEntry.callback.AddListener((data) => { OnEndDragDelegate((PointerEventData)data); });
            trigger.triggers.Add(dragEntry);
            trigger.triggers.Add(endDragEntry);
            // Register to OrbPanel
            OrbPanel.regOrb(this, row, column);
            // Cache sprite renderer instance
            sprite = GetComponent<SpriteRenderer>();
            // Cache extended hitbox
            extendedHitbox = GetComponents<BoxCollider2D>();
            // Randomize orb type
            type = GetRandomNumber(1, 4);
            // Update sprite
            updateSprite();
        }

        /// <summary>
        /// Get the current type of the Orb
        /// </summary>
        /// <returns>Type of the Orb</returns>
        public int getType() {
            return type;
        }

        /// <summary>
        /// Set the type of this Orb
        /// </summary>
        /// <param name="type">New type to be applied</param>
        public void setType(int type) {
            this.type = type;
            updateSprite();
        }

        /// <summary>
        /// Eliminate this Orb and wipe its type
        /// </summary>
        public void eliminate() {
            type = -1;
        }

        /// <summary>
        /// Triggered when clicked
        /// </summary>
        public void OnMouseDown() {
            // Call OnBeginDrag - all drag / click begins here
            OrbPanel.OnBeginDrag(this, row, column);
            // Set mouseDown as true for OnMouseUp
            mouseDown = true;
        }

        /// <summary>
        /// Triggered when mouse is released
        /// </summary>
        public void OnMouseUp() {
            // Check mouseDown
            if (mouseDown) {
                // If mouseDown is true, it means that no OnDragDelegate is ever triggered
                // In that case, obviously no OnEndDragDelegate is triggered either, so we call OnEndDrag on their behalf
                OrbPanel.OnEndDrag(true);
            }
            // Reset mouseDown
            mouseDown = false;
        }

        /// <summary>
        /// Triggered when being dragged
        /// </summary>
        /// <param name="data">Current pointer information</param>
        public void OnDragDelegate(PointerEventData data) {
            // Notify OnMouseUp that it does not have to call OnEndDrag
            mouseDown = false;
            // Notify OrbPanel on the current drag
            OrbPanel.OnDrag(data);
        }

        /// <summary>
        /// Triggered when drag is completed
        /// </summary>
        /// <param name="data">Current pointer information</param>
        public void OnEndDragDelegate(PointerEventData data) {
            // Notify OrbPanel the drag has stopped
            OrbPanel.OnEndDrag(false);
        }

        /// <summary>
        /// Call this when Orb is selected (aka the user pointer is hovering above this Orb during Drag or being clicked)
        /// </summary>
        public void OnSelectedOrb() {
            // Fade out the color when selected
            sprite.color -= new Color(0, 0, 0, selectedAlpha);
        }

        /// <summary>
        /// Call this when Orb is de-selected
        /// </summary>
        public void OnDeselectOrb() {
            sprite.color += new Color(0, 0, 0, selectedAlpha);
        }

        /// <summary>
        /// Call this when Orb has switched into another type (via player input)
        /// </summary>
        /// <param name="newType">New type</param>
        public void OnSwap(int newType) {
            // Set new type
            type = newType;
            // Update sprite to the new type
            updateSprite();
        }

        /// <summary>
        /// Activate the extended hitbox if any
        /// </summary>
        public void ActivateExtendedHitbox() {
            if (extendedHitbox != null) {
                foreach (BoxCollider2D bc2d in extendedHitbox) {
                    bc2d.enabled = true;
                }
            }
        }

        /// <summary>
        /// Deactivate the extended hit box if any
        /// </summary>
        public void DeactivateExtendedHitbox() {
            if (extendedHitbox != null) {
                foreach (BoxCollider2D bc2d in extendedHitbox) {
                    bc2d.enabled = false;
                }
            }
        }

        /// <summary>
        /// Update the sprite to match the current type of this Orb
        /// </summary>
        private void updateSprite() {
            if (type == 1) {
                sprite.sprite = typeOneOrb;
            }
            else if (type == 2) {
                sprite.sprite = typeTwoOrb;
            }
            else {
                sprite.sprite = typeThreeOrb;
            }
        }

        /// <summary>
        /// Get a ranom integer
        /// </summary>
        /// <param name="min">Minimum integer returned (inclusive)</param>
        /// <param name="max">Maximum integer returned (exclusive)</param>
        /// <returns>Random integer between min (inclusive) and max (exclusive)</returns>
        public static int GetRandomNumber(int min, int max) {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

    }

}
