using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Observer<int> onEnemyDeath = new Observer<int>(0);

    public void Die()
    {
        // Düşmanın ölme işlemleri burada gerçekleştirilir.

        // Observer'a bilgi gönderme
        onEnemyDeath.Set(onEnemyDeath + 1);
    }
}
