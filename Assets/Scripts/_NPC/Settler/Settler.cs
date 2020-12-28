using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : MonoBehaviour{
    
    //Final
    public int id;
    public string firstName;
	public string lastName;
	public char gender;

    public string desc, likes, dislikes;

    //Dynamic
    public int age;
	public int health;
    public bool isScavenging, isBuilding, isHungry, hasGun;

    void Awake(){
        int genderSelect = Random.Range(0, 2);
        if(genderSelect == 0){
            this.gender = 'M';
            this.firstName = Misc.firstNamesMale[Random.Range(0, Misc.firstNamesMale.Length)];
        }else if(genderSelect == 1){
            this.gender = 'F';
            this.firstName = Misc.firstNamesFemale[Random.Range(0, Misc.firstNamesFemale.Length)];
        }

		this.lastName = Misc.lastNames[Random.Range(0, Misc.lastNames.Length)];//Random Generation	
		this.age = (Random.Range(20, 40)); //All settlers found will be above 20 unless born.
		this.health = (Random.Range(95, 100));
		this.isScavenging = false;
		this.isBuilding = false;
		this.isHungry = false;
        this.likes = Misc.preferences[Random.Range(0, Misc.preferences.Length)];
        this.dislikes = Misc.preferences[Random.Range(0, Misc.preferences.Length)];
		this.hasGun = false;
    }
}
