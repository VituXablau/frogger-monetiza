using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject[] areas;

    void Update()
    {
        if (FilledAreas())
        {
            GameManager.instance.IncreaseScore(200);
            GameManager.instance.NextLevel();
        }
    }

    private bool FilledAreas()
    {
        foreach (GameObject area in areas)
        {
            if (area.activeSelf)
                return false;
        }
        return true;
    }
}
