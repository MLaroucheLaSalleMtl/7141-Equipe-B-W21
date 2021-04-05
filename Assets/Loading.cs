using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering;//Splashscreen
using UnityEngine.SceneManagement;//Loading

public class Loading : MonoBehaviour
{
    private AsyncOperation async;//toutes les valeur de mon chargement
    [SerializeField] private Image imageFilling;//barre de progression
    [SerializeField] private Text txtPercent;//txt de pourcentage
    [SerializeField] private Text txtAnyKey;//txt press any key to continue
    [SerializeField] private bool waitForUserInput = false;
    private bool anyKey = false;
    private bool ready = false;

    [SerializeField] private string loadSceneByName = "";
    [SerializeField] private int loadSceneByNum = -1;


    public void OnAnyKey(InputAction.CallbackContext context)
    {
        anyKey = context.performed;
    }

    private void Init()
    {
        Time.timeScale = 1f;
        Input.ResetInputAxes();
        System.GC.Collect();
 
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();//initialise la scene

        if(loadSceneByName != "")
        {
        async = SceneManager.LoadSceneAsync(loadSceneByName);//charge la scene avec le nom
        }
        else if (loadSceneByNum < 0)//si le nb est negatif, on passe a al scene suivante
        {
        Scene currentScene = SceneManager.GetActiveScene();
        async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }
        else
        {
        async = SceneManager.LoadSceneAsync(loadSceneByNum);
        }        

        async.allowSceneActivation = false;

        if (txtAnyKey)
        {
            txtAnyKey.enabled = false;
        }
        if (!waitForUserInput)
        {
            ready = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (async.progress >= 0.9f && SplashScreen.isFinished)
        {
            if (txtAnyKey) { txtAnyKey.enabled = true; }
            if (waitForUserInput && anyKey)
            {
                ready = true;               
            }
        }

        if (imageFilling)
        {
            imageFilling.fillAmount = async.progress + 0.1f;
        }
        if (txtPercent)
        {
            txtPercent.text = ((async.progress + 0.1f)*100).ToString("F2") + " %";
        }
        if(async.progress >= 0.9f && SplashScreen.isFinished && ready)
        {
            async.allowSceneActivation = true;
        }
    }
}
