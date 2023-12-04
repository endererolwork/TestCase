using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // Hedef takip edilecek nesne (oyuncu)
    [SerializeField] private float smoothSpeed = 5f;  // Takip etme yumuşaklık hızı

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned for TopDownCameraFollow script.");
            return;
        }

        // Hedefin pozisyonunu al
        Vector3 targetPosition = target.position;

        // Kameranın Y ekseni pozisyonunu sabit tut (yükseklik)
        targetPosition.y = transform.position.y;

        // Kamera pozisyonunu güncelle, yumuşak bir şekilde hedefe doğru takip et
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Kameranın rotasyonunu sabit tut (yere bakacak şekilde)
        transform.LookAt(target);
    }
}