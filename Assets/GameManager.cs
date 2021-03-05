using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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
    [SerializeField] private int checkpointID;
    #endregion

    #region Variable UI
    [SerializeField] private Text timer;
    private float time;

    [SerializeField] private Text live;

    [SerializeField] private Text hp;

    [SerializeField] private GameObject panelEnd;
    #endregion

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
        player.transform.position = checkpoint;
        time = 599;

        data.hp = 5;
        data.lives = 5;

        panelEnd.SetActive(false);

        CheckpointIDCheck();
    }

    // Update is called once per frame
    void Update()
    {
        StartTime();
        Text();
        if (data.hp <= 0)
            Death();
    }
    #endregion

    public void Death() // si le player a 1 vie ou plus, il recommence au checkpoint, sinon il recommence du d�but
    {
        if (data.lives > 1)
        {
            data.lives--;
            data.hp = 5;
            player.transform.position = checkpoint;
            CheckpointIDCheck();
        }
        else
            SceneManager.LoadScene("SceneZachary");
    }

    public void LevelFinish()// active la fin du niveau
    {
        panelEnd.SetActive(true);
        Invoke("ByeBye", 2.0f);
    }

    public void ByeBye() // quit l'application
    {
        Application.Quit();
    }

    #region UIFunction
    private void StartTime() //cr�e le timer / Source: https://www.youtube.com/watch?v=x-C95TuQtf0&ab_channel=N3KEN
    {
        float t = time - Time.timeSinceLevelLoad;

        string minute = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timer.text = "TIMER: " + minute + ":" + seconds;
    }

    private void Text() // initi les textes
    {
        live.text = "Lives: " + data.lives;
        hp.text = "HP: " + data.hp;
    }
    #endregion
    public void GetCheckpoint(GameObject pos) // regarde quelle checkpoint il est rendue
    {
        checkpoint = pos.transform.position;
        hazardManager[checkpointID].SetActive(false);
        checkpointID++;
        CheckpointIDCheck();
    }

    private void CheckpointIDCheck() // V�rifie que seulement le checkpoint et ces hazard sois activer
    {
        for(int i = checkpointID; i < hazardManager.Count; i++)
        {
            hazardManager[i].SetActive(false);
        }
        hazardManager[checkpointID].SetActive(true);
    }
}
