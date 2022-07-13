using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{

    public TMPro.TextMeshProUGUI resultText;
    public Button restartButton;
    public Button backToMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        resultText.text = $"Congratulations!\nYou answered {SharedPrefs.correctAnswers} out of 5 questions correctly!";
        restartButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        backToMenuButton.onClick.AddListener(() => SceneManager.LoadScene("StartScreen"));
    }


}
