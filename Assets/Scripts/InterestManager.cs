using System.Collections.Generic;
using UnityEngine;

public class InterestManager : MonoBehaviour
{
    [SerializeField]
    Transform interestTarget;

    [SerializeField]
    List<GameObject> interestingStuff;

    [SerializeField]
    int interestingThingsPerMinute;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float spawnRange;

    float maxDelay;
    float currentDelay;

    private void Start()
    {
        maxDelay = 60.0f / interestingThingsPerMinute;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0.0f)
        {
            Reset();

            var nextThing = NextInterestingThing();
            if (nextThing != null)
            {
                nextThing.transform.position = spawnPoint.position + RandomSpawnOffset();

                foreach (var resetable in nextThing.GetComponents<IResetable>())
                    resetable.Reset();
            }
        }
    }

    Vector3 RandomSpawnOffset()
    {
        var x = Random.value > 0.5f ? 30 : -30;
        return new Vector3(x + Random.Range(-spawnRange, spawnRange), 0, 0);
    }

    void Reset()
    {
        currentDelay = Random.Range(maxDelay * 0.6f, maxDelay);
    }

    GameObject NextInterestingThing()
    {
        GameObject thing = null;
        bool isValid = false;
        while (!isValid)
        {
            int index = Random.Range(0, interestingStuff.Count);
            thing = interestingStuff[index];
            interestingStuff.RemoveAt(index);

            // insert this at a new index
            index = Random.Range(0, interestingStuff.Count);
            interestingStuff.Insert(index, thing);

            // is this a valid thing?
            isValid = IsValid(thing);
        }

        return thing;
    }

    bool IsValid(GameObject thing)
    {
        if (Vector3.Distance(thing.transform.position, interestTarget.position) > 25)
        {
            return true;
        }

        return false;
    }
}
