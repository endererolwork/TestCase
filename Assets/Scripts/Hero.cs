using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] public Observer<int> Health = new Observer<int>(100);
    


    private void Start()
    {
        
    }
}
