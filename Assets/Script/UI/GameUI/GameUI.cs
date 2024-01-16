

using UnityEngine;

using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
   [SerializeField] private Button ContinueBut;
   [SerializeField] private Button QuitBut;
   [SerializeField] private GameObject panel;

   private void Start()
   {
     
      ContinueBut.onClick.AddListener(() => { GameManager.Instance.ReStartGame(); });
      
      QuitBut.onClick.AddListener(() => { GameManager.Instance.GameOver(); });
      
      

      GameManager.Instance.UIEnable += UIenble;
      GameManager.Instance.UIDisable += UIdisable;
panel.SetActive(false);
   }

   public void UIenble()
   {
      panel.SetActive(true);
   }

   public void UIdisable()
   {
      panel.SetActive(false);
   }

    private void OnDestroy ()
    {
        ContinueBut.onClick.RemoveAllListeners ();
        QuitBut.onClick.RemoveAllListeners ();
       
    }
    

}
