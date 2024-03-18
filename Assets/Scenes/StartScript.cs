using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartingScreenController : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField nameInputField;
    public Button startButton;
    public TextMeshProUGUI playerNameText;
    public GameObject qAndAContent;

    private void Start()
    {
        
        ShowNameInput();
    }

    public void ShowNameInput()
    {
       
        questionText.text = "Jak siê nazywasz?";
        nameInputField.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        playerNameText.gameObject.SetActive(false); 
        qAndAContent.SetActive(false);
    }

    public void SubmitName()
    {
        string playerName = nameInputField.text;
        playerNameText.text = playerName + ", grasz o MILION z³otych!";
        playerNameText.gameObject.SetActive(true); 
        questionText.gameObject.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        qAndAContent.SetActive(true);
    }
}