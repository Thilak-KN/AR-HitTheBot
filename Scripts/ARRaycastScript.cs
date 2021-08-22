using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ARRaycastScript : MonoBehaviour
{
    [SerializeField] private ARRaycastManager m_RaycastManager;
    [SerializeField] private GameObject placePrefab;
    private int prefabCount=0;
    private TMPro.TMP_Text hitTypeTMP;

    private Resolution screenRes;
    private double borderLimitUpper;
    private double borderLimitLower;

    List<ARRaycastHit> hitResultsList = new List<ARRaycastHit>(); //initially list is empty and each ARRaycastHit is appended to list [refer function call in line 17]

    private void Start()
    {
        hitTypeTMP = GameObject.Find("HitType").GetComponent<TMPro.TMP_Text>();
        screenRes = Screen.currentResolution;
        borderLimitUpper = 0.85 * screenRes.height; //80% of the screen is touch-able to instantiate robot prefab
        borderLimitLower = 0.2 * screenRes.height;
    }

    void Update()
    {
        if (Input.touchCount == 0) //no touches since last frame so return call 
            return;

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, hitResultsList /*Contents of hitResultsList are replaced with the raycast results, if successful hit*/) && (prefabCount <=1) && (Input.GetTouch(0).position.y < borderLimitUpper && (Input.GetTouch(0).position.y > borderLimitLower))/*accept only touch input with-in the borderLimit*/)
        {
            // Only returns true if there is at least one hit

            hitTypeTMP.text = "Hit Type : " + hitResultsList[0].hitType.ToString();

            if (prefabCount==0)
            {
                Instantiate(placePrefab, hitResultsList[0].pose.position, placePrefab.transform.rotation);
                prefabCount++;
            }
            else
            {
                GameObject scenePrefabObject = GameObject.FindGameObjectWithTag("Prefab");
                scenePrefabObject.transform.position = hitResultsList[0].pose.position;
            }


            

        }
    }
}
