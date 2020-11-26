using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfHubScene : MonoBehaviour
{
    public Button button;
    public string scene;

    public void deplaceProf()
    {
        SceneManager.LoadScene(scene);
    }

}
