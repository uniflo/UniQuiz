using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class StartScreen : MonoBehaviour
{

    public Button startButton;
    public Button exitButton;
    
    private KeywordRecognizer startRecognizer;
    private KeywordRecognizer exitRecognizer;



    // Start is called before the first frame update
    void Start()
    {

        startRecognizer = new KeywordRecognizer(new string[] { "Start" });
        startRecognizer.OnPhraseRecognized += (_) => startGame();
        startRecognizer.Start();

        exitRecognizer = new KeywordRecognizer(new string[] { "Ende" });
        exitRecognizer.OnPhraseRecognized += (_) => exitGame();
        exitRecognizer.Start();

        exitButton.onClick.AddListener(exitGame);
        startButton.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void exitGame()
    {
        Application.Quit();
    }
}
