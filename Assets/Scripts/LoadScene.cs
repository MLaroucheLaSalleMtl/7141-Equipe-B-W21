using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation async;//Contenue du chargement
    // Start is called before the first frame update

    public void BtnLoadScene()//pas de parametre = charge la scene suivante
    {
        if (async != null) return;//Arrete l<executiion si async nes pas null

        Scene currScene = SceneManager.GetActiveScene();
        async = SceneManager.LoadSceneAsync(currScene.buildIndex + 1);//charge la scene suivante
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
