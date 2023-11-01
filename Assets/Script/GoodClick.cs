using UnityEngine;

public class GoodClick : MonoBehaviour
{

    private GameManage gameManage;
    private AudioSource audioClick;

    private void Start(){

        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        audioClick = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();;
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
                    audioClick.Play();
                    Destroy(gameObject);
                }
            }
        }
    }

}
