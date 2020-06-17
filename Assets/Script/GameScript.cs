using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameScript : MonoBehaviour
{
    public GameObject character;
    public GameObject script;
    public GameObject retryButton;
    public GameObject StartMenu;
    public Camera myCamera;
    public GameObject myAudioALL;
    public GameObject destroyedEnemyALL;
    public GameObject scoreCanvas;
    float calculatedScore = 0;
    Text score;
    Text bestScore;
    AudioSource myAudio;
    AudioSource destroyedEnemy;
    Button button;
    bool mySwitch = false;
    bool isGamePaused = false;
    public GameObject  characterDestroyedParticles;
    GameObject instParticles;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CameraZoom");
        myAudio = myAudioALL.GetComponent<AudioSource>();
        destroyedEnemy = destroyedEnemyALL.GetComponent<AudioSource>();
        score = scoreCanvas.transform.GetChild(1).GetComponent<Text>();
        bestScore = scoreCanvas.transform.GetChild(3).GetComponent<Text>();
        bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Increase the score as time goes by (you can change the increment to make the score go up faster—if increased—or slower—if decreased)
        if (!script.GetComponent<EnemyScript>().hasGameEnded && !isGamePaused)
        {
            calculatedScore += 0.1f;
            calculatedScore = Mathf.Round(calculatedScore * 10) / 10;
            if (calculatedScore % 1 == 0) score.text = calculatedScore.ToString();
        }

        //If you lose, pop the retry button AND evaluates whether you beat the best score AND destroys the character AND triggers the particle effect
        if (script.GetComponent<EnemyScript>().hasGameEnded && !mySwitch)
        {
            if(int.Parse(score.text) > int.Parse(bestScore.text))
            {
                PlayerPrefs.SetInt("BestScore", int.Parse(score.text));
            }
            retryButton = Instantiate(retryButton, new Vector3(0, 0, 0), Quaternion.identity);
            button = retryButton.transform.GetChild(0).GetComponent<Button>();
            button.onClick.AddListener(Restart);
            mySwitch = true;
            myAudio.mute = true;
            instParticles = Instantiate(characterDestroyedParticles, character.transform.position, Quaternion.identity);
            instParticles.GetComponent<ParticleSystem>().Play();
            destroyedEnemy.Play();
            Destroy(character);
        }

        //Pause Game and display menu if you press pause
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!StartMenu.activeSelf)
            {
                isGamePaused = true;
                StartMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                isGamePaused = false;
                StartMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            
        }
    }
    //Function that restart game if restart is pressed
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Function that resumes the game if resume is pressed
    public void Resume()
    {
        StartMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    //Function that quits the game if quit is pressed
    public void Quit()
    {
        Application.Quit();
    }
    //Gradually zoom into the scene when the game begins
    IEnumerator CameraZoom()
    {
        while (myCamera.orthographicSize > 5f)
        {
            if (!isGamePaused)
            {
                myCamera.orthographicSize -= 0.05f;
            }
            yield return null;

        }
    }
}
