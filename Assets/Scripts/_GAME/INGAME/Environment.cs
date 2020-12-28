using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Environment : MonoBehaviour{

    [Header("Main Game Panel")]
    public GameObject mainGamePanel;
    public TMP_Text daysLeft;

    [Header("Death Panel")]
    public GameObject deathPanel;
    public TMP_Text deathReason;
    public TMP_Text daySurvived;
    public TMP_Text bestDays;

    //Panels
    [Header("Next Day Panel")]
    public GameObject nextDayPanel;
    public TMP_Text nextDayText;
    public TMP_Text nextDayInfo;

    [Header("Prefabs")]
    public GameObject settlerPrefab;
    public GameObject settlerParent;
    public GameObject settlementPrefab;
    public GameObject settlementGO;
    private Settlement settlement;

    [Header("Scripts")]
    public UpdateValues updateValues;

    [Header("Buttons")]
    //Buttons
    public Button actionButton;
    public Button nextDayButton;
    public Button scavengingActionButton;
    public Button doNothingButton;

    [Header("Game Variables")]
    //Game Variables
    public int day;
    public bool actionComplete = false;
    bool newGame = true;
    public List<string> dayMessages = new List<string>(); 

    // Start is called before the first frame update
    void Start(){
        if(newGame){
            GameObject newSettlement = Instantiate(settlementPrefab);
            settlementGO = newSettlement;
            settlement = settlementGO.GetComponent<Settlement>();
            newSettlement.gameObject.name = "_Settlement";
            day = 1;
        }else if(!newGame){

        }
    }

    public void nextDay(){

        nextDayInfo.text = "";
        dayMessages.Clear();
        actionComplete = false;
        day += 1;

        settlementConsume();

        if(settlement.isScavenging){

            daysLeft.text = "Returning in " + settlement.daysScavenging.ToString() + " days";

            if(settlement.daysScavenging > 0){
                dayMessages.Add("Your Scavengers will return in <color=orange>" + settlement.daysScavenging + "</color> day(s)");
                settlement.daysScavenging -= 1;
            }else if(settlement.daysScavenging == 0){
                this.GetComponent<ScavengeLogic>().EndScavenging();
                dayMessages.Add("Your Scavengers Have Returned From Scavenging: \n Water Found: " +  this.GetComponent<ScavengeLogic>().foundWater
                + "\n Food Found: " + this.GetComponent<ScavengeLogic>().foundFood
                + "\n Metal Found: " +  this.GetComponent<ScavengeLogic>().foundMetal
                + "\n Wood Found: " + this.GetComponent<ScavengeLogic>().foundWood);
            }else{
                Debug.Log("Error code: Vulture");
            }
        }

        //Run Random
        runRandomEvents();
        nextDayText.text = "DAY " + day.ToString("000");

        if(dayMessages.Count == 0){
            nextDayInfo.text = "A day passes with no significance...";
        }else{
            for(int i = 0; i < dayMessages.Count; i++){
                nextDayInfo.text += dayMessages[i] + "\n";
            }
        }

        nextDayPanel.SetActive(true);
    }

    // Settlement Consume
    public void settlementConsume(){

        //Initial Check
        if(settlement.food == 0 || settlement.water == 0){
            if(settlement.food == 0){
                settlement.noFood = true;
                settlement.daysWithoutFood += 1;
                dayMessages.Add("<color=red>- Your Settlement is out of Food.</color>");
            }

            if(settlement.water == 0){
                settlement.noWater = true;
                settlement.daysWithoutWater += 1;
                dayMessages.Add("<color=red>- Your Settlement is out of Water.</color>");
            }

        }        
        //Consuming food
        if(settlement.food > 0){
            settlement.noFood = false;
            settlement.daysWithoutFood = 0;
            if((settlement.food - (settlement.settlers.Count * 2)) < 0){
                settlement.food = 0;
            }else{
                settlement.food -= (settlement.settlers.Count * 2);
            }
        }

        //Consuming Water
        if(settlement.water > 0){
            settlement.noWater = false;
            settlement.daysWithoutWater = 0;
            if((settlement.water - settlement.settlers.Count) < 0){
                settlement.water = 0;
            }else{
                settlement.water -= (settlement.settlers.Count);
            }
        }

        
    }

    // Random Events
    private void runRandomEvents(){
        float randomValue = Random.Range(0.0f, 100.0f);
        Debug.Log(randomValue);
        if(randomValue <= 1.0f){
            //Nuclear Winter
        }else if(randomValue > 1.0f && randomValue < 15.0f){
            //Settler Joins
            SettlerJoinEvent();
        }else if(randomValue >= 15.0f && randomValue < 20.0f){
            //Airdrop
            AirDropEvent();
        }else if(randomValue >= 20.0f && randomValue < 25.0f){
            //Robbed
            SettlementRobbedEvent();
        }else if(randomValue >= 25.0f && randomValue < 30.0f){
            //Attacked
        }else if(randomValue >= 30.0f && randomValue < 40.0f){
            //Weather Event
        }else if(randomValue >= 40.0f && randomValue < 45.0f){
            //
        }else if(randomValue >= 45.0f && randomValue < 50.0f){

        }else{

        }
    }

    private void SettlerJoinEvent(){
        int numberOfSettlers = Random.Range(1, 3);
        for(int i = 0; i < numberOfSettlers; i++){
            settlement.addSettler();
        }
        dayMessages.Add("<color=green>+ </color><color=white>" + numberOfSettlers + "</color><color=green> Settlers Have Joined Your Settlement</color>");
    }

    private void AirDropEvent(){
        int airDropWater = Random.Range(10, 60);
        settlement.water += airDropWater;

        int airDropFood = Random.Range(10, 60);
        settlement.food += airDropFood;

        int airDropMetal = Random.Range(0, 30);
        settlement.metal += airDropMetal;

        int airDropWood = Random.Range(0, 30);
        settlement.wood += airDropWood;

        dayMessages.Add("<color=green>+ An Airdrop Has Dropped Near Your Settlement! \n Water Found: " + airDropWater + "\n Food Found: " + airDropFood + "\n Metal Found: " + airDropMetal
        + "\n Wood Found: " + airDropWood + "</color>");
    }

    private void SettlementRobbedEvent(){
        dayMessages.Add("<color=red>Your Settlement was robbed during the night!</color>");
        float r1 = Random.Range(0.0f, 100.0f);
        if(r1 <= 45.0f){
            dayMessages.Add("<color=red>The Robber had gotten away some supplies</color>");
            settlement.food -= Random.Range(1, 3);
            settlement.water -= Random.Range(1, 3);
            settlement.metal -= Random.Range(1, 3);
            settlement.wood -= Random.Range(1, 3);
        }else if(r1 > 45.0f){
            dayMessages.Add("<color=green>Your settlers had caught the robber</color>");
            int r2 = Random.Range(0, 2);
            if(r2 == 1){
                dayMessages.Add("The robber flees from your settlers and escapes into the night");
            }else{
                dayMessages.Add("<color=green>The robber joins your settlement to help (+1 Settler)</color>");
                settlement.addSettler();
            }
        }
    }

    // End of Random Events

    public void closeNextDayPanel(){
        nextDayPanel.SetActive(false);
    }

    public void completeAction(){
        actionComplete = true;
    }

    void Update() {

        if(settlement.food < 0){
            settlement.food = 0;
        }

        if(settlement.water < 0){
            settlement.water = 0;
        }

        if(actionComplete && actionButton.interactable == true){
            actionButton.interactable = false;
            nextDayButton.interactable = true;

            doNothingButton.interactable = false;
        }
        if(!actionComplete && actionButton.interactable == false){
            actionButton.interactable = true;
            nextDayButton.interactable = false;

            doNothingButton.interactable = true;
        }

        if(settlement.isScavenging){
            scavengingActionButton.interactable = false;
        }else{
            scavengingActionButton.interactable = true;
        }

        if(settlement.daysWithoutFood >= 6 || settlement.daysWithoutWater >= 3 || settlement.settlers.Count == 0){
            mainGamePanel.SetActive(false);
            if(settlement.daysWithoutFood >= 6){
                deathReason.text = "Your Settlers Starved To Death";
            }else if(settlement.daysWithoutWater >= 3){
                deathReason.text = "Your Settlers Died of Dehydration";
            }else if(settlement.settlers.Count == 0){
                deathReason.text = "All Your Settlers Died";
            }else{
                deathReason.text = "Unknown Causes...";
            }

            daySurvived.text = day.ToString("000");

            if(day > PlayerPrefs.GetInt("BestDays") || PlayerPrefs.GetInt("BestDays") == null){
                PlayerPrefs.SetInt("BestDays", day);
            }

            bestDays.text = "Best Days Survived: " + PlayerPrefs.GetInt("BestDays");
            
            deathPanel.SetActive(true);
        }

    }
}
