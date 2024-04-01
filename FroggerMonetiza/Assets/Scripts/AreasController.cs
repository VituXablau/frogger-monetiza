using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreasController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefFrog;

    void OnTriggerEnter(Collider col) 
    {
        if(col.CompareTag("Player"))
        {
            Instantiate(prefFrog, transform.position, Quaternion.identity);
            gameObject.SetActive(false);

            col.gameObject.transform.position = col.gameObject.GetComponent<PlayerController>().initPos;
        }
    }
}
