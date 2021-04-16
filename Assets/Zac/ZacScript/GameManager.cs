using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour  // majorité Zachary 
{
    public static GameManager instance = null;
    #region Variable Player
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerData data;
    #endregion

    #region Variable Level
    [SerializeField] private GameObject startpoint;
    [SerializeField] private GameObject end;
    public Vector2 checkpoint;
    [SerializeField] private List<GameObject> hazardManager;
    private int checkpointID;

    [SerializeField] private BearBossBehavior boss;
    private bool thereBoss;
    #endregion

    #region Variable UI
    [SerializeField] private Text timer;
    private float time;

    [SerializeField] private Text live;

    [SerializeField] private Text score;

    [SerializeField] private Image hp;

    [SerializeField] private Text snowBallText;

    [SerializeField] private GameObject panelEnd;

    [SerializeField] private GameObject panelPlayerUI;

    [SerializeField] private GameObject panelPause;

    [SerializeField] private GameObject panelOption;

    [SerializeField] private GameObject panelGameOver;

    [SerializeField] private Text highScore;
    
    [SerializeField] private GameObject selectMenu;
    
    [SerializeField] private Selectable defaultBtn;

    [SerializeField] private Selectable winBtn;

    [SerializeField] private Selectable gameOverBtn;
    
    #endregion

    public GameObject Player { get => player; set => player = value; }

    #region Awake, Start, Update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {        
        checkpointID = 0;
        checkpoint = startpoint.transform.position;
        Player.transform.position = checkpoint;

        time = 599;

        data.hp = 5;
        
        panelEnd.SetActive(false);
        panelPlayerUI.SetActive(true);
        panelPause.SetActive(false);
        panelOption.SetActive(false);
        panelGameOver.SetActive(false);
        highScore.gameObject.SetActive(false);
        CheckpointIDCheck();

        if (boss)
        {
            end.SetActive(false);
            thereBoss = true;
        }
        else
        {
            end.SetActive(true);
            thereBoss = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        StartTime();
        Text();
        if (data.hp <= 0)
        {
            Death();            
        }

        if (!boss && thereBoss)
            end.SetActive(true);
    }
    #endregion

    public void Death() // si le player a 1 vie ou plus, il recommence au checkpoint, sinon il recommence du début
    {
        if (data.lives > 1)
        {
            data.lives--;
            data.hp = 5;
            Player.transform.position = checkpoint;
            CheckpointIDCheck();
            boss.currentHealth = boss.maxHealth;
        }
        else
        {                  
            highScore.gameObject.SetActive(true);
            panelGameOver.SetActive(true);
            gameOverBtn.Select();
            GetHighScore();
            Time.timeScale = 0f;
            data.score = 0;
            data.lives = data.maxHp+1;
        }
    }

    public void LevelFinish()// active la fin du niveau
    {
        panelEnd.SetActive(true);
        winBtn.Select();
        highScore.gameObject.SetActive(true);
        GetHighScore();
        Time.timeScale = 0f;
    }

    public void ByeBye() // quit l'application
    {
        Application.Quit();
    }

    #region UIFunction 
    private void StartTime() //crée le timer / Source: https://www.youtube.com/watch?v=x-C95TuQtf0&ab_channel=N3KEN 
    {
        float t = time - Time.timeSinceLevelLoad;

        string minute = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("00");

        timer.text = "X " + minute + ":" + seconds;
    }

    private void Text() // initi les textes
    {
        live.text = "X " + data.lives;
        
        snowBallText.text = "X " + data.snowBallCount;

        hp.rectTransform.sizeDelta = new Vector2(data.hp*(288f / 5), 32f);

        score.text = data.score.ToString("00000000");

    }
    #endregion
    public void GetCheckpoint(GameObject pos) // regarde quelle checkpoint il est rendue
    {
        checkpoint = pos.transform.position;
        hazardManager[checkpointID].SetActive(false);
        checkpointID++;
        CheckpointIDCheck();
    }

    private void CheckpointIDCheck() // Vérifie que seulement le checkpoint et ces hazard sois activer
    {
        {
            for (int i = checkpointID; i < hazardManager.Count; i++)
            {
                hazardManager[i].SetActive(false);
            }
            hazardManager[checkpointID].SetActive(true);
        }
    }


    public void OnPause(InputAction.CallbackContext context) // Simon
    {
        if (context.started && !panelEnd.activeSelf && !panelGameOver.activeSelf)
        {
            if (!panelPause.activeSelf && !selectMenu.activeSelf)
            {
                selectMenu.SetActive(true);
                panelPause.SetActive(true);
                defaultBtn.Select();
                Time.timeScale = 0f;
            }
            else
            {
                //Resume();
            }
        }
        
    }

    public void Resume() // Simon
    {
        selectMenu.SetActive(false);
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    private void GetHighScore() //change le highscore si les points son plus grand, sinon garde le highscore
    {
        int staticHighScore = PlayerPrefs.GetInt("HighScore");
        Debug.Log(staticHighScore);
        Debug.Log(data.score);

        if (data.score > staticHighScore)
        {
            PlayerPrefs.SetInt("HighScore", data.score);
            highScore.text = "New Highscore: " + PlayerPrefs.GetInt("HighScore");
        }

        else if (data.score <= staticHighScore)
        {
            highScore.text = "Highscore: " + staticHighScore;
        }
        

    }

}
