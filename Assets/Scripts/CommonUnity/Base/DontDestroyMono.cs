using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMono : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
