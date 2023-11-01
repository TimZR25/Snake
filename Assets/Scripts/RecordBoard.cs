using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private string _bestScoreKey = "BestScore";
    private int _score;
    private void Start()
    {
        _score = GameManager.Instance.Score;
        _scoreText.text = "You score: " + _score;

        int bestScore = PlayerPrefs.GetInt(_bestScoreKey);

        if (bestScore < _score)
        {
            PlayerPrefs.SetInt(_bestScoreKey, _score);
        }

        bestScore = PlayerPrefs.GetInt(_bestScoreKey);
        _bestScoreText.text = "Best score: " + bestScore;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void OnEnable()
    {
        GameManager.Instance.DisplayIsActive = true;
    }
    private void OnDisable()
    {
        AudioManager.Instance.PlaySound("KeyTap");
        GameManager.Instance.DisplayIsActive = false;
    }
}
