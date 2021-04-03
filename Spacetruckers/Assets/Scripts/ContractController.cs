using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContractController : MonoBehaviour
{
    private float defaultContractDuration = 300f; // in Zeitschritte... Sekunden?? - Noch nicht implementiert
    private float defaultContractValue = 10000f; // in Zeitschritte... Sekunden?? - Noch nicht implementiert

    [SerializeField]
    private List<GameObject> allPlanets;

    [SerializeField]
    private List<CargoSO> allCargo;

    // Start is called before the first frame update
    void Start()
    {
        this.allPlanets = new List<GameObject>();
        allPlanets.AddRange(GameObject.FindGameObjectsWithTag("Planet"));

        this.allCargo = new List<CargoSO>();
        foreach (GameObject planet in this.allPlanets)
        {
            PlanetCargoController pcc = planet.GetComponent<PlanetCargoController>();
            if (pcc != null)
            {
                if (pcc.producedGoods != null)
                {
                    allCargo.Add(pcc.producedGoods);
                    Debug.Log("Adding " + pcc.producedGoods + " as possible contract goal");
                }
            }
        }

        StartCoroutine(CreateRandomContract());
    }

    IEnumerator CreateRandomContract()
    {
        yield return new WaitForSeconds(1);

        // debug test
        Contract temp = this.createNewRandomContract();
        Debug.Log("Contract created: " + temp.contractCargo + " to " + temp.targetPlanet);

        temp.targetPlanet.GetComponent<PlanetCargoController>().SetContract(temp);
    }

    private void Update()
    {
        // TODO randomly create contracts ever X timestept


    }

    private Contract createNewRandomContract()
    {        
        int randomPlanetIndex = (int)Mathf.Round(Random.value * (this.allPlanets.Count - 1));
        GameObject planet = this.allPlanets[randomPlanetIndex];
        
        CargoSO cargo, targetPlanetCargo;
        do
        {
            int randomCargoIndex = Random.Range(0, this.allCargo.Count);
            Debug.Log("randomCargoIndex: " + randomCargoIndex + " this.allCargo.Count: " + this.allCargo.Count);
            cargo = this.allCargo[randomCargoIndex]; // get a random produced cargo
            targetPlanetCargo = planet.GetComponent<PlanetCargoController>().producedGoods; // get the cargo form the target planet
        } while (cargo == null || cargo.Equals(targetPlanetCargo)); // do this until a cargo is found that is not produced on the target system

        return new Contract(planet, cargo);
    }
}
