using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject[,] GridRows = new GameObject[8, 8];
    public GameObject[] GridColoumns = new GameObject[8];

    void Start()
    {
        //declaring game grid peaces
        int i = 0;
        foreach (GameObject G in GridColoumns)
        {
            GridPiece[] allChildren = G.GetComponentsInChildren<GridPiece>();
            int i2 = 0;
            foreach (GridPiece child in allChildren)
            {
                child.GridPosY = i;
                child.GridPosX = i2;
                GridRows[i, i2] = child.gameObject;
                i2++;
            }
            i++;
        }
    }

    //this may seem weird but it's for testing porpouses
    private void Update()
    {
        foreach (GridPiece gridpeace in GetActiveGridPieces())
        {
            gridpeace.IsHighLighted = true;
        }
        //Debug.Log(GetActiveGridPeaces().Length);
    }

    public void StartSnap()
    {
        //Debug.Log("Start Snap");
        GridPiece HighestPiece = HighestPeace();

        if (GetActiveGridPieces().Length > 0)
        {
            if (HighestPiece.TriggeringPeace.MinSquareAmount == GetActiveGridPieces().Length)
            {
                //Debug.Log("Started a snap");
                if (!HighestPiece.TriggeringPeace.IsRotated)
                {
                    HighestPiece.TriggeringPeace.DefPos = (HighestPiece.transform.position + HighestPiece.TriggeringPeace.GripPosClip);
                }
                else
                {
                    HighestPiece.TriggeringPeace.DefPos = (HighestPiece.transform.position + HighestPiece.TriggeringPeace.GripPosClipRotated);
                }

                //Debug.Log(HighestPiece.GridPosX + " " + HighestPiece.GridPosY);
                foreach (GridPiece G in GetActiveGridPieces())
                {
                    G.IsTriggered = false;
                    G.IsConquvered = true;
                    G.TriggeringPeace.IsConquering = true; //maybe false
                }
            }
            else
            {
                //Debug.Log("No peaces");
                HighestPiece.TriggeringPeace.DefPos = HighestPiece.TriggeringPeace.StartPos;
                HighestPiece.TriggeringPeace.IsConquering = false;
            }
        }

    }

    GridPiece HighestPeace()
    {
        int HighX = 0;
        int HighY = 0;
        GridPiece HighestPiece = new GridPiece();
        foreach (GridPiece gridpeace in GetActiveGridPieces())
        {
            if (gridpeace.GridPosY >= HighY && gridpeace.GridPosX >= HighX)
            {
                HighestPiece = gridpeace;
                // gridpeace.IsTriggered = false;
            }
            //Debug.Log("Gridpeace highlited: " + gridpeace.GridPosX + " " + gridpeace.GridPosY);
        }
        return HighestPiece;
    }

    GridPiece[] GetActiveGridPieces()
    {
        List<GridPiece> Activated = new List<GridPiece>();
        for (int i = 0; i < 8; i++)
        {
            for (int i2 = 0; i2 < 8; i2++)
            {
                if (GridRows[i, i2].GetComponent<GridPiece>().IsTriggered && !GridRows[i, i2].GetComponent<GridPiece>().IsConquvered)
                {
                    Activated.Add(GridRows[i, i2].GetComponent<GridPiece>());
                }
            }
        }
        return Activated.ToArray();
    }

    public GridPiece[] GetConqueredPieces()
    {
        List<GridPiece> Conquevered = new List<GridPiece>();
        for (int i = 0; i < 8; i++)
        {
            for (int i2 = 0; i2 < 8; i2++)
            {
                if (GridRows[i, i2].GetComponent<GridPiece>().IsConquvered)
                {
                    Conquevered.Add(GridRows[i, i2].GetComponent<GridPiece>());
                }
            }
        }
        return Conquevered.ToArray();
    }

    public void SetDisabledPeaces(List<int[]> DisablesCoordinates)
    {
        foreach (int[] cord in DisablesCoordinates)
        {
            GridRows[cord[0], cord[1]].GetComponent<GridPiece>().IsDisabled = true;
        }
    }
}