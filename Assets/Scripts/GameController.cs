using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CubeSpawner cubeSpawner;
    public Text fpsCounter;

    void Start()
    {
        InvokeRepeating("FPSCounter", 0f, 1f);
    }

    public void StartTrack()
    {
        cubeSpawner.StartTrack();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

    void FPSCounter()
    {
        fpsCounter.text = "FPS: " + ((int)(1f / Time.unscaledDeltaTime));
    }
}
