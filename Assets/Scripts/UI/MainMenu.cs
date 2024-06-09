using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MusicOn;
    [SerializeField] private GameObject MusicOff;
    [SerializeField] private GameObject Menu;

    private void Start()
    {
        // If the music is not playing upon returning to the menu, make the text reflect that
        MusicManager musicManager = MusicManager.GetMusicManager();
        if (!musicManager.isPlaying())
        {
            ToggleMusicText();
        }
    }

    public void StartGame()
    {
        GameManager.GetGameManager().SetPlayerColour();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void ToggleMusic()
    {
        MusicManager.GetMusicManager().ToggleMusic();
    }

    public void ToggleMusicText()
    {
        if (MusicOn.gameObject.activeSelf)
        {
            MusicOn.gameObject.SetActive(false);
            MusicOff.gameObject.SetActive(true);
        }
        else
        {
            MusicOn.gameObject.SetActive(true);
            MusicOff.gameObject.SetActive(false);
        }

    }

    // ONLY CALLED WHEN OPENED FROM THE GAME SCENE
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        GameManager.GetGameManager().EnableBall();
        Destroy(Menu);
    }
}
