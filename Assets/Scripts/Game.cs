using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

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
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] TextMeshProUGUI scoreText;
    Board board;
    Direction moveDirection = Direction.RIGHT;
    float interval = 0.5f;
    float nextTime = 0;
    CancellationTokenSource cts = new();
    Food[] foodItems;
    Food currentFood;
    Food previousFood;
    int streak = 1;
    int score = 0;
    private bool gameOver = false;

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
        if(gameOver)
        {
            return;
        }
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
        if(row >= ROWS || row < 0 || col >= COLS || col < 0)
        {
            GameOver();
            return;
        }
        cell = board.GetCell(row, col);
        cellStatus = cell.Status;
        
        if(cellStatus == CellStatus.Food)
        {
            currentFood.foodGameObj.SetActive(false);
            if(previousFood != null)
            {
                if(previousFood == currentFood)
                {
                    streak++;
                }
                else
                {
                    streak = 1;
                }
            }
            score += (currentFood.Points*streak);
            previousFood = currentFood;
            snake.Expand();
            StartCoroutine(SpawnFood());
            scoreText.text = score.ToString();
        }
        else if(cellStatus == CellStatus.Snake)
        {
            GameOver();
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

    private void GameOver()
    {
        gameOver = true;
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
        gameOverPopup.SetActive(true);
    }

    public void Restart()
    {

    }
}
