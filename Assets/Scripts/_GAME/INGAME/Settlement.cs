using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settlement : MonoBehaviour{

    [Header("UI Related")]
    public GameObject canvas;
    public GameObject environment;
    [Header("Settlement Related")]
    public GameObject settlerPrefab;
    public GameObject settlerParent;
    public int settlerNumber;
    public GameObject scavengerParent;

    [Header("Settlement Details")]
    [SerializeField]
    public string settlementName;
    public int water, food, metal, wood;
    public int daysWithoutWater, daysWithoutFood, daysScavenging;
    public bool noFood = false, noWater = false, isScavenging = false, isBuilding = false;
    [SerializeField]
    public List<GameObject> settlers = new List<GameObject>();
    public List<GameObject> scavengers = new List<GameObject>();
    public List<GameObject> builders = new List<GameObject>();

    public void setSettlementName(string name){
        settlementName = name;
    }

    private void Awake() {
        //Initialise
        settlerNumber = 0;
        canvas = GameObject.FindGameObjectWithTag("UI_Canvas");
        environment = GameObject.FindGameObjectWithTag("_Environment");
        settlerParent = GameObject.FindGameObjectWithTag("SettlerParent");
        scavengerParent = GameObject.FindGameObjectWithTag("ScavengerParent");

        //Pass Object To Canvas
        // canvas.GetComponent<UpdateValues>().setSettlement(this);

        //Generate Settlers
        int randomNumber = Random.Range(4, 7);
        setSettlementName(PlayerPrefs.GetString("SettlementName"));
        for(int i = 0; i <= randomNumber; i++){
            addSettler();
        }

        //Generate Starting Items
        water = Random.Range(80, 100);
        food = Random.Range(80, 100);
        metal = Random.Range(20, 40);
        wood = Random.Range(20, 40);
    }

    public void addSettler(){
        GameObject newSettler = Instantiate (settlerPrefab);
        newSettler.transform.parent = settlerParent.transform;
        newSettler.gameObject.name = newSettler.GetComponent<Settler>().firstName + "_" + newSettler.GetComponent<Settler>().lastName;
        newSettler.GetComponent<Settler>().id = settlerNumber;
        settlers.Add(newSettler);
        settlerNumber++;
    }

    public void moveScavengersToSettlers(){
        for(int i = 0; i < scavengers.Count; i++){
            settlers.Add(scavengers[i]);
        }
        scavengers.Clear();
    }

    public int getSettlerCount(){
        return settlers.Count;
    }
}
