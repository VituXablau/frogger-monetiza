using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefFrog;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX("Goal");
            GameManager.instance.IncreaseScore(100 + (int)GameManager.instance.timeToGoal);
            GameManager.instance.ResetTimer();

            Instantiate(prefFrog, transform.position, Quaternion.identity);
            gameObject.SetActive(false);

            col.gameObject.transform.position = col.gameObject.GetComponent<PlayerController>().initPos;
        }
    }
}
