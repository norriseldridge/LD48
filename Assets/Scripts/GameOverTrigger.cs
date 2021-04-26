using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField]
    GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            gameController.EndGame();
            gameObject.SetActive(false);
        }
    }
}
