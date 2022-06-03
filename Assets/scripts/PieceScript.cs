using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    private Collider2D ObjectCollider;
    private GameManager GameManager;

    public Vector3 DefPos;
    public Vector3 StartPos;
    public Camera Cam;
    public GridController Grid;
    public int MinSquareAmount;
    public Vector3 GripPosClip;
    public Vector3 GripPosClipRotated;
    public bool CanMove = true; //disables movement. It will be very amongusly sus for a new gamemode (a gamemode where you place down a square and then you can't move it anymore)
    public bool IsRotated = false;
    public bool IsConquering = false;
    private float tapSpeed = 0.3f;
    private float lastTapTime = 0;

    private void Start()
    {
        DefPos = transform.position;
        StartPos = transform.position;
        ObjectCollider = GetComponent<BoxCollider2D>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (Input.touchCount > 0 && CanMove)
        {
            var wp = Cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            var touchPosition = new Vector2(wp.x, wp.y);
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (ObjectCollider.OverlapPoint(touchPosition))
                {
                    if (GameManager.WhichTouch == gameObject.name)
                    {
                        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);
                        wp.z = 0;
                        transform.position = wp;
                        gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (ObjectCollider.OverlapPoint(touchPosition) && GameManager.WhichTouch == "")
                {
                    GameManager.WhichTouch = gameObject.name;
               

                if ((Time.time - lastTapTime) < tapSpeed && !IsConquering)
                {
                    if (!IsRotated)
                    {
                        IsRotated = true;
                        Vector3 rotaion = new Vector3(0f, 0f, 90f);
                        transform.rotation = Quaternion.Euler(rotaion);
                    } else
                    {
                        IsRotated = false;
                        Vector3 rotaion = new Vector3(0f, 0f, 0f);
                        transform.rotation = Quaternion.Euler(rotaion);
                    }
                }

                lastTapTime = Time.time;
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                GameManager.WhichTouch = "";
                if (ObjectCollider.OverlapPoint(touchPosition))
                {
                    Grid.StartSnap();
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                    gameObject.GetComponentInParent<Transform>().position = DefPos;
                    gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
                }
            }

        }
    }
}