
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    [SerializeField]
    private float spd;
    [HideInInspector]
    public int dirX;

    [SerializeField]
    private int maxArea;

    void Update()
    {
        Move();
        LimitMove();
    }

    void Move()
    {
        transform.Translate(new Vector3(dirX * spd, 0, 0) * Time.deltaTime);
    }

    void LimitMove()
    {
        if (transform.position.x > (maxArea / 2))
            Destroy(gameObject);
        if (transform.position.x < -(maxArea / 2))
            Destroy(gameObject);
    }
}
