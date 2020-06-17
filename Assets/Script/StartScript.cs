using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public GameObject startMenu;
    Button startButton;
    Button exitButton;
    Button resetScoreButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton = startMenu.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        exitButton = startMenu.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>();
        resetScoreButton = startMenu.transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        resetScoreButton.onClick.AddListener(ResetScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartGame()
    {
        SceneManager.LoadScene("MyStage");
    }
    void ExitGame()
    {
        Application.Quit();
    }
    void ResetScore()
    {
        PlayerPrefs.SetInt("BestScore", 0);
    }
}
