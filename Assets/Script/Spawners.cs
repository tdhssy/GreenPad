using UnityEngine;

public class Spawners : MonoBehaviour
{
    public GoodClick goodClick;
    public BadClick badClick;
    public GameObject[] spawnPoints;
    public int nmbrGoodInit;
    private int nmbrGood;
    private GameManage gameManage;
    private System.Random rand = new System.Random();
    private bool isStart = false;
    private bool isDespawn = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();    
        nmbrGood=nmbrGoodInit;    
    }


    public void Launch(){
        Spawn();
        isStart = true;
    }

    public void Spawn(){
        isDespawn=false;
        int nmbrGoodTmp = (rand.Next()%nmbrGood)+1;

        spawnPoints.Shuffle();
        foreach (GameObject spawner in spawnPoints)
        {
            Vector3 objPos = spawner.transform.position;
            Quaternion objRot = spawner.transform.rotation;

            if(nmbrGoodTmp>0){
                nmbrGoodTmp--;
                Instantiate(goodClick,objPos,objRot);
            }else{
                Instantiate(badClick,objPos,objRot);
            }
        }
        
    }

    public void Despawn(){
        isDespawn=true;
        GameObject[] clicks = GameObject.FindGameObjectsWithTag("click");

        foreach (GameObject item in clicks)
        {
            Destroy(item);
        }

    }

    public bool CheckGoodClick(){

        bool result = false;
        GoodClick[] goodClicks = FindObjectsOfType<GoodClick>();
        foreach (GoodClick item in goodClicks)
        {
            if(item.isActiveAndEnabled){
                result = true;
            }
        }

        return result;
    }

    public void AddnmbrGood(){
        if(nmbrGood<spawnPoints.Length-2){
            nmbrGood += 1;
        }

    }

    public void ResetNmbrGood(){
        nmbrGood = nmbrGoodInit;
    }


    // Update is called once per frame
    void Update()
    {

        if(isStart && !isDespawn && !CheckGoodClick() && gameManage!=null){
            gameManage.AddScore();
            gameManage.ReloadClick();

        }

    }


}
