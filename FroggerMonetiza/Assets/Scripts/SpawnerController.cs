using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefObj, levelControllerObj;

    [SerializeField]
    private float minTime, maxTime;
    private float waitSeconds;

    [SerializeField]
    private int dirLookX;

    void Start()
    {
        if (gameObject.name.Contains("Aligator"))
            StartCoroutine(AligatorSpawn());
        else
            StartCoroutine(CarAndPlatformSpawn());
    }

    IEnumerator CarAndPlatformSpawn()
    {
        while (true)
        {
            waitSeconds = Random.Range(minTime, maxTime);
            GameObject obj = Instantiate(prefObj, transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<ObjectController>().dirX = dirLookX;
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    IEnumerator AligatorSpawn()
    {
        while (true)
        {
            waitSeconds = Random.Range(minTime, maxTime);
            int spawnPoint = Random.Range(0, levelControllerObj.GetComponent<LevelController>().areas.Length);
            yield return new WaitForSeconds(waitSeconds);

            if (levelControllerObj.GetComponent<LevelController>().areas[spawnPoint].activeSelf)
                Instantiate(prefObj, levelControllerObj.GetComponent<LevelController>().areas[spawnPoint].transform.position, Quaternion.identity);
        }
    }
}
