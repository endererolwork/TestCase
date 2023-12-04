using UnityEngine;

namespace AlictusPlatform
{
    public class ProjectileCollisionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerDetector playerDetector;
        private GameObject owner;

        public void SetOwner(GameObject owner)
        {
            this.owner = owner;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject != owner)
            {
                Debug.Log(" I hit player");
                // Deal damage to the player only if the collided object has the "Player" tag
                Health targetHealth = collision.gameObject.GetComponent<Health>();
                if (targetHealth != null)
                {
                    
                    targetHealth.TakeDamage(10);
                }
            }

            // Add any other logic for when the projectile hits something
            Destroy(gameObject);
        }
    }
}