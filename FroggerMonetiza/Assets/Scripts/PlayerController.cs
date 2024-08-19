using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rig;
    private Animator animChild;

    private static GameObject frogger;

    [SerializeField] private int maxArea;
    [SerializeField] private float deadZone;

    [HideInInspector] public Vector3 initPos;
    
    private Vector2 touchBeganPos, touchEndPos;
    

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        animChild = GetComponentInChildren<Animator>();

        initPos = transform.position;

        frogger = gameObject;
        DontDestroyOnLoad(frogger);
    }

    void Update()
    {
        InputManager();
        LimitMove();
    }

    void InputManager()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                touchBeganPos = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;

                animChild.SetTrigger("Jumping");

                if (touchBeganPos.x + deadZone >= touchEndPos.x && touchBeganPos.x - deadZone <= touchEndPos.x)
                {
                    Move(0, 1);
                    transform.rotation = Quaternion.Euler(0, 0, 0);                    
                    GameManager.instance.IncreaseScore(10);
                }
                if (touchBeganPos.x + deadZone < touchEndPos.x)
                {
                    Move(1, 0);
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                if (touchBeganPos.x - deadZone > touchEndPos.x)
                {
                    Move(-1, 0);
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                }                
            }
        }
    }

    void Move(int dirX, int dirZ)
    {
        AudioManager.instance.PlaySFX("Hop");

        transform.position = new Vector3(transform.position.x + dirX, transform.position.y, transform.position.z + dirZ);
    }

    void LimitMove()
    {
        if (transform.position.x > (maxArea / 2))
            transform.position = new Vector3((maxArea / 2), transform.position.y, transform.position.z);
        if (transform.position.x < -(maxArea / 2))
            transform.position = new Vector3(-(maxArea / 2), transform.position.y, transform.position.z);

        if (transform.position.y < -0.5f)
            LoseLife();
    }

    void LoseLife()
    {
        AudioManager.instance.PlaySFX("Death");

        if (GameManager.instance.gameLives > 0)
        {
            GameManager.instance.gameLives--;

            for (int i = GameManager.instance.lives.Length - 1; i >= 0; i--)
            {
                if (GameManager.instance.lives[i].activeSelf)
                {
                    GameManager.instance.lives[i].SetActive(false);
                    break;
                }
            }

            transform.position = initPos;
        }
        else
        {
            DestroyItself();
            GameManager.instance.GameOver();
        }
    }

    public static void DestroyItself()
    {
        Destroy(frogger);
    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Obstacle":
                LoseLife();
                break;
            case "Platform":
                transform.position = new Vector3(transform.position.x, col.transform.position.y + 1, transform.position.z);
                Transform curParent = col.transform;
                transform.SetParent(curParent);
                rig.isKinematic = true;
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Platform":
                transform.SetParent(null);
                rig.isKinematic = false;
                break;
        }
    }
}
