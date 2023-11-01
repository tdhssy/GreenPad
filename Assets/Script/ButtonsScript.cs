using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    private GameManage gameManage;
    public Button btn_start;
    public Button btn_reset;

    private void Start(){
        
    }

    public void OnPlayButton(){
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        btn_start.gameObject.SetActive(false);
        btn_reset.gameObject.SetActive(false);
        gameManage.Launch();
    }

    public void OnResetButton(){
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        gameManage.ResetPersonScore();
    }
}
