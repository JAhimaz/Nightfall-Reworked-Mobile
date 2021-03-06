﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettlerDetails : MonoBehaviour{

    //Text Fields
    public TMP_Text nameField;
    public TMP_Text ageGenderField;
    public TMP_Text healthField;

    //Buttons
    public Button sendToScavengingBTN;

    //Values
    public int id, settlerHealth, settlerAge;
    public string settlerName;
    public string settlerGender;

    //bools
    bool isScavening, isBuilding;

    public void InitialiseValues(int id, string firstname, string lastname, int age, int health, string gender, bool isScavenging, bool isBuilding){
        this.id = id;
        settlerName = firstname + " " + lastname;
        settlerAge = age;
        settlerGender = gender;
        settlerHealth = health;

        nameField.text = settlerName;
        ageGenderField.text = gender + " " + age.ToString();
        healthField.text = health.ToString();

        if(sendToScavengingBTN != null){
            if(isBuilding){
                sendToScavengingBTN.interactable = false;
            }else{
                sendToScavengingBTN.interactable = true;
            }
        }

    }

    // Start is called before the first frame update
    void Start(){
        
    }
}
