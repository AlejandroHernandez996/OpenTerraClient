using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public Button queueButton;
    public GameObject networkManager;
    public InputField playerNameField;
    public InputField deckCodeField;

    void Start()
    {
        queueButton.GetComponent<Button>().onClick.AddListener(OnQueueClick);
    }

    void OnQueueClick()
    {
        DataSender.SendNameDeckServer(playerNameField.text, deckCodeField.text);
    }
}
