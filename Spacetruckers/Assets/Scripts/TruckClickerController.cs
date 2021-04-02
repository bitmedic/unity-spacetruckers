using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckClickerController : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas selectDestinationCanvas;

    private Transform truck;
    private bool isSelectDestinationPopupOpen = false;
    private bool isSelectDestinationActive = false;
    private Vector3 truckDestination;

    void Start()
    {
        this.truck = this.GetComponent<Transform>(); // get truck transform object

        this.selectDestinationCanvas.gameObject.SetActive(false); // deactive destination popup at start
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if left button pressed...
        {
            if (this.isSelectDestinationActive)
            {
                // if selection a detsination is active, select the next click as detsination

                // make a plane at Y level of truck to get click location on that plane
                Plane planeTruck = new Plane(Vector3.up, this.truck.position.y);

                Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);

                float distance;
                if (planeTruck.Raycast(ray, out distance))
                {
                    // set truck detsination to the click location on the plane of the truck
                    this.truck.position = ray.GetPoint(distance);
                }
            }
            else if (this.isSelectDestinationPopupOpen == true)
            {
             

                // TODO: Close Popup if not clicked in popup
            }
            // else select a detsination is not active, do nothing for now 



            // to get the object that was clicked on in 3D
            //Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit))
            //{
            //    // the object identified by hit.transform was clicked
            //    // do whatever you want
            //}
        }
    }

    public void OnMouseDown()
    {
        if (truck != null)
        {
            // locate the popup for destination selection at the trucks position (the topleft corner at click position)
            Transform selectDestinationTransform = this.selectDestinationCanvas.GetComponent<Transform>();
            selectDestinationTransform.position = new Vector3(this.truck.position.x  /*+ (selectDestinationTransform.size.x / 2)*/, 
                this.truck.position.y + 1, 
                this.truck.position.z /*+ (selectDestinationTransform.size.z / 2)*/);

            this.selectDestinationCanvas.gameObject.SetActive(true);

            this.isSelectDestinationPopupOpen = true;
        }
    }


    public void SelectDetsinationButtonClick()
    {
        // deactivate select destination popup
        this.isSelectDestinationPopupOpen = false;
        this.selectDestinationCanvas.gameObject.SetActive(false);

        // activate select destination so the next click will be set as destination
        this.isSelectDestinationActive = true; 
    }

}
