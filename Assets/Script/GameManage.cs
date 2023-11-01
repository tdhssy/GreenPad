using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{

    private GameObject[] gameUI;
    private TextMeshProUGUI[] timeNumber;
    private TextMeshProUGUI[] scoreNumber;
    private TextMeshProUGUI[] bestScoreNumber;
    private Spawners spawner;
    private static int score;
    private static int bestScore;
    public double timeInit;
    private double time;
    private double timeModif;
    private bool isTimeout = false;
    private int diffCurve = 10;
    private bool isStart = false;

    // Start is called before the first frame update
    private void  Start()
    {   
        time=timeInit;
        timeModif=timeInit;

        score=0;
        bestScore = PlayerPrefs.GetInt("BestScore",0);



        bestScoreNumber = GameObject.FindGameObjectsWithTag("BSnumber")
        .Select(objet => objet.GetComponent<TextMeshProUGUI>())
        .ToArray();


        scoreNumber = GameObject.FindGameObjectsWithTag("Snumber")
        .Select(objet => objet.GetComponent<TextMeshProUGUI>())
        .ToArray();

        timeNumber = GameObject.FindGameObjectsWithTag("Tnumber")
        .Select(objet => objet.GetComponent<TextMeshProUGUI>())
        .ToArray();

        gameUI = GameObject.FindGameObjectsWithTag("gameUI");

        spawner = GameObject.FindWithTag("spawn").GetComponent<Spawners>();

        gameUI.ToList().ForEach(objet => objet.SetActive(false));
        bestScoreNumber.ToList().ForEach(objet => objet.text=bestScore.ToString());


    }

    public void Launch(){
        gameUI.ToList().ForEach(objet => objet.SetActive(true));

        scoreNumber.ToList().ForEach(objet => objet.text=score.ToString());
        timeNumber.ToList().ForEach(objet => objet.text=timeInit.ToString());

        spawner.Launch();
        isStart=true;

    }

    public void AddScore()
    {
        score++;
        if(scoreNumber!=null){
            scoreNumber.ToList().ForEach(objet => objet.text=score.ToString());
        }
    }

    public void ResetPersonScore(){
        PlayerPrefs.DeleteAll();
        bestScore = 0;
        bestScoreNumber.ToList().ForEach(objet => objet.text=bestScore.ToString());
    }

    public void ResetScore(){
        SaveData();
        score = 0;
        if(scoreNumber!=null){
            scoreNumber.ToList().ForEach(objet => objet.text=score.ToString());
           
        }
        timeModif=timeInit;
        diffCurve=10;
        spawner.ResetNmbrGood();
        Invoke(nameof(ReloadCurrentScene), 0.1f);

    }

    public void UnloadClick(){
        if(spawner!=null){
            spawner.Despawn(); 
        }
        time=timeModif;
        isTimeout=false;
    }

    public void ReloadClick(){
        if(spawner!=null){
            spawner.Despawn();
            spawner.Spawn();
            
        }
        time=timeModif;
        isTimeout=false;
        

    }

    void ReloadCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    //Method Time Up once
    private void Timeout(){
        time -= Time.deltaTime;

        if(timeNumber!=null){
            timeNumber.ToList().ForEach(objet => objet.text=Math.Round(time,2).ToString());
        }

        if(time<=0 && !isTimeout){
            isTimeout=true;
            ResetScore();
            ReloadClick();

        }

    }

    public void SaveData(){
        int bestScore = PlayerPrefs.GetInt("BestScore",0);
        if(score>bestScore){
            PlayerPrefs.SetInt("BestScore",score);
            PlayerPrefs.Save();
            bestScoreNumber.ToList().ForEach(objet => objet.text=bestScore.ToString());
        }
    }


    private void Difficulty(){

        if(score>0 && score%diffCurve==0){
            diffCurve+=10;
            spawner.AddnmbrGood();
            if(timeModif>1.5f){
                timeModif-=0.4f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart){
            Timeout();
            Difficulty();
        }
        
    }
}
