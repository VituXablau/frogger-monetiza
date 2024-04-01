using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefObj;

    [SerializeField]
    private float minTime, maxTime;
    private float waitSeconds;

    [SerializeField]
    private int dirLookX;
    
    void Start()
    {
        StartCoroutine(Spawn());        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            waitSeconds = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(waitSeconds);

            GameObject obj = Instantiate(prefObj, transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<ObjectsController>().dirX = dirLookX;
        }
    }
}
