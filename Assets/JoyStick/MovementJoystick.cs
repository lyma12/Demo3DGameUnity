using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private GameObject player;
    public float playerSpeed;
    private Rigidbody rb;
    private RectTransform rectTransform;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;
    private Touch theTouch;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            rb = player.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, touchPos))
            {
                theTouch = Input.GetTouch(0);
                if (theTouch.phase == TouchPhase.Began)
                {
                    joystick.transform.position = theTouch.position;
                    joystickBG.transform.position = theTouch.position;
                    joystickTouchPos = theTouch.position;
                }
                else if (theTouch.phase == TouchPhase.Ended)
                {
                    joystickVec = Vector2.zero;
                    joystick.transform.position = joystickOriginalPos;
                    joystickBG.transform.position = joystickOriginalPos;
                }
                else
                {
                    Vector2 dragPos = theTouch.position;
                    joystickVec = (dragPos - joystickTouchPos).normalized;

                    float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

                    if (joystickDist < joystickRadius)
                    {
                        joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
                    }

                    else
                    {
                        joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
                    }
                }
            }
            else
            {
                joystickVec = Vector2.zero;
                joystick.transform.position = joystickOriginalPos;
                joystickBG.transform.position = joystickOriginalPos;
            }
            if (rb != null)
            {
                if (joystickVec.x != 0)
                {

                    rb.velocity = new Vector3(joystickVec.x, 0f, joystickVec.y);
                }
                else
                {
                    rb.velocity = Vector3.zero;
                }
                //rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime);
            }
        }
        

    }


}
