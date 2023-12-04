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

        [SerializeField] private float scaleDuration = 0.5f;
        [SerializeField] private Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.5f);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(CollectAndDestroy());
            }
        }

        private IEnumerator CollectAndDestroy()
        {
            // Scale up
            float timer = 0f;
            while (timer < scaleDuration)
            {
                transform.localScale = Vector3.Lerp(Vector3.one, maxScale, timer / scaleDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            // Trigger collection event
            OnAnyCollected?.Invoke(points);

            // Scale down
            timer = 0f;
            while (timer < scaleDuration)
            {
                transform.localScale = Vector3.Lerp(maxScale, Vector3.zero, timer / scaleDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}