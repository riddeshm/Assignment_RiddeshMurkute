using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    private const int ROWS = 10, COLS = 10;
    [SerializeField] Snake snake;
    [SerializeField] GameObject foodPrefab;
    Board board;
    Direction moveDirection = Direction.RIGHT;
    float interval = 0.5f;
    float nextTime = 0;
    CancellationTokenSource cts = new();

    GameObject food;
    // Start is called before the first frame update
    void Start()
    {
        board = new Board(ROWS, COLS);
        snake.Init(board.GetCell(0,0));
        food = Instantiate(foodPrefab);
        food.SetActive(false);
        StartCoroutine(SpawnFood());
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
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
        Cell cell;
        CellStatus cellStatus;

        switch (moveDirection)
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
        cell = board.GetCell(row, col);
        cellStatus = cell.Status;
        snake.Move(cell);
        if(cellStatus == CellStatus.Food)
        {
            food.SetActive(false);
            snake.Expand();
            StartCoroutine(SpawnFood());
        }
    }

    private void CheckInput()
    {
        if (Input.GetAxis("Horizontal") > 0 && moveDirection != Direction.LEFT)
        {
            moveDirection = Direction.RIGHT;
        }
        else if (Input.GetAxis("Horizontal") < 0 && moveDirection != Direction.RIGHT)
        {
            moveDirection = Direction.LEFT;
        }
        else if (Input.GetAxis("Vertical") > 0 && moveDirection != Direction.DOWN)
        {
            moveDirection = Direction.UP;
        }
        else if (Input.GetAxis("Vertical") < 0 && moveDirection != Direction.UP)
        {
            moveDirection = Direction.DOWN;
        }
    }

    private IEnumerator SpawnFood()
    {
        int _row = 0;
        int _column = 0;
        Cell cell;
        while (true)
        {
            _row = Random.Range(0, ROWS);
            _column = Random.Range(0, COLS);
            cell = board.GetCell(_row, _column);
            if (cell.Status == CellStatus.None)
            {
                break;
            }
            yield return null;
        }
        cell.Status = CellStatus.Food;
        food.transform.position = cell.Pos;
        food.SetActive(true);
    }
}
