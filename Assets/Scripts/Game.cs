using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    UP,
    LEFT,
    DOWN,
    RIGHT
}

public class Game : MonoBehaviour
{
    [SerializeField] Snake snake;
    Board board;
    Direction moveDirection = Direction.RIGHT;
    float interval = 0.5f;
    float nextTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        board = new Board(10, 10);
        snake.Init(board.GetCell(0,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {

            //do something here every interval seconds

            nextTime += interval;
            Tick();

        }
    }

    private void Tick()
    {
        int row = snake.Head.Row;
        int col = snake.Head.Col;

        switch(moveDirection)
        {
            case Direction.UP:
                col++;
                break;
            case Direction.LEFT:
                row--;
                break;
            case Direction.DOWN:
                col--;
                break;
            case Direction.RIGHT:
                row++;
                break;
        }
        snake.Move(board.GetCell(row,col));
    }
}
