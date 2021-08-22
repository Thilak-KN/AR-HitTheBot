using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;

public class ARPlaneCountManager : MonoBehaviour
{
    [SerializeField] private ARPlaneManager ARPlaneManagerScript;
    [SerializeField] private GameObject planeCountObject;
    private TMP_Text planeCountText;
    private int planeCount=0;

    private void Awake()
    {
        planeCountText = planeCountObject.GetComponent<TMP_Text>();
        planeCountText.text = "Plane Count : " + planeCount.ToString();
    }

    private void OnEnable()
    {
        ARPlaneManagerScript.planesChanged += ARPlaneManagerScript_planesChanged; //subscribe to Event
    }

    private void OnDisable()
    {
        ARPlaneManagerScript.planesChanged -= ARPlaneManagerScript_planesChanged; //unsubscribe to Event when this script component is set Inactive/disabled
    }

    private void ARPlaneManagerScript_planesChanged(ARPlanesChangedEventArgs obj) //event handler
    {
        planeCount = planeCount + obj.added.Count - obj.removed.Count; //plane count in current AR Session Space
        planeCountText.text = "Plane Count : " + planeCount.ToString();

        //throw new System.NotImplementedException();
    }
}
