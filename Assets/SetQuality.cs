using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    [RequireComponent(typeof(Dropdown))]
public class SetQuality : MonoBehaviour
{
    private Dropdown dropdown; //boite de selection
    private string[] qualities;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();//cache le dropdown
        qualities = QualitySettings.names;
        List<string> dropOptions = new List<string>();
        foreach(string s in qualities)
        {
            dropOptions.Add(s);//Ajoute le string a la list
        }
        dropdown.AddOptions(dropOptions);
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetGFX()
    {
        QualitySettings.SetQualityLevel(dropdown.value, true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
