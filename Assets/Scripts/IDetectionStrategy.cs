using UnityEngine;
using Utilities;

namespace AlictusPlatform {
    public interface IDetectionStrategy {
        bool Execute(Transform player, Transform detector, CountdownTimer timer);
    }
}