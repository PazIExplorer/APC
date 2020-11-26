using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadSpecificScene : MonoBehaviour
{
    public Animator fadeSystem;
    public string groupe;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
        }
        if (collision.CompareTag("player"))
        {
            SceneManager.LoadScene(groupe);
        }
    }
    public IEnumerator loadNextScene()
    {
        PlayerPrefs.SetString("competence","Untagged");
        fadeSystem.SetTrigger("Fade");
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene(groupe);
    }
}
