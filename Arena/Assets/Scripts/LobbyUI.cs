using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Launcher launcher;
    private void Start()
    {
        playButton.onClick.AddListener(OnPlayCliced);
        statusText.text = "Готов к подключению";
    }
    private void OnEnable()
    {
        launcher.OneStatusChenged += UpdateStatus;
    }
    private void OnDisable()
    {
        launcher.OneStatusChenged -= UpdateStatus;
    }
    private void OnPlayCliced()
    {
        string playerName = nameInput.text;

        if (string.IsNullOrWhiteSpace(playerName))
        {
            statusText.text = "Введите имя !!!";
            return;
        }
        playButton.interactable = false;
        launcher.Connect(playerName);
    }
    private void UpdateStatus(string status)
    {
        statusText.text = status;
    }
}
