using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeOrientation : MonoBehaviour
{
    //1 = portrait 0= landscape
    public int orientation;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = (orientation == 0 ? ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
