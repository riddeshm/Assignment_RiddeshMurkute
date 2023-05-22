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
    [SerializeField] FoodConfig foodConfig;
    Board board;
    Direction moveDirection = Direction.RIGHT;
    float interval = 0.5f;
    float nextTime = 0;
    CancellationTokenSource cts = new();
    Food[] foodItems;
    Food currentFood;

    // Start is called before the first frame update
    void Start()
    {
        board = new Board(ROWS, COLS);
        snake.Init(board.GetCell(0,0));
        InitializeFoodObjs();
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
        Debug.Log(row);
        if(row >= ROWS || row < 0 || col >= COLS || col < 0)
        {
            Debug.Log("GameOver");
            return;
        }
        cell = board.GetCell(row, col);
        cellStatus = cell.Status;
        
        if(cellStatus == CellStatus.Food)
        {
            currentFood.foodGameObj.SetActive(false);
            snake.Expand();
            StartCoroutine(SpawnFood());
        }
        else if(cellStatus == CellStatus.Snake)
        {
            Debug.Log("GameOver");
            return;
        }
        snake.Move(cell);
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
        currentFood = foodItems[Random.Range(0, foodItems.Length)];
        currentFood.foodGameObj.transform.position = cell.Pos;
        currentFood.foodGameObj.SetActive(true);
    }

    private void InitializeFoodObjs()
    {
        foodItems = foodConfig.FoodContainer.FoodItems;
        for (int i = 0; i < foodItems.Length; i++)
        {
            GameObject foodObj = Resources.Load<GameObject>(foodItems[i].Color);
            foodItems[i].foodGameObj = Instantiate(foodObj);
            foodItems[i].foodGameObj.SetActive(false);
        }
    }
}
