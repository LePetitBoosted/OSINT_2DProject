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
        mailManager.SetCurrentMail(this);
    }
}
