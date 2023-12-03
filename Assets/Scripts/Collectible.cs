using System;
using System.Collections;
using System.Collections.Generic;
using AlictusPlatform;
using UnityEngine;

namespace AlictusPlatform
{
    public class Collectible : Entity
    {
        [SerializeField] private int points = 1;
        public static event Action<int> OnAnyCollected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnAnyCollected?.Invoke(points);
                gameObject.SetActive(false);
            }
        }
    }
}