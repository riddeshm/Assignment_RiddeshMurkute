using UnityEngine;

public enum CellStatus
{
    None,
    Snake,
    Food
}

public class Cell
{
    private int row, col;
    private Vector3 pos;
    private CellStatus status;

    public Cell(int _row, int _col, Vector3 _pos)
    {
        row = _row;
        col = _col;
        pos = _pos;
    }

    public int Row { get { return row; } }
    public int Col { get { return col; } }
    public Vector3 Pos { get { return pos; } }
    public CellStatus Status { 
        get { return status; }
        set { status = value; }
    }
}
