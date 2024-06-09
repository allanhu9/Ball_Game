using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public void OpenMenu() {
       Instantiate(menu);
       GameManager.GetGameManager().DisableBall();
       Time.timeScale = 0;
    }
}
