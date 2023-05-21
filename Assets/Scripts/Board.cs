using UnityEngine;

public class Board
{
    private int rows, cols;
    private Cell[,] cells;
    

    public Board(int _rows, int _cols)
    {
        rows = _rows;
        cols = _cols;

        cells = new Cell[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                cells[i,j] = new Cell(i, j, new Vector3(i, 0, j));
            }
        }
    }

    public Cell GetCell(int row, int col)
    {
        return cells[row, col];
    }
}
