using UnityEngine.UI;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Button button;
    public void changeColor()
    {
        Debug.Log("Changing highlighed color");
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        ColorBlock colorVar = button.colors;
        colorVar.highlightedColor = new Color(r, g, b);
        button.colors = colorVar;
    }
}
