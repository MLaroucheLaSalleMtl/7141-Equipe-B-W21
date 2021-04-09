using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Dropdown))]
public class SetResolution : MonoBehaviour
{
    private Dropdown dropdown;
    private Resolution[] resolutions;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        resolutions = Screen.resolutions;
        List<string> dropOptions = new List<string>();
        int position = 0;
        int i = 0;
        Resolution currentRes = Screen.currentResolution;
        foreach(Resolution res in resolutions)
        {
            string v = res.ToString();
            dropOptions.Add(v);
            if(res.width == currentRes.width 
            && res.height == currentRes.height 
            && res.refreshRate == currentRes.refreshRate)
            {
                position = i;
            }
            i++;
        }
        dropdown.AddOptions(dropOptions);
        dropdown.value = position;

    }

    public void SetRes()
    {
        Resolution res = resolutions[dropdown.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen, res.refreshRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
