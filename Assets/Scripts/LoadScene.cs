using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    

    public void loadSceneMatiereSelect()
    {
        Debug.Log("TEST");
        PlayerPrefs.SetString("competence", "Untagged");
        SceneManager.LoadScene("MatiereSelect");
    }
    
}
