using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    public Text comboCounterText;

    private int comboCounter;

    // Start is called before the first frame update
    void Start()
    {
        RestartScore();
    }

    public void GoodCut()
    {
        ++comboCounter;
        comboCounterText.text = comboCounter.ToString();
    }

    public void BadCut()
    {
        RestartScore();
    }

    public void RestartScore()
    {
        comboCounterText.text = "0";
        comboCounter = 0;
    }
}
