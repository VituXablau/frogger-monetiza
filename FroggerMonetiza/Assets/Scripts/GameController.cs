using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] areas;

    void Update()
    {
        if(FilledAreas())
            Debug.Log("Acabou");
    }
    
    private bool FilledAreas()
    {
        foreach(GameObject area in areas)
        {
            if(area.activeSelf)
                return false;
        }
        return true;
    }

    public static void NextLevel()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(curSceneIndex + 1);
    }

    public static void RestartLevel()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(curSceneIndex);
    }
}
