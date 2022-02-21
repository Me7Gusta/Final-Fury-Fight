using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    public int vSync = 1;
    public int target = 30;

    private void Start()
    {
        QualitySettings.vSyncCount = vSync;
        Application.targetFrameRate = target;
    }

    private void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;
    }
}
