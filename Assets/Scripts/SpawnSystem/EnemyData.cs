using UnityEngine;

namespace AlictusPlatform {
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data")]
    public class EnemyData : EntityData {
        [Header("Enemy-Specific Attributes")]
        public int damage = 10; // Örnek olarak düşmanın verdiği hasar
        // Diğer özel özellikler buraya eklenebilir
    }
}