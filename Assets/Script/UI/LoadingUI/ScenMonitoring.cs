using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenMonitoring : MonoBehaviour
{
    

     public static int CurrentSceneindex=0;

   
    public static void ChangeScene(int index)
    {
        CurrentSceneindex = index;
        SceneManager.LoadScene ("Scenes/LoadingScene");    
    }
}
