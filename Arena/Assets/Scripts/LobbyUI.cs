using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Text statusText;
    private void Start()
    {
        playButton.onClick.AddListener(OnPlayCliced);
        statusText.text = "Готов к подключению";
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
        statusText.text = $"Подключение как {playerName}...";
    }
}
