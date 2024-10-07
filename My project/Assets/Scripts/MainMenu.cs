using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // start; begin game (at level one)
    public void StartOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("LevelOne");
    }

    // start; begin game (at level two)
    public void StartTwo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("LevelTwo");
    }

    // quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    // send player to level select screen,
    // so they can choose between the levels (even if there's only 2)
    public void LevelSelect()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MMLevelSelect");
    }

    // allows the player to return to the main screen
    public void BackMM()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MainMenu");
    }
}
