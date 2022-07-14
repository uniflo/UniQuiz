using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class GameScreen : MonoBehaviour { 

    public Button answer1Button;
    public TMPro.TextMeshProUGUI answer1ButtonText;
    public Button answer2Button;
    public TMPro.TextMeshProUGUI answer2ButtonText;
    public Button answer3Button;
    public TMPro.TextMeshProUGUI answer3ButtonText;
    public Button answer4Button;
    public TMPro.TextMeshProUGUI answer4ButtonText;
    public TMPro.TextMeshProUGUI question;
    public Button selection;

    private string[] keywords = { "Kreis", "Dreieck", "Viereck", "Stern", "Klick" };

    private KeywordRecognizer recognizer;

    private List<Question> questions;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int? selectedAnswerIndex;

    // Start is called before the first frame update
    void Start()
    {
        SharedPrefs.correctAnswers = 0;

        recognizer = new KeywordRecognizer(keywords);
        recognizer.OnPhraseRecognized += onPhraseRecognized;
        recognizer.Start();

        questions = generateQuestions();
        showNextQuestion();

    }

    void hideSelection()
    {
        selectedAnswerIndex = null;
        selection.gameObject.SetActive(false);
    }

    void showSelection()
    {
        selection.gameObject.SetActive(true);
    }

    private void onPhraseRecognized(PhraseRecognizedEventArgs args) {
        Debug.Log($"recognized: {args.text} with confidence: {args.confidence}");

        switch (args.text)
        {
            case "Kreis":
                selection.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-172.9993f, 0.3676f, 0);
                selectedAnswerIndex = 0;
                break;
            case "Dreieck":
                selection.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(191f, 0.3676f, 0);
                selectedAnswerIndex = 1;
                break;
            case "Viereck":
                selection.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-173, -168, 0);
                selectedAnswerIndex = 2;
                break;
            case "Stern":
                selection.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(187.7f, -168.7f, 0);
                selectedAnswerIndex = 3;
                break;
            case "Klick":
                onSaidClick();
                return;
        }
        showSelection();
    }

    void onSaidClick()
    {
        if (selectedAnswerIndex == null) return;

        Question q = questions[currentQuestionIndex-1];
        answerCallback(selectedAnswerIndex == q.resultIndex);
    }

    void showNextQuestion() {
        hideSelection();


        if (currentQuestionIndex == questions.Count)
        {
            SharedPrefs.correctAnswers = correctAnswers;
            SceneManager.LoadScene("ResultScreen");
            return;
        }

        Question q = questions[currentQuestionIndex];
        question.text = q.question;
        answer1ButtonText.text = q.answers[0];
        answer2ButtonText.text = q.answers[1];
        answer3ButtonText.text = q.answers[2];
        answer4ButtonText.text = q.answers[3];

        answer1Button.onClick.RemoveAllListeners();
        answer2Button.onClick.RemoveAllListeners();
        answer3Button.onClick.RemoveAllListeners();
        answer4Button.onClick.RemoveAllListeners();

        answer1Button.onClick.AddListener(() => answerCallback(0 == q.resultIndex));
        answer2Button.onClick.AddListener(() => answerCallback(1 == q.resultIndex));
        answer3Button.onClick.AddListener(() => answerCallback(2 == q.resultIndex));
        answer4Button.onClick.AddListener(() => answerCallback(3 == q.resultIndex));

        currentQuestionIndex++;
    }

    List<Question> generateQuestions()
    {
        
        List<Question> questions = new List<Question>();

        for (int i = 0; i < 5; i++)
        {
            int a = Random.Range(100, 1000);
            int b = Random.Range(100, 1000);
            questions.Add(new Question(a, b));
        }

        return questions;

    }

    void answerCallback(bool answerCorrect)
    {
        if (answerCorrect)
        {
            correctAnswers++;
        }

        showNextQuestion();
    }

}


public class Question
{
    public int resultIndex;
    public string question;
    public List<string> answers;

    public Question(int a, int b)
    {
       
        question = $"What is {a} + {b} ?";
        answers = new List<string>{
            $"{a+b-75}",
            $"{a+b+113}",
            $"{a+b-121}",
        };

        resultIndex = Random.Range(0, 4);
        answers.Insert(resultIndex, $"{a + b}");
    }
}