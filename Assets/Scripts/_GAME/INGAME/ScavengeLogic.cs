using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ScavengeLogic : MonoBehaviour{

    //DaysToScavenge
    [Header("Days To Scavenge Variables")]
    public int daysToScavenge;
    public TMP_Text daysText;
    public Button addDays;
    public Button removeDays;

    [Header("Settlement Variables")]
    public GameObject settlementGO;
    public Settlement settlement;

    public TMP_Text settlerText;
    public GameObject settlerContent;
    public GameObject scavengerContent;

    public int foundFood, foundWater, foundMetal, foundWood;

    [Header("Scavenge UI Button")]
    public Button scavengeButton;
    public GameObject _informationLoader;

    void Start(){
        daysToScavenge = 1;
    }

    void Update(){
        if(settlement == null){
            settlementGO = GameObject.FindGameObjectWithTag("SettlementTag");
            settlement = settlementGO.GetComponent<Settlement>();
        }

        if(int.Parse(daysText.text) != daysToScavenge){
            daysText.text = daysToScavenge.ToString("00");
        }

        if(settlement.scavengers.Count <= 0){
            scavengeButton.interactable = false;
        }else{
            scavengeButton.interactable = true;
        }
    }

    // SCAVENGE MAIN CODE -----------------------------------------------------------

    public void StartScavenging(){
        settlement.isScavenging = true;
        _informationLoader.GetComponent<InformationPanel>().ScavengingPanelClose();
        settlement.daysScavenging = daysToScavenge;
        this.GetComponent<Environment>().actionComplete = true;
        this.GetComponent<Environment>().daysLeft.text = "Returning in " + settlement.daysScavenging.ToString() + " days";
    }

    public void EndScavenging(){
        foundFood = settlement.scavengers.Count * daysToScavenge * Random.Range(2, 5);
        foundWater = settlement.scavengers.Count * daysToScavenge * Random.Range(2, 5);
        foundMetal = settlement.scavengers.Count * daysToScavenge * Random.Range(4, 6);
        foundWood = settlement.scavengers.Count * daysToScavenge * Random.Range(4, 6);

        settlement.food += foundFood;
        settlement.water += foundWater;
        settlement.metal += foundMetal;
        settlement.wood += foundWood;

        settlement.isScavenging = false;
        settlement.moveScavengersToSettlers();
        settlement.scavengers.Clear();
    }

    // Send to Scavengers/Settlers --------------------------------------------------

    public void SendToScavenging(){
        GameObject btn = EventSystem.current.currentSelectedGameObject;
        SettlerDetails settler = btn.GetComponentInParent<SettlerDetails>();
        if(settlement.settlers.Count > 2){
            settlerText.text = "Settlers";
            for(int i = 0; i < settlement.settlers.Count; i++){
                if(settler.id == settlement.settlers[i].GetComponent<Settler>().id){
                    settlement.scavengers.Add(settlement.settlers[i]);
                    settlement.settlers.RemoveAt(i);    
                    settlerContent.GetComponent<PopulateGridSettlers>().Repopulate();
                    scavengerContent.GetComponent<PopulateGridScavengers>().Repopulate();
                }
            }
        }else{
            settlerText.text = "Settlers <color=red>(Require 2 Settlers)</color>";
        }
    }

    public void SendToSettlers(){
        GameObject btn = EventSystem.current.currentSelectedGameObject;
        ScavengerDetails scavenger = btn.GetComponentInParent<ScavengerDetails>();

        for(int i = 0; i < settlement.scavengers.Count; i++){
            if(scavenger.id == settlement.scavengers[i].GetComponent<Settler>().id){
                settlement.settlers.Add(settlement.scavengers[i]);
                settlement.scavengers.RemoveAt(i);
                settlerContent.GetComponent<PopulateGridSettlers>().Repopulate();
                scavengerContent.GetComponent<PopulateGridScavengers>().Repopulate();
            }
        }
    }


    //Days to Scavenge ----------------------------------------------------------------

    public void AddDay(){
        if(daysToScavenge <= 98){
            daysText.color = new Color32(255, 255, 255, 255);
            daysToScavenge += 1;
        }else{
            daysText.color = new Color32(255, 0, 0, 255);
            daysToScavenge = 99;
        }
    }

    public void ReduceDay(){
        if(daysToScavenge >= 2){
            daysText.color = new Color32(255, 255, 255, 255);
            daysToScavenge -= 1;
        }else{
            daysText.color = new Color32(255, 0, 0, 255);
            daysToScavenge = 1;
        }
    }

    // --------------------------------------------------------------------------------


}
