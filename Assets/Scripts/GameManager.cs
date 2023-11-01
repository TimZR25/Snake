using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _recordBoardUI;
    [SerializeField] private GameObject _pausePanel;
    public bool DisplayIsActive = true;
    private PlayerBehavior _playerBehavior;
    private int _score = 0;

    private void Awake()
    {
        Instance = this;
        _playerBehavior = FindObjectOfType<PlayerBehavior>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisplayPausePanel();
        }
    }
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    private void UpdateScore()
    {
        _scoreText.text = "Score: " + Score;
    }
    private void DisableScore()
    {
        _scoreText.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _playerBehavior.OnEaten += UpdateScore;
        _playerBehavior.OnDead += DisplayRecordBoard;
        _playerBehavior.OnDead += DisableScore;
    }
    private void OnDisable()
    {
        _playerBehavior.OnEaten -= UpdateScore;
        _playerBehavior.OnDead -= DisplayRecordBoard;
        _playerBehavior.OnDead -= DisableScore;
    }
    public void DisplayRecordBoard()
    {
        _recordBoardUI.SetActive(true);
    }
    public void DisplayPausePanel()
    {
        if (DisplayIsActive == true)
            return;

        AudioManager.Instance.PlaySound("KeyTap");
        _pausePanel.SetActive(true);
    }
}
