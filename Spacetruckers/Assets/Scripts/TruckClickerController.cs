using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckClickerController : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas selectDestinationCanvas;

    private Transform truck;
    private Button loadCargoAndSelectDestinationButton;
    private bool isSelectDestinationPopupOpen = false;
    private bool isSelectDestinationActive = false;
    private Vector3 truckDestination;


    void Start()
    {
        this.truck = this.GetComponent<Transform>(); // get truck transform object
        this.loadCargoAndSelectDestinationButton = GameObject.Find("SelectCargoButton").GetComponent<Button>();

        this.setDestinationSelectionPopupActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if left button pressed...
        {
            Debug.Log("Left Click");
            // if selection a detsination is active, select the next click as destination
            if (this.isSelectDestinationActive)
            {
                Vector3 targetLocation = this.getMouseTargetLocation();

                if (targetLocation != Vector3.zero)
                {
                    // set truck target to the click location on the plane of the truck
                    //TODO this should be a prefab
                    GameObject target = new GameObject();
                    target.layer = LayerMask.NameToLayer("Stations");
                    CapsuleCollider stopCollider = target.AddComponent<CapsuleCollider>();
                    stopCollider.radius = 0.1f;
                    stopCollider.height = 10f;
                    stopCollider.isTrigger = true;

                    target.transform.position = targetLocation;
                    GetComponent<Truck>().target = target.transform;
                }

                this.setDestinationSelectionPopupActive(false);
                this.isSelectDestinationActive = false;
            }
            else if (this.isSelectDestinationPopupOpen == true)
            {
                //this.setDestinationSelectionPopup(false);

                // TODO: Close Popup if not clicked in popup
            }
            // else select a detsination is not active, do nothing for now 
        }
        else // if there was no click
        {
            // check if mouse hovers over a planet to highlight it
            Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // if a planet was clicked
                if (hit.transform.gameObject.CompareTag("Planet"))
                {
                    Debug.Log("highlight Planet: " + hit.transform.gameObject);
                    // TODO: Highlight game object
                }
                else if (hit.transform.gameObject.CompareTag("Truck"))
                {
                    Debug.Log("highlight Truck: " + hit.transform.gameObject);
                    // TODO: Highlight game object
                }
            }
        }
    }

    public void OnMouseDown()
    {
        if (truck != null)
        {
            loadCargoAndSelectDestinationButton.gameObject.SetActive(false);

            // locate the popup for destination selection at the trucks position (the topleft corner at click position)
            Transform selectDestinationTransform = this.selectDestinationCanvas.GetComponent<Transform>();
            selectDestinationTransform.position = new Vector3(this.truck.position.x  /*+ (selectDestinationTransform.size.x / 2)*/, 
                this.truck.position.y + 1, 
                this.truck.position.z /*+ (selectDestinationTransform.size.z / 2)*/);

            // Get Planet the truck is on (if any)
            // TODO Get Ray to check from truck position Ray ray = this.mainCamera.ray(this.truck.position);
            Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition); // temporary workaround: the click on the truck is porbably above a planet when cargo is available
            GameObject planet = this.getPlanetAtLocation(ray);
            if (planet != null)
            {
                PlanetCargoController pcc = planet.GetComponent<PlanetCargoController>();
                if (!pcc.producedGoods.Equals(EnumCargo._Nichts)) // if anything gets produced
                {
                    loadCargoAndSelectDestinationButton.GetComponentInChildren<Text>().text = "Load " + pcc.producedGoods + " & \nSelectdestination";
                    loadCargoAndSelectDestinationButton.gameObject.SetActive(true);
                }
            }

            this.setDestinationSelectionPopupActive(true);
        }
    }


    public void SelectDetsinationButtonClick()
    {
        this.setDestinationSelectionPopupActive(false);

        // activate select destination so the next click will be set as destination
        this.isSelectDestinationActive = true; 
    }

    internal void setDestinationSelectionPopupActive(bool active)
    {
        this.selectDestinationCanvas.gameObject.SetActive(active);
        this.isSelectDestinationPopupOpen = active;
    }

    private Vector3 getMouseTargetLocation()
    {
        Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);

        // first check if planet was clicked
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // if a planet was clicked
            if (hit.transform.gameObject.CompareTag("Planet"))
            {
                Debug.Log(hit.transform.gameObject);
                return new Vector3(hit.transform.position.x, this.truck.position.y, hit.transform.position.z);
            }
        }


        // make a plane at Y level of truck to get click location on that plane
        Plane planeTruck = new Plane(Vector3.up, this.truck.position.y);

        float distance;
        if (planeTruck.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }


        return Vector3.zero;
    }

    private GameObject getPlanetAtLocation(Ray locationRay)
    {
        // first check if planet was clicked
        RaycastHit hit;
        if (Physics.Raycast(locationRay, out hit))
        {
            // if a planet was clicked
            if (hit.transform.gameObject.CompareTag("Planet"))
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }
}
