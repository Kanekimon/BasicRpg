using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Interactable : MonoBehaviour
    {
        
        public enum InteractionType
        {
            Click,
            Hold
        }

        float holdTime;

        public InteractionType interactionType;
        public GameObject prefab;

        public abstract string GetDescription();
        public abstract void Interact(GameObject interactinWith);
        public void IncreaseHoldTime() => holdTime += Time.deltaTime;
        public void ResetHoldTime() => holdTime = 0f;
        public float GetHoldTime() => holdTime;
        public GameObject GetPrefab() => prefab;

    }
}