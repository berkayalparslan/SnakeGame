using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScore : MonoBehaviour
{
    private Text _scoreText;

    
    void Start()
    {
        _scoreText = GetComponent<Text>();    
    }


    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
