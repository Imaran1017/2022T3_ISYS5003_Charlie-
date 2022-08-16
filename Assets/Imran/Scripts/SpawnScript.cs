using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnScript : MonoBehaviour
{

    [SerializeField] ARRaycastManager myRaycastManager;
    List<ARRaycastHit> myHits = new List<ARRaycastHit>();
    [SerializeField] GameObject spawnablePrefab;
    Camera arCam;
    GameObject spawnedObject;
    void Start()
    {
        spawnedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (myRaycastManager.Raycast(Input.GetTouch(0).position, myHits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        spawnedObject = hit.collider.gameObject;

                    }
                    else
                    {
                        SpawnPrefab(myHits[0].pose.position);    


                        //  Spawnpefab(myHits[0].pose.position);
                    }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
                {
                    spawnedObject.transform.position = myHits[0].pose.position;

                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    spawnedObject = null;
                }

            }

        }


    }

    private void SpawnPrefab(Vector3 position)
    {
        spawnedObject = Instantiate(spawnablePrefab, position, Quaternion.identity);
    }
}

