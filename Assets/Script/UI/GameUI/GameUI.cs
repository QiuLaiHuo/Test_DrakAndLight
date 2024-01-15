
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
   [SerializeField] private Button ContinueBut;
   [SerializeField] private Button QuitBut;
   [SerializeField] private GameObject panel;

   private void Start()
   {
      panel.SetActive(false);
      ContinueBut.onClick.AddListener(() => { GameManager.Instance.ReStartGame(); });
      
      QuitBut.onClick.AddListener(() => { GameManager.Instance.GameOver(); });
     

   }

   public void PlayerDie()
   {
      panel.SetActive(true);
   }

   public void PlayerSurvival()
   {
      panel.SetActive(false);
   }
   
   
   
}
