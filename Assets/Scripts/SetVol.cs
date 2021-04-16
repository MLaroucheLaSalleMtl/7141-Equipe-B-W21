//Script venant de GameEngine1, cours sur les menu et UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class SetVol : MonoBehaviour
{
    [Header("Variables pour AudioMixer")]
    [SerializeField] private AudioMixer audioM = null;
    [SerializeField] private string nameParam = null;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParam, 0);//0 = +-0db
        slider.value = v;//met le visuel sur la bonne valeur
        audioM.SetFloat(nameParam, v);        
    }

    public void SetVolume (float vol)
    {
        audioM.SetFloat(nameParam, vol);
        slider.value = vol;
        PlayerPrefs.SetFloat(nameParam, vol);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
