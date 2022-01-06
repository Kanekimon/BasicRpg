using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Player
{
    public class PlayerTargetSystem : MonoBehaviour
    {
        public float interactionDistance;
        public TMPro.TextMeshProUGUI interactionText;
        private GameObject _target;
        Camera cam;

        public Transform npc;
        // Use this for initialization
        void Start()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Debug.DrawRay(this.transform.position, new Vector3(Screen.width / 2f, Screen.height / 2f, 0f), Color.red, 100);
            bool successfulHit = false;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null)
                {
                    HandleInteraction(interactable);
                    interactionText.text = interactable.GetDescription();
                    successfulHit = true;
                }

            }

            if (!successfulHit)
                interactionText.text = "";
        }

        void HandleInteraction(Interactable interactable)
        {
            KeyCode key = KeyCode.E;
            switch (interactable.interactionType)
            {
                case Interactable.InteractionType.Click:
                    // interaction type is click and we clicked the button -> interact
                    if (Input.GetKeyDown(key))
                    {
                        interactable.Interact(this.gameObject);
                    }
                    break;
                case Interactable.InteractionType.Hold:
                    if (Input.GetKey(key))
                    {
                        // we are holding the key, increase the timer until we reach 1f
                        interactable.IncreaseHoldTime();
                        if (interactable.GetHoldTime() > 1f)
                        {
                            interactable.Interact(this.gameObject);
                            interactable.ResetHoldTime();
                        }
                    }
                    else
                    {
                        interactable.ResetHoldTime();
                    }
                    break;
                // helpful error for us in the future
                default:
                    throw new System.Exception("Unsupported type of interactable.");
            }
        }
    }
}