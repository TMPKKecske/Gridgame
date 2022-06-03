using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    public bool IsHighLighted = false;
    public bool IsTriggered = false;
    public bool IsConquvered = false;
    public bool IsDisabled = false;
    public int GridPosX;
    public int GridPosY;
    public PieceScript TriggeringPeace;
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsDisabled) 
        {     
                if (IsConquvered == false) 
                {
                    IsTriggered = true;
                    TriggeringPeace = collision.GetComponentInParent<PieceScript>();  ;
                }                    
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
 
            if (collision.gameObject.GetComponentInParent<PieceScript>() == TriggeringPeace && !IsDisabled)
            {
                IsTriggered = false;
                IsConquvered = false;
                IsHighLighted = false;
            }
            else
            {
                IsTriggered = false;
                IsHighLighted = false;
            }

        //IsHighLighted = false;        
    }

    public void Update()
    {
        if (IsHighLighted && !IsDisabled)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        else 
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        }

        if (IsDisabled)        
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }
    }
}
