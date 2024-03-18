using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.InputSystem;

public class GameManagerUI : MonoBehaviour
{
    private float timeRemaining = 30f; 
    private bool timerIsRunning = false;
    private int[] prizeAmounts = { 0, 500, 1000, 2500, 5000, 7500, 10000, 25000, 50000, 75000, 100000, 500000 };

    private List<Question> easyQuestions = new List<Question>()
{
    new Question("Jaki kolor ma niebo w dzie�?", new string[]{"Czerwony", "Niebieski", "Zielony", "��ty"}, 1),
    new Question("Ile wynosi 2 + 2?", new string[]{"3", "4", "5", "6"}, 1),
    new Question("Kt�ra z tych figur geometrycznych ma trzy boki?", new string[]{"Kwadrat", "Tr�jk�t", "Ko�o", "Prostok�t"}, 1),
    new Question("Kto jest autorem powie�ci 'Harry Potter'?", new string[]{"J.R.R. Tolkien", "George R.R. Martin", "J.K. Rowling", "Stephenie Meyer"}, 2),
    new Question("Kt�ry z tych sport�w wykorzystuje rakiet� i pi�k�?", new string[]{"Pi�ka no�na", "Koszyk�wka", "Tenis", "Hokej"}, 2),
    new Question("Kt�ry miesi�c jest trzecim miesi�cem w roku?", new string[]{"Stycze�", "Marzec", "Maj", "Lipiec"}, 1),
    new Question("Ile jest godzin w dobie?", new string[]{"12", "24", "36", "48"}, 1),
    new Question("Kt�re z tych zwierz�t jest ssakiem?", new string[]{"Paj�k", "Rekin", "Ko�", "��w"}, 2),
    new Question("Kt�ry z tych kraj�w nie le�y w Europie?", new string[]{"Francja", "Niemcy", "Argentyna", "W�ochy"}, 2),
    new Question("Kt�ra z tych planet jest najbli�ej S�o�ca?", new string[]{"Mars", "Jowisz", "Merkury", "Saturn"}, 2)
};

    private List<Question> mediumQuestions = new List<Question>()
{
    new Question("Kt�ra z tych rzek jest najd�u�sza?", new string[]{"Nil", "Amazonka", "Missisipi", "Jangcy"}, 1),
    new Question("Kto jest autorem obrazu 'Mona Lisa'?", new string[]{"Pablo Picasso", "Vincent van Gogh", "Leonardo da Vinci", "Michelangelo"}, 2),
    new Question("Kt�ry kontynent jest najmniej zaludniony?", new string[]{"Afryka", "Azja", "Australia", "Europa"}, 2),
    new Question("Ile kontynent�w jest na Ziemi?", new string[]{"4", "6", "7", "8"}, 2),
    new Question("Kt�re pa�stwo posiada najwi�cej czasu strefowego?", new string[]{"Rosja", "Stany Zjednoczone", "Chiny", "Australia"}, 0),
    new Question("Jak nazywa si� najwy�szy szczyt w Afryce?", new string[]{"Everest", "Mont Blanc", "Mount Kenya", "Kilimand�aro"}, 3),
    new Question("Kt�ry z tych papie�y by� najd�u�ej na tronie papieskim?", new string[]{"Jan Pawe� II", "Pius IX", "Pius XII", "Benedykt XVI"}, 0),
    new Question("Kt�ry pierwiastek chemiczny jest najci�szy?", new string[]{"Uran", "O��w", "Tor", "Rdze�"}, 0),
    new Question("Kt�ra z tych g�rskich rzek ma najwi�kszy przep�yw?", new string[]{"Amazonka", "Nil", "Missisipi", "Jangcy"}, 0),
    new Question("W kt�rym roku mia�a miejsce bitwa pod Grunwaldem?", new string[]{"1309", "1410", "1525", "1772"}, 1)
};

    private List<Question> hardQuestions = new List<Question>()
{
    new Question("Ile lat trwa�a wojna stuletnia?", new string[]{"75 lat", "90 lat", "100 lat", "116 lat"}, 2),
    new Question("Kt�ry z tych astronom�w odkry� planetoid�?", new string[]{"Nicolaus Copernicus", "Galileo Galilei", "Tycho Brahe", "Clyde Tombaugh"}, 3),
    new Question("Kt�ry z tych j�zyk�w programowania by� pierwszy?", new string[]{"Python", "Java", "C++", "Fortran"}, 3),
    new Question("Kt�re z tych pa�stw jest najwi�kszym producentem ropy naftowej?", new string[]{"Rosja", "Arabia Saudyjska", "Stany Zjednoczone", "Kanada"}, 1),
    new Question("Kt�ry z tych kr�l�w panowa� najd�u�ej w historii Wielkiej Brytanii?", new string[]{"Kr�l Henryk VIII", "Kr�l Jerzy III", "Kr�l Edward VII", "Kr�lowa El�bieta II"}, 3),
    new Question("Kt�ry z tych wynalazk�w by� opatentowany jako pierwszy?", new string[]{"Telefon", "�ar�wka", "Telegraf", "Samoch�d"}, 2),
    new Question("Kt�ry z tych kompozytor�w nie by� Austriakiem?", new string[]{"Wolfgang Amadeusz Mozart", "Joseph Haydn", "Ludwig van Beethoven", "Johann Sebastian Bach"}, 3),
    new Question("Kt�ry z tych dyktator�w by� uwa�any za najbardziej sanguinarycznego?", new string[]{"Joseph Stalin", "Mao Zedong", "Adolf Hitler", "Pol Pot"}, 0),
    new Question("Kt�ra z tych bitew by�a najwi�ksz� bitw� morsk� w historii?", new string[]{"Bitwa pod Salamis", "Bitwa pod Lepanto", "Bitwa o Midway", "Bitwa jutlandzka"}, 3),
    new Question("Kt�ry z tych wynalazk�w nie zosta� stworzony w staro�ytnym Egipcie?", new string[]{"Ko�o", "Pismo hieroglificzne", "S�ownik", "Kalibrator"}, 3)
};


    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public GameObject gameOverPanel;
    public TextMeshProUGUI timerText;
    public GameObject prizeContent; 
    private Image[] prizes;
    public Button fiftyFiftyButton; 
    public Button callAFriendButton; 
    public GameObject resignPanel; 
    public TextMeshProUGUI resignText;
    public Button resign;
    public Button restart;
    public GameObject winPanel;

    private bool fiftyFiftyUsed = false; 
    private bool callAFriendUsed = false;
    private Question currentQuestion;
    private int currentQuestionIndex = 0;
    private bool gameOver = false;
    private List<int> correctAnswerIndexes = new List<int>();
    

    void Start()
    {
        gameOverPanel.SetActive(false);
        resignPanel.SetActive(false);
        winPanel.SetActive(false);
        prizeContent = GameObject.Find("PrizeContent"); 
        prizes = prizeContent.GetComponentsInChildren<Image>(); 
        SetDefaultPrizeColors();
        DisplayNextQuestion();
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            UpdateTimer(); 
        }
    }

    void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; 
            DisplayTime(timeRemaining); 
        }
        else
        {
            timeRemaining = 0;
            timerIsRunning = false;
            GameOver(); 
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); 

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    }

    void DisplayNextQuestion()
    {
        if (gameOver) return;

        timeRemaining = 30f;

        List<Question> currentQuestionList;

        if (currentQuestionIndex < 2)
        {
            currentQuestionList = easyQuestions;
        }
        else if (currentQuestionIndex < 7)
        {
            currentQuestionList = mediumQuestions;
        }
        else if (currentQuestionIndex < 12)
        {
            currentQuestionList = hardQuestions;
        }
        else
        {
            Debug.Log("Brak wi�cej pyta�.");
            return;
        }

        currentQuestion = GetNextQuestion(currentQuestionList);
        questionText.text = currentQuestion.QuestionText;

        foreach (var button in answerButtons)
        {
            button.interactable = true;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.Answers[i];
            int answerIndex = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
        }

        correctAnswerIndexes.Clear();
        for (int i = 0; i < currentQuestion.Answers.Length; i++)
        {
            if (i == currentQuestion.CorrectAnswerIndex)
            {
                correctAnswerIndexes.Add(i);
            }
        }

        currentQuestionIndex++;
        UpdatePrizeColors();
    }

    private Question GetNextQuestion(List<Question> questionList)
    {
        int randomIndex = Random.Range(0, questionList.Count);
        return questionList[randomIndex];
    }

    private void CheckAnswer(int selectedAnswerIndex)
    {

        Button selectedButton = answerButtons[selectedAnswerIndex];
        bool isCorrect = selectedAnswerIndex == currentQuestion.CorrectAnswerIndex;

        if (selectedAnswerIndex == currentQuestion.CorrectAnswerIndex)
        {
            Debug.Log("Correct answer!");

            
            ChangeButtonColor(selectedButton, Color.green);

            if (currentQuestionIndex >= 12)
            {
                Invoke("ShowWinPanel", 2f);
            }

        }
        else
        {
            Debug.Log("Incorrect answer");

            
            ChangeButtonColor(selectedButton, Color.red);
            Invoke("GameOver", 2f);
        }
        Invoke("DisplayNextQuestionWithColorReset", 2f);
    }

    void ChangeButtonColor(Button button, Color color)
    {
        var buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color; 
        }
    }

    void ResetButtonColors()
    {
        foreach (var button in answerButtons)
        {
            var buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = new Color(0, 120f / 255f, 206f / 255f);
            }
        }
    }

    void DisplayNextQuestionWithColorReset()
    {
        ResetButtonColors(); 
        DisplayNextQuestion();
        foreach (var button in answerButtons)
        {
            button.interactable = true;
        }

    }



    private void GameOver()
    {
        timerIsRunning = false;
        gameOver = true;
        /*foreach (Transform child in transform )
        {
            if (child.gameObject != gameOverPanel)
            {
                child.gameObject.SetActive(false);
            }
        }*/
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }
        resign.gameObject.SetActive(false);
        fiftyFiftyButton.gameObject.SetActive(false);
        callAFriendButton.gameObject.SetActive(false);
        restart.gameObject.SetActive(true);
        gameOverPanel.SetActive(true);
        Debug.Log("przegrana");
    }

    private struct Question
    {
        public string QuestionText;
        public string[] Answers;
        public int CorrectAnswerIndex;

        public Question(string questionText, string[] answers, int correctAnswerIndex)
        {
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }

    private void SetDefaultPrizeColors()
    {
        
        for (int i = 0; i < prizes.Length; i++)
        {
            if (i == currentQuestionIndex)
            {
                prizes[i].color = new Color(255f / 255f, 215f / 255f, 0f / 255f); 
            }
            else
            {
                prizes[i].color = new Color(0, 96f / 255f, 167f / 255f); 
            }
        }
    }

    private void UpdatePrizeColors()
    {
        
        int prizeIndex = currentQuestionIndex - 1; 
        if (prizeIndex >= 0 && prizeIndex < prizes.Length)
        {
            prizes[prizeIndex].color = new Color(0, 96f / 255f, 167f / 255f); 
        }

        prizeIndex = currentQuestionIndex; 
        if (prizeIndex >= 0 && prizeIndex < prizes.Length)
        {
            prizes[prizeIndex].color = new Color(255f / 255f, 215f / 255f, 0f / 255f); 
        }
    }

    public void UseFiftyFifty()
    {
        Color color = new Color(0, 120f / 255f, 206f / 255f);
        if (!fiftyFiftyUsed)
        {
            List<int> wrongAnswersIndexes = new List<int>();

            for (int i = 0; i < currentQuestion.Answers.Length; i++)
            {
                if (i != currentQuestion.CorrectAnswerIndex && answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text != "")
                {
                    wrongAnswersIndexes.Add(i);
                }
            }

            
            foreach (int index in wrongAnswersIndexes)
            {
                if (answerButtons[index].GetComponent<Image>().color != color)
                {
                    wrongAnswersIndexes.Remove(index);
                    break;
                }
            }

            
            int[] indexesToRemove = SelectRandomIndexes(wrongAnswersIndexes, 2);
            foreach (int index in indexesToRemove)
            {
                answerButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = "";
                answerButtons[index].interactable = false;
            }

            fiftyFiftyButton.gameObject.SetActive(false); 
            fiftyFiftyUsed = true; 
        }
    }

    int[] SelectRandomIndexes(List<int> indexes, int count)
    {
        
        HashSet<int> selectedIndexes = new HashSet<int>();
        while (selectedIndexes.Count < count)
        {
            int randomIndex = Random.Range(0, indexes.Count);
            selectedIndexes.Add(indexes[randomIndex]);
        }

        return selectedIndexes.ToArray();
    }

    public void UseCallAFriend()
    {
        if (!callAFriendUsed)
        {
            float probability = Random.Range(0f, 1f); 
            Color goldColor = new Color(255f / 255f, 215f / 255f, 0f / 255f); 

            List<int> availableAnswersIndexes = new List<int>(correctAnswerIndexes);

            if (fiftyFiftyUsed)
            {
                
                availableAnswersIndexes.RemoveAll(index => answerButtons[index].GetComponentInChildren<TextMeshProUGUI>().text == "");
            }

            if (probability <= 0.8f) 
            {
                
                int randomIndex = Random.Range(0, availableAnswersIndexes.Count);
                ChangeButtonColor(answerButtons[availableAnswersIndexes[randomIndex]], goldColor);
            }
            else 
            {
                
                List<int> wrongAnswersIndexes = Enumerable.Range(0, answerButtons.Length)
                    .Except(availableAnswersIndexes) 
                    .ToList();
                int randomIndex = Random.Range(0, wrongAnswersIndexes.Count);
                ChangeButtonColor(answerButtons[wrongAnswersIndexes[randomIndex]], goldColor);
            }

            callAFriendButton.gameObject.SetActive(false); 
            callAFriendUsed = true; 
        }
    }

    public void Resign()
    {
        timerIsRunning = false; 

        int prizeIndex = Mathf.Clamp(currentQuestionIndex - 1, 0, prizeAmounts.Length - 1); 
        int prizeAmount = prizeAmounts[prizeIndex];

        /*foreach (Transform child in transform)
        {
            if (child.gameObject != gameOverPanel)
            {
                child.gameObject.SetActive(false);
            }
        }*/
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }
        restart.gameObject.SetActive(true);
        resignPanel.SetActive(true); 
        resignText.text = "Gratulacje, wygra�e� " + prizeAmount.ToString("C0"); 
    }

    

    public void RestartGame()
    {
        Debug.Log("Restart.");
        
        timeRemaining = 30f;
        timerIsRunning = false;
        currentQuestionIndex = 0;
        fiftyFiftyUsed = false;
        callAFriendUsed = false;
        fiftyFiftyButton.gameObject.SetActive(true);
        callAFriendButton.gameObject.SetActive(true);
        resign.gameObject.SetActive(true);
        gameOver = false;
        correctAnswerIndexes.Clear(); 
        SetDefaultPrizeColors(); 
        gameOverPanel.SetActive(false); 
        resignPanel.SetActive(false); 
        InitializeGame();
        timerIsRunning = true; 
    }

    void InitializeGame()
    {
        gameOverPanel.SetActive(false);
        resignPanel.SetActive(false);
        winPanel.SetActive(false);
        ResetButtonColors();
        prizeContent = GameObject.Find("PrizeContent"); 
        prizes = prizeContent.GetComponentsInChildren<Image>(); 
        SetDefaultPrizeColors();
        DisplayNextQuestion();
        timerIsRunning = true;
    }

    void ShowWinPanel()
    {
        timerIsRunning = false;
        gameOver = true;
        resign.gameObject.SetActive(false);
        fiftyFiftyButton.gameObject.SetActive(false);
        callAFriendButton.gameObject.SetActive(false);
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }
        winPanel.SetActive(true); 
        Debug.Log("Wygrana!");
    }

}
