using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rig;
    [SerializeField]
    private int maxArea;

    [HideInInspector]
    public Vector3 initPos;

    private Vector2 touchBeganPos, touchEndPos;    
    [SerializeField]
    private float deadZone;    

    [SerializeField]
    private TextMeshProUGUI txtLife;
    private int life = 3;

    void Start()
    {
        rig = GetComponent<Rigidbody>();

        initPos = transform.position;
    }

    void Update()
    {
        InputManager();
        LimitMove();

        GameOver();
    }

    void InputManager()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
                touchBeganPos = touch.position;

            if(touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;

                if(touchBeganPos.x + deadZone >= touchEndPos.x && touchBeganPos.x - deadZone <= touchEndPos.x)
                    Move(0, 1);
                if(touchBeganPos.x + deadZone < touchEndPos.x)
                    Move(1, 0);
                if(touchBeganPos.x - deadZone > touchEndPos.x)
                    Move(-1, 0);                
            }           
        }        
    }

    void Move(int dirX, int dirZ)
    {
        transform.position = new Vector3(transform.position.x + dirX, transform.position.y, transform.position.z + dirZ);        
    }

    void LimitMove()
    {
        if (transform.position.x > (maxArea / 2))
            transform.position = new Vector3((maxArea / 2), transform.position.y, transform.position.z);
        if (transform.position.x < -(maxArea / 2))
            transform.position = new Vector3(-(maxArea / 2), transform.position.y, transform.position.z);

        if(transform.position.y < -0.5f)
            LostLife();
    }

    void LostLife()
    {
        life --;
        transform.position = initPos;
    }

    void GameOver()
    {
        txtLife.text = "Vidas: " + life;

        if(life <= 0)
            GameController.RestartLevel();
    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Obstacle":
                LostLife();
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
