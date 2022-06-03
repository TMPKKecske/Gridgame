using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefPosResetterScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PieceScript Piece = collision.gameObject.GetComponentInParent<PieceScript>();
            Piece.DefPos = Piece.StartPos;
            Piece.IsConquering = false;
        }
    }
}
