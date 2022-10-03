using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mail : MonoBehaviour
{
    MailManager mailManager;

    public TMP_Text previewSenderText;
    public TMP_Text previewObjectText;
    public TMP_Text previewContentText;
    public Request request;

    bool read = false;
    public bool answered = false;

    private void Start()
    {
        SetPreviewText();

        mailManager = FindObjectOfType<MailManager>().GetComponent<MailManager>();
    }

    private void SetPreviewText() 
    {
        previewSenderText.text = request.requestSender;
        previewObjectText.text = request.requestObject;
        previewContentText.text = request.requestContent;
    }

    public void MakeCurrentMail() 
    {
        if (!read) 
        {
            read = true;

            mailManager.unreadMailCount--;
            mailManager.UpdateUnread();

            previewSenderText.fontStyle = FontStyles.Normal;
            previewObjectText.fontStyle = FontStyles.Normal;
        }

        mailManager.SetCurrentMail(this);
    }

    public void AnsweredCorrectly() 
    {
        Debug.LogError("Correct!");
    }

    public void AnsweredWrongly()
    {
        Debug.LogError("Wrong!");
    }
}
