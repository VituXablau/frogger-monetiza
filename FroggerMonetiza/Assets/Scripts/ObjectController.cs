
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField]
    private float spd;
    [HideInInspector]
    public int dirX;

    [SerializeField]
    private int maxArea;

    void Update()
    {
        if (gameObject.name.Contains("Aligator"))
            Destroy(gameObject, 3);
        else
        {
            Move();
            LimitMove();
        }
    }

    void Move()
    {
        transform.position += new Vector3(dirX * spd, 0, 0) * Time.deltaTime;
    }

    void LimitMove()
    {
        if (transform.position.x > (maxArea / 2))
            Destroy(gameObject);
        if (transform.position.x < -(maxArea / 2))
            Destroy(gameObject);
    }
}
