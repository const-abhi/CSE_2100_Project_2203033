using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class logicScript : MonoBehaviour
{
    
    public int playerScore = 0;
    public Text scoreText;

    public GameObject gameOverScreen;
 public bird_script bird; 

[ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd){
    if (bird != null && bird.birdIsAlive) { // Only add score if bird is alive
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }
}

public void gameOver(){
        gameOverScreen.SetActive(true);
    }
    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
