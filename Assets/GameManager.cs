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
    [SerializeField] private int lives = 3;
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
        lives = 3;
        checkpoint = startpoint.transform.position;
        player.transform.position = checkpoint;
        time = 299;

        CheckpointIDCheck();
    }

    // Update is called once per frame
    void Update()
    {
        StartTime();
        LiveText();
    }
    #endregion

    public void Death()
    {
        if (lives > 1)
        {
            lives--;
            player.transform.position = checkpoint;
            CheckpointIDCheck();
        }
        else
            SceneManager.LoadScene("SceneZachary");
    }

    public void LevelFinish()
    {
        Debug.Log("Nice Job");
    }

    #region UIFunction
    private void StartTime() //Source: https://www.youtube.com/watch?v=x-C95TuQtf0&ab_channel=N3KEN
    {
        float t = time - Time.timeSinceLevelLoad;

        string minute = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timer.text = "TIMER: " + minute + ":" + seconds;
    }

    private void LiveText()
    {
        live.text = "Lives: " + lives;
    }
    #endregion
    public void GetCheckpoint(GameObject pos) 
    {
        checkpoint = pos.transform.position;
        hazardManager[checkpointID].SetActive(false);
        checkpointID++;
        CheckpointIDCheck();
    }

    private void CheckpointIDCheck()
    {
        for(int i = checkpointID; i < hazardManager.Count; i++)
        {
            hazardManager[i].SetActive(false);
        }
        hazardManager[checkpointID].SetActive(true);
    }
}
