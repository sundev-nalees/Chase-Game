using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera normalCam;
    [SerializeField] private CinemachineVirtualCamera chaseCam;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraSwitch();
    }

    private void CameraSwitch()
    {
        if (DataStore.chaseMode == true)
        {
            normalCam.Priority=0;
            chaseCam.Priority = 1;
        }
        else
        {
            normalCam.Priority = 1;
            chaseCam.Priority = 0;
        }
    }
}
