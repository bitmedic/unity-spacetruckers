using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TruckClickerController : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas selectDestinationCanvas;

    private Button loadCargoAndSelectDestinationButton;
    private bool isSelectCargoPopupOpen = false;
    private bool isSelectDestinationActive = false;
    private Vector3 truckDestination;

    private CargoSO toLoadCargo = null;

    void Start()
    {
        this.loadCargoAndSelectDestinationButton = GameObject.Find("SelectCargoButton").GetComponent<Button>();

        this.setDestinationSelectionPopupActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if left button pressed...
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                this.OnMouseClick();
            }
        }
    }

    public void OnMouseClick()
    {
        GetComponent<HighlightOnMouseover>().SetSelected(false);


        Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);

        // first check if planet was clicked
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // if the truck was clicked on the popup is not already open
            if (hit.transform.gameObject.CompareTag("Truck"))
            {
                GetComponent<HighlightOnMouseover>().SetSelected(true);
                GetComponentInChildren<TruckAudioController>().setTruckSelected(true);

                if (this.isSelectCargoPopupOpen) // if popup is already open do nothing
                {
                    return;
                }

                loadCargoAndSelectDestinationButton.gameObject.SetActive(false);

                // locate the popup for destination selection at the trucks position (the topleft corner at click position)
                RectTransform selectDestinationTransform = this.selectDestinationCanvas.GetComponent<RectTransform>();
                selectDestinationTransform.position = transform.position + new Vector3(0, 1, -1f);

                // Get Planet the truck is on (if any)
                // TODO Get Ray to check from truck position Ray ray = this.mainCamera.ray(this.truck.position);
                GameObject planet = this.getPlanetAtLocation(ray);
                if (planet != null)
                {
                    PlanetCargoController pcc = planet.GetComponent<PlanetCargoController>();
                    if (pcc.producedGoods != null) // if anything gets produced
                    {
                        this.toLoadCargo = pcc.producedGoods;
                        loadCargoAndSelectDestinationButton.GetComponentInChildren<Text>().text = "Load " + pcc.producedGoods.cargoName;
                        loadCargoAndSelectDestinationButton.gameObject.SetActive(true);

                        this.setDestinationSelectionPopupActive(true);
                    }

                }

                this.isSelectDestinationActive = true; // if no cargo is available only select destination and don't show popup
                return; // if truck was clicked this was it. For all other cases (planet or nothing was clicked) continue
            }
        }


        // if selection a destination is active, select the next click as destination
        if (this.isSelectDestinationActive)
        {
            Vector3 targetLocation = this.getMouseTargetLocation();

            if (targetLocation != Vector3.zero)
            {
                //TODO this should be a prefab
                GameObject target = new GameObject();
                target.layer = LayerMask.NameToLayer("Stations");
                CapsuleCollider stopCollider = target.AddComponent<CapsuleCollider>();
                stopCollider.radius = 0.1f;
                stopCollider.height = 10f;
                stopCollider.isTrigger = true;

                target.transform.position = targetLocation;
                GetComponent<Truck>().target = target.transform;

                GetComponentInChildren<TruckAudioController>().setTruckMoving(true);
            }

            this.setDestinationSelectionPopupActive(false);
            this.isSelectDestinationActive = false;
        }        
    } 

    public void LoadCargoAndSelectDestinationButtonClick()
    {
        // load Cargo
        gameObject.GetComponent<Truck>().LoadCargo(this.toLoadCargo);

        this.setDestinationSelectionPopupActive(false);

        // activate select destination so the next click will still be set as destination
        this.isSelectDestinationActive = true;
    }

    internal void setDestinationSelectionPopupActive(bool active)
    {
        this.selectDestinationCanvas.gameObject.SetActive(active);
        this.isSelectCargoPopupOpen = active;
        this.isSelectDestinationActive = active;
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
                return new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
            }
        }

        // make a plane at Y level of truck to get click location on that plane
        Plane planeTruck = new Plane(Vector3.up, transform.position.y);

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
        foreach(RaycastHit hit in Physics.RaycastAll(locationRay))
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
