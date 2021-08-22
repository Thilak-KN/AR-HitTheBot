using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; //use ARFoundation namespace
using UnityEngine.XR.ARSubsystems; //use ARSubsystems namespace (ARSubsystems works on low-level and AR framework)
using UnityEngine.UI;
using TMPro;

public class ARSessionState : MonoBehaviour
{
    [SerializeField] private TMP_Text sessionStateTMP;

    private void awake()
    {
        sessionStateTMP.text = "~";
    }

    private void OnEnable()
    {
        ARSession.stateChanged += ARSession_stateChanged; // "+=" will subscribe to an Event in C# and "ARSession_stateChanged" is the function invoked when the Event is invoked
    }

    private void OnDisable()
    {
        ARSession.stateChanged -= ARSession_stateChanged; // -= will unsubscribe the Event
    }

    private void ARSession_stateChanged(ARSessionStateChangedEventArgs obj) //event handler
    {
        if(sessionStateTMP==null)
        {
            Debug.Log("Fak u");
        }
        switch(((int)obj.state))
        {
            case 0:
                sessionStateTMP.text = "Status : None[Not Initialized]\n"; break;
            case 1:
                sessionStateTMP.text= "Status : Unsupported\n"; break;
            case 2:
                sessionStateTMP.text = "Status : CheckingAvailability\n"; break;
            case 3:
                sessionStateTMP.text = "Status : NeedsInstall\n"; break;
            case 4:
                sessionStateTMP.text = "Status : Installing\n"; break;
            case 5:
                sessionStateTMP.text = "Status : Ready\n"; break;
            case 6:
                sessionStateTMP.text = "Status : SessionInitializing\n"; break;
            case 7:
                sessionStateTMP.text = "Status : SessionTracking\n"; break;

        }

        //First, we can check what the total AR engine state is. Next, we can look at the tracking state itself.

        switch (ARSession.notTrackingReason)
        {
            case NotTrackingReason.None:
                sessionStateTMP.text += "Tracking Error : None";
                break;
            case NotTrackingReason.Initializing:
                sessionStateTMP.text += "Tracking Error : Initializing";
                break;
            case NotTrackingReason.Relocalizing:
                sessionStateTMP.text += "Tracking Error : Relocalizing";
                break;
            case NotTrackingReason.InsufficientLight:
                sessionStateTMP.text += "Tracking Error : InsufficientLight";
                break;
            case NotTrackingReason.InsufficientFeatures:
                sessionStateTMP.text += "Tracking Error : InsufficientFeatures";
                break;
            case NotTrackingReason.ExcessiveMotion:
                sessionStateTMP.text += "Tracking Error : ExcessiveMotion";
                break;
            case NotTrackingReason.Unsupported:
                sessionStateTMP.text += "Tracking Error : Unsupported";
                break;
        }
        //throw new System.NotImplementedException();

    }

}
