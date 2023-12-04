using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Observer<int> onEnemyDeath = new Observer<int>(0);

    public void Die()
    {
        //death operations ruled here
        onEnemyDeath.Set(onEnemyDeath + 1);
    }
}
