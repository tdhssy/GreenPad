using UnityEngine;

public class BadClick : MonoBehaviour
{

    public GameManage gameManage;

    private void Start(){
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
    }


    private void Update()
    {
        // Détecter les clics (touches de l'écran) sur les objets 2D
        if (Input.GetMouseButtonDown(0)) // Clic gauche
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject objetClique = hit.collider.gameObject;
                if (objetClique == gameObject)
                {
                    if(gameManage!=null){
                        gameManage.ResetScore();
                        gameManage.UnloadClick();
                    }
                }
            }
        }
    }

}
