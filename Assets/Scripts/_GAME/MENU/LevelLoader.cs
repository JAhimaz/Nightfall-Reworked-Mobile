using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour{

    public GameObject loadingPanel;
    public Slider slider;
    public TMP_Text bestDays;
    

    public TMP_Text[] loadingText = new TMP_Text[5];

    public void LoadLevel(int sceneIndex){
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    void Start(){
        bestDays.text = "Highest Days Survived: " + PlayerPrefs.GetInt("BestDays");
        loadingPanel.SetActive(false);
        for(int i = 0; i < loadingText.Length; i++){
            loadingText[i].text = "";
        }
    }

    IEnumerator LoadAsynchronously (int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingPanel.SetActive(true);

        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress * 100;
            if(progress * 100 < 20){
                loadingText[0].text = "Generating Settlement...";
            }
            if(progress * 100 < 40){
                loadingText[1].text = "Creating Settlers...";
            }
            if(progress * 100 < 60){
                loadingText[2].text = "Nuking World...";
            }
            if(progress * 100 < 80){
                loadingText[3].text = "Reading Weather Reports...";
            }
            if(progress * 100 < 80){
                loadingText[4].text = "Done Loading!";
            }
            yield return null;
        }
    }

}
