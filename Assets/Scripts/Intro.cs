using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Intro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI topScoreText;
    // Start is called before the first frame update
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        topScoreText.text = highScore.ToString();
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
