using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoretxt;
    public int score;
    public float multiplyValue;

    public GameObject restartPanel;
    public GameObject startPanel;
    public GameObject taptap;

    private void Awake()
    {
        startPanel.SetActive(true);
        restartPanel.SetActive(false);
    }

    public void scoreUp(int valueIn) 
    {
        score += valueIn;
        scoretxt.text = score.ToString();
    }

    public void Multiply(float valueEnd) 
    {
        if (valueEnd <= multiplyValue)
            return;
        multiplyValue = valueEnd;
        scoretxt.text = (score * multiplyValue).ToString();
    }

    public void restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<PlayerControler>().playing = false;
    }
}
