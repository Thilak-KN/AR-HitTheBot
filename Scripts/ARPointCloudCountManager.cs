using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARPointCloudCountManager : MonoBehaviour
{
    [SerializeField] private ARPointCloudManager ARPointCloudManagerScript;
    [SerializeField] private GameObject pointCloudCountObject;
    private TMP_Text pointCloudCountText;
    private int pointCloudCount=0;
    

    private void Awake()
    {
        pointCloudCountText = pointCloudCountObject.GetComponent<TMP_Text>();
        pointCloudCountText.text = "Point Cloud Count : "+pointCloudCount.ToString();
        pointCloudCountText.text += "\nPoints Count : " + 0.ToString();
    }

    private void OnEnable()
    {
        ARPointCloudManagerScript.pointCloudsChanged += ARPointCloudManagerScript_pointCloudsChanged; //subscribe to pointCloudChange Event
    }

    private void OnDisable()
    {
        ARPointCloudManagerScript.pointCloudsChanged -= ARPointCloudManagerScript_pointCloudsChanged; //unsubscribe to pointCloudChange Event
    }

    private void ARPointCloudManagerScript_pointCloudsChanged(ARPointCloudChangedEventArgs obj) //event handler
    {
        int pointsCount = 0;

        pointCloudCount = pointCloudCount + obj.added.Count - obj.removed.Count; // number of points in current PointCloud in AR Session Space
        pointCloudCountText.text = "Point Cloud Count : "+pointCloudCount.ToString();

        for (int i = 0; i < obj.updated.Count; i++)
        {
            pointsCount = pointsCount + obj.updated[i].identifiers.GetValueOrDefault().Length;
        }

        pointCloudCountText.text = pointCloudCountText.text + "\nPoints Count : " + pointsCount;
        //throw new System.NotImplementedException();
    }
}
