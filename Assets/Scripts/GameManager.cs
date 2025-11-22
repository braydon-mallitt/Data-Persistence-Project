using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    public int highScorePoints;
    public string highScore;
    
    public bool m_GameOver = false;

    public Button restartButton;
    public Button mainMenuButton;
    public GameObject pauseScreen;
    public bool gamePaused = false;
    public Slider volumeSlider;
    public TextMeshProUGUI highScoreText;
    public TMP_InputField nameInput;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        LoadScore();
        nameInput.onSubmit.AddListener(OnNameSubmitted);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        //else if (m_GameOver)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //    }
        //}
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    private void LateUpdate()
    {
        //if the current points are greater than the saved high score points, update the high score
        
        highScoreText.text = "High Score: " + highScore;
        if (m_Points > highScorePoints)
        {
            highScorePoints = m_Points;
        }
    }

    private void OnNameSubmitted(string nameInput)
    {
        highScore = highScorePoints + " - " + nameInput;
        highScoreText.text = "High Score: " + highScore;
        SaveScore();
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        nameInput.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        //highScore = highScorePoints + " - " + nameInput.text;
        //highScoreText.text = "High Score: " + highScore;
        //SaveScore();
    }

    public void MainMenuNew()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        //Get the name of the current loaded scene and then load that scene (reload current scene)
        gamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        volumeSlider.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        Time.timeScale = 0f; //Stop time so objects are not affected by physics 
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseScreen.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        volumeSlider.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        Time.timeScale = 1f; //Resume time so objects are affected by physics 
        gamePaused = false;
    }

    [System.Serializable]
    class SaveData
    {
        public int highScorePoints;
        public string highScore;

    }
    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScorePoints = highScorePoints;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScorePoints = data.highScorePoints;
        }
    }
}
