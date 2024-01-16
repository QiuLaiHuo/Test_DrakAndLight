using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    public string[] hints;
  private int sceneint;

    private void Start ()
    {
        sceneint = ScenMonitoring.CurrentSceneindex;
        Random.InitState ((int) System.DateTime.Now.Ticks);
        int index = hints.Length-1;
        if(index>0)
            hintText.text = hints[Random.Range (0,index)];
       StartCoroutine (LoadingScenes (sceneint));
    }


    IEnumerator LoadingScenes(int index)
    {
        yield return new WaitForSecondsRealtime (1.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync (index);
        async.allowSceneActivation = false;
        while(async.progress<0.9f)
        {
            yield return new WaitForSecondsRealtime (1.5f);
        }
        async.allowSceneActivation = true;

    }

}
