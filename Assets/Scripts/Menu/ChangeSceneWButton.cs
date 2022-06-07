using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneWButton : MonoBehaviour
{
    public GameObject rawImageContainer;
    public int delay;
    private Image img;
    private int i;
    private bool startedIntro = true;
    private bool startedOutro = false;
    private string sceneName;

    void Start()
    {
        img = rawImageContainer.GetComponent<Image>();
    }

    void Update()
    {
        if (startedIntro)
        {
            img.color = new Color32(0,0,0, (byte)(Mathf.Round(255 - 255.0f/(delay*1.0f)*i)));
            i++;

            if (255 - 255.0f/(delay*1.0f)*i < 0)
            {
                i = 0;
                startedIntro = false;
            }
        }

        if (startedOutro && !startedIntro)
        {
            img.color = new Color32(0,0,0, (byte)(Mathf.Round(255.0f/(delay*1.0f)*i)));
            // Debug.Log("TEst");
            // Debug.Log((Mathf.Round(256.0f/(delay*1.0f)*i)));
            // Debug.Log(256/delay*i);
            // Debug.Log(i);
            i++;

            if (255.0f/(delay*1.0f)*i > 255)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    public void LoadScene(string localSceneName) 
    {
        if (!startedOutro)
        {
            startedOutro = true;
            sceneName = localSceneName;
        }
    }
}
