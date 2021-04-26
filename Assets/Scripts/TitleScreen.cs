using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    List<Text> titleText;

    [SerializeField]
    float fadeSpeed;

    [SerializeField]
    ToggleFadeText anyText;

    public void FadeText()
    {
        // gotta disable this so it can be faded out forever
        anyText.enabled = false;

        IEnumerator FadeOut(Text text)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            while (text.color.a > 0)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (fadeSpeed * Time.deltaTime));
                yield return null;
            }
        }

        foreach (var t in titleText)
            StartCoroutine(FadeOut(t));
    }
}
