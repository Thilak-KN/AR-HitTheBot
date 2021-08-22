using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;

public class DeviceSupportChecker : MonoBehaviour
{
    private ARSession m_Session;
    [SerializeField] private TMP_Text supportInfo;
    IEnumerator Start() //start() function as co-routine
    {
        m_Session = GameObject.Find("AR Session").GetComponent<ARSession>();

        if ((((int)ARSession.state) == 0) ||
            (((int)ARSession.state) == 2))
        {
            yield return ARSession.CheckAvailability(); //start the CheckAvailability() co-routine
        }

        if (((int)ARSession.state) == 1)
        {
            supportInfo.text = "AR Engine is not Supported in this Device :(";
            Application.Quit(); //quit application as it not supported by this device
            // Start some fallback experience for unsupported devices
        }
        else
        {
            if(((int)ARSession.state)==3 || ((int)ARSession.state)==4)
            {
                ARSession.Install();
            }
            supportInfo.text = "Device supports AR !";
            // Start the AR session
            m_Session.enabled = true;
        }
    }

}

