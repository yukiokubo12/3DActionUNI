using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookCameraOverride : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    
    private void Update()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
