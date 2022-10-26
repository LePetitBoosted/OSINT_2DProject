using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SendButton : MonoBehaviour
{
    public TMP_InputField inputField;
    public bool isMailButton;
    public MailManager mailManager;
    public MessageManager messageManager;

    private void Start()
    {
        if (!isMailButton) 
        {
            messageManager = FindObjectOfType<MessageManager>().GetComponent<MessageManager>();
        }
    }

    public void Send() 
    {
        if (inputField.text != "")
        {

            if (isMailButton && mailManager != null)
            {
                mailManager.CheckAnswer(inputField.text);
            }

            else if (!isMailButton && messageManager != null) 
            {
                messageManager.CheckAnswer(inputField.text);
            }

            inputField.interactable = false;
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<TMP_Text>().text = "Sent";
        }
    }
}
