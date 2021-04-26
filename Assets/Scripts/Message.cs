using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField]
    Text text;

    [SerializeField]
    float textDuration;

    float currentDelay;
    Queue<string> messages = new Queue<string>();

    public void PushMessage(string message) => messages.Enqueue(message); 

    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0)
        {
            if (messages.Count > 0)
            {
                StartCoroutine(ShowMessage(messages.Dequeue()));
                currentDelay = textDuration;
            }
            else
            {
                currentDelay = 0;
            }
        }
    }

    IEnumerator ShowMessage(string message)
    {
        text.text = message.Replace(@"\n", "\n");

        float a = 0.0f;
        while (a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            a += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(textDuration);

        a = 1.0f;
        while (a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            a -= Time.deltaTime;
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.text = "";
    }
}
