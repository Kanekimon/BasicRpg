using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerSystem : MonoBehaviour
    {

        private GameObject _currentTarget;

  
        // Use this for initialization
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetTarget(GameObject g)
        {
            this._currentTarget = g;
        }

        public void RemoveTarget()
        {
            this._currentTarget = null;
        }


        //public void Gather(Item i, int amount)
        //{
        //    this._inv.AddItem(i, amount);
        //}

    }
}