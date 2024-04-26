using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float totalTime;
    private float timeToGoal;
    private bool isCounting;

    // Start is called before the first frame update
    void Start()
    {
        timeToGoal = totalTime;
        isCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        while (isCounting)
        {
            timeToGoal -= Time.deltaTime;

            if (timeToGoal <= 0)
            {
                ResetTimer();
            }
        }
    }

    void ResetTimer()
    {
        timeToGoal = totalTime;
    }
}
