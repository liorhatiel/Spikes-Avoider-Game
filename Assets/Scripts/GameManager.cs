using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    SoundManager soundManager;
    public int score = 0;


    [Header("Text Refrences:")]
    [SerializeField] Text startText;
    [SerializeField] Text scoreText;
    [SerializeField] Text gameOverScoreText;

    [Header("Canvas Refrences:")]
    [SerializeField] Canvas startCanvas;
    [SerializeField] Canvas scoreCanvas;
    [SerializeField] Canvas gameOverCanvas;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Start()
    {
        scoreText.text = "00";                       // At the start - the score is always 0.
        startCanvas.gameObject.SetActive(true);      // At the start - the start cavnas is always ON.
        scoreCanvas.gameObject.SetActive(false);     // At the start - the score is hidden. The score will be shown after the player start play.
        gameOverCanvas.gameObject.SetActive(false);  // At the start - the Game Over is hidden. The canvas will be shown after the player die.
    }


    // This function hapend after the player start the actual game. Tap on the screen.
    public void PlayerStartPlay()
    {
        startCanvas.gameObject.SetActive(false);     // Remove the start screen ("Press start to play").
        scoreCanvas.gameObject.SetActive(true);      // Add the score canvas. Start counting score.
    }


    // This funcion will add score to the player and write the score on the screen.
    public void AddScore()
    {
        score++;

        //                [Boolian]          if true              if false
        scoreText.text = score < 10 ? "0" + score.ToString() : score.ToString();

    }



    // *************************************************************** //
    // Those funcions are belong to the GAME OVER canvas.

    public void ShowGameOverCanvas()
    {
        gameOverScoreText.text = score.ToString();
        gameOverCanvas.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }





}
