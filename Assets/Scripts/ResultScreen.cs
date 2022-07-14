using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class ResultScreen : MonoBehaviour
{

    public TMPro.TextMeshProUGUI resultText;
    public Button restartButton;
    public Button backToMenuButton;

    private KeywordRecognizer againRecognizer;
    private KeywordRecognizer backRecognizer;

    // Start is called before the first frame update
    void Start()
    {

        againRecognizer = new KeywordRecognizer(new string[] { "Nochmal" });
        againRecognizer.OnPhraseRecognized += (_) => SceneManager.LoadScene("GameScene");
        againRecognizer.Start();

        backRecognizer = new KeywordRecognizer(new string[] { "Menü" });
        backRecognizer.OnPhraseRecognized += (_) => SceneManager.LoadScene("StartScreen");
        backRecognizer.Start();

        resultText.text = $"Congratulations!\nYou answered {SharedPrefs.correctAnswers} out of 5 questions correctly!";
        restartButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        backToMenuButton.onClick.AddListener(() => SceneManager.LoadScene("StartScreen"));
    }


}
