using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private Text _scoresCount;
    private Text _winOrFailText;

    private const int _pointsForEnemy = 100;
    private const int _pointsForIntelEnemy = 200;
    private int _totalScore = 0;

    public void Start()
    {
        _scoresCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameInitializer>().scores.GetComponentInChildren<Text>();
        _winOrFailText = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameInitializer>().winOrfail.GetComponentInChildren<Text>();
    }


    private void OutputScoresCount()
    {
        _scoresCount.text = "Score Count: " + _totalScore.ToString();
        OutputWinText();
    }
    public void UpdateScoreForEnemy(Characters enemyType)
    {
        if (enemyType == Characters.Enemy)
            _totalScore += _pointsForEnemy;
        else if (enemyType == Characters.IntelligentEnemy)
            _totalScore += _pointsForIntelEnemy;
        OutputScoresCount();
    }

    public void OutputFailText()
    {
        _winOrFailText.color = Color.red;
        _winOrFailText.text = "You Loose!";
    }
    public void OutputWinText()
    {
        if (_totalScore == GetAimToWin())
        {
            _winOrFailText.color = Color.green;
            _winOrFailText.text = "You Win!";
        }
    }
    private int GetAimToWin()
    {
        var enemies = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameInitializer>().enemyCount;
        var intelEnemies = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameInitializer>().intelligentEnemyCount;

        return enemies * _pointsForEnemy + intelEnemies * _pointsForIntelEnemy;
    }

}
