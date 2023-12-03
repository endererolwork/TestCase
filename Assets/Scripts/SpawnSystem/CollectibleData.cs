using UnityEngine;

namespace AlictusPlatform {
    [CreateAssetMenu(fileName = "CollectibleData", menuName = "Platformer/Collectible Data")]
    public class CollectibleData : EntityData {
        public int score;

        public int killScore;
        // additional properties specific to collectibles
    }
}