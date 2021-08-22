using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class LightEstimationScript : MonoBehaviour
{
    [SerializeField] private ARCameraManager ARCamManager;
    ARLightEstimationData lightEstimationData;

    private float avgBrightness = 0;
    private float avgColorTemp = 0;
    private Color colorCorrection;

    [SerializeField] private GameObject colorCorrectionGameObject;
    [SerializeField] private GameObject averageBrightnessGameObject;
    [SerializeField] private GameObject averageColorTemperatureGameObject;

    private Image colorCorrectionImg;
    private Image averageBrightnessImg;
    private Image averageColorTemperatureImg;

    [SerializeField] Light unitySceneLight;
    private void Awake()
    {
        colorCorrectionImg = colorCorrectionGameObject.GetComponent<Image>();
        averageBrightnessImg = averageBrightnessGameObject.GetComponent<Image>();
        averageColorTemperatureImg = averageColorTemperatureGameObject.GetComponent<Image>();
    }

    private void OnEnable()
    {
        ARCamManager.frameReceived += ARcamManager_frameReceived; //subscribe the event
    }

    private void OnDisable()
    {
        ARCamManager.frameReceived -= ARcamManager_frameReceived; //unsubscribe the event
    }

    private void ARcamManager_frameReceived(ARCameraFrameEventArgs obj) //event handler
    {
        lightEstimationData = obj.lightEstimation;

        if(lightEstimationData.averageBrightness.HasValue)
        {
            avgBrightness = lightEstimationData.averageBrightness.Value;
            unitySceneLight.intensity = avgBrightness;   
        }

        if(lightEstimationData.averageColorTemperature.HasValue)
        {
            avgColorTemp = lightEstimationData.averageColorTemperature.Value;
            unitySceneLight.colorTemperature = avgColorTemp;
        }

        if(lightEstimationData.colorCorrection.HasValue)
        {
            colorCorrection = lightEstimationData.colorCorrection.Value;
            unitySceneLight.color = colorCorrection;
        }

        colorCorrectionImg.color = colorCorrection;
        averageColorTemperatureImg.color = Mathf.CorrelatedColorTemperatureToRGB(avgColorTemp);
        averageBrightnessImg.color = new Color(255 * avgBrightness, 255 * avgBrightness, 255 * avgBrightness, 1);
     
        //throw new System.NotImplementedException();
    }
}
