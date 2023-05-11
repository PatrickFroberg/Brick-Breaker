using Assets.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public int Bricks;
    public Brick BrickPrefab;
    public Rigidbody Ball;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public GameObject GameOverText;

    private Paddle _paddle;
    private int _maxLines;
    private int _lines;
    private int[] _pointCountArray;
    private int _points;
    private bool _isGameStarted = false;
    private bool _isGameOver = false;

    void Start()
    {
        _lines = 0;
        _maxLines = 8;
        _pointCountArray = new[] { 1, 1, 2, 2, 5, 5, 10, 10 };

        _paddle = GameObject.Find(nameof(Paddle)).GetComponent<Paddle>();

        SetLevel();
        GetHighScore();
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isGameStarted = true;
                AudioManager.Instance.PlayBounceSound();
                FireOffBall();
            }
        }
        else if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.Instance.PlayStartGameSound();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.Instance.PlayBtnSound();
                SceneManager.LoadScene(0);
            }
        }
    }

    private void FireOffBall()
    {
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
    }

    public void SetLevel()
    {
        Bricks = 0;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        IncreaseDifficulty();

        for (int i = 0; i < _lines; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = _pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);

                Bricks++;
            }
        }
    }

    private void IncreaseDifficulty()
    {
        if (_lines < _maxLines)
            _lines += 2;

        if (_isGameStarted)
        {
            Ball.GetComponent<Ball>().IncreaseMaxSpeed();
            _paddle.IncreaseMaxSpeed();
        }
    }
    private void GetHighScore()
    {
        HighScoreText.text = HighScoreManager.Instance.GetTopHighScore();
    }

    private void AddPoint(int point)
    {
        _points += point;
        ScoreText.text = $"Score : {_points}";

        Bricks--;
    }

    public void GameOver()
    {
        _isGameOver = true;
        HighScoreManager.Instance.SetHighScore(_points);
        GameOverText.SetActive(true);
    }
}
