using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoretxt;
    public int score;
    private void Update()
    {
        score = 0;
    }

    public void scoreUp(int valueIn) 
    {
        score += valueIn;
        scoretxt.text = score.ToString();
    }
}
