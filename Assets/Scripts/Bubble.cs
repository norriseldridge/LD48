using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    float lifeTime;

    [SerializeField]
    float upSpeed;

    [SerializeField]
    float swayAmount;

    [SerializeField]
    float swaySpeed;

    float currentLifetime;

    public void AddVariance()
    {
        swayAmount = Random.Range(swayAmount / 3, swayAmount);
        swaySpeed = Random.Range(swaySpeed / 2, swaySpeed * 1.5f);
        upSpeed = Random.Range(upSpeed * 0.8f, upSpeed * 1.1f);
        transform.localScale = Vector3.one * Random.Range(0.6f, 1.05f);
    }

    // Update is called once per frame
    void Update()
    {
        var sway = Mathf.Sin(swaySpeed * Time.time) * swayAmount;
        transform.position += new Vector3(sway, upSpeed * Time.deltaTime, 0);

        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
            Pop();
    }

    IEnumerator InternalPop()
    {
        for (int i = 0; i < 30; ++i)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);
            yield return null;
        }

        gameObject.SetActive(false);
    }

    public void Pop() => StartCoroutine(InternalPop());

    public void Reset()
    {
        gameObject.SetActive(true);
        currentLifetime = lifeTime;
        transform.localScale = Vector3.one;
    }
}
