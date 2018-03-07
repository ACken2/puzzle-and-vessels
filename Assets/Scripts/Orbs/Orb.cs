using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

namespace Assets.Scripts.Orbs {

    public class Orb : MonoBehaviour {

        // Publicly set variable
        public int row;
        public int column;
        public Sprite typeOneOrb;
        public Sprite typeTwoOrb;
        public Sprite typeThreeOrb;

        private int type = 1;

        // 
        private bool mouseDown = false;
        private const float selectedAlpha = 0.8f;

        // Used in orb swapping movement
        private int animationConstant = 10;
        private int counter = 0;
        private Vector3 moveTarget = Vector3.zero;
        private Vector3 moveSpeed = Vector3.zero;
        private Vector3 _preRotateionPos = Vector3.zero;

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
            // Randomize orb type
            type = GetRandomNumber(1, 4);
            GetComponent<SpriteRenderer>().sprite = getCurrentSprite();
        }

        // Update is called once per frame
        public void Update() {
            if (moveTarget.magnitude != 0) {
                // rotate();
                move();
            }
        }

        public int getType() {
            return type;
        }

        public void OnMouseDown() {
            OrbPanel.OnBeginDrag(this, row, column);
            mouseDown = true;
        }

        public void OnMouseUp() {
            if (mouseDown) {
                OrbPanel.OnEndDrag();
            }
            mouseDown = false;
        }

        public void OnDragDelegate(PointerEventData data) {
            mouseDown = false;
            OrbPanel.OnDrag(data);
        }

        public void OnEndDragDelegate(PointerEventData data) {
            OrbPanel.OnEndDrag();
        }

        public void OnSelectedOrb() {
            GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, selectedAlpha);
        }

        public void OnDeselectOrb() {
            GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, selectedAlpha);
        }

        public void OnSwap(int newType) {
            type = newType;
            GetComponent<SpriteRenderer>().sprite = getCurrentSprite();
        }

        private void move() {
            if (_preRotateionPos.magnitude == 0) {
                _preRotateionPos = transform.position;
                counter = animationConstant + 1;
            }
            transform.position += moveSpeed * Time.deltaTime;
            counter--;
            if (counter <= 0) {
                moveTarget = Vector3.zero;
                transform.position = _preRotateionPos;
            }
        }


        private Sprite getCurrentSprite() {
            if (type == 1) {
                return typeOneOrb;
            }
            else if (type == 2) {
                return typeTwoOrb;
            }
            else {
                return typeThreeOrb;
            }
        }

        private static int GetRandomNumber(int min, int max) {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

    }

}
