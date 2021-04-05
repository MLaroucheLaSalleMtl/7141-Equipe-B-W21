using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation async;//Contenue du chargement


    public void BtnLoadScene()//pas de parametre = charge la scene suivante
    {
        if (async != null) return;//Arrete l'executiion si async nes pas null

        Scene currScene = SceneManager.GetActiveScene();
        async = SceneManager.LoadSceneAsync(currScene.buildIndex + 1);//charge la scene suivante
    }
    public void BtnLoadScene(int i)//i = numero de scene
    {
        if (async != null) return;//Arrete l'executiion si async nes pas null
     
        async = SceneManager.LoadSceneAsync(i);//charge la scene au numero i
    }
    public void BtnLoadScene(string s)//s = nom de la scene
    {
        if (async != null) return;//Arrete l'executiion si async nes pas null
                
        async = SceneManager.LoadSceneAsync(s);//charge la scene au nom de s
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
