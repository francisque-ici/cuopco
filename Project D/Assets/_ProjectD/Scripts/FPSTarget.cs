using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSUnlocker : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;   
    }
}
