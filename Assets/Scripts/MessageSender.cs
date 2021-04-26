using UnityEngine;

public class MessageSender : MonoBehaviour
{
    [SerializeField]
    string text;

    [SerializeField]
    Message message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            message.PushMessage(text);
            gameObject.SetActive(false);
        }
    }
}
