using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailManager : MonoBehaviour
{
    public TMP_Text currentSender;
    public TMP_Text currentObject;
    public TMP_Text currentContent;

    Mail currrentMail;

    public void SetCurrentMail(Mail mail) 
    {
        currrentMail = mail;

        currentSender.text = mail.request.requestSender;
        currentObject.text = mail.request.requestObject;
        currentContent.text = mail.request.requestContent;
    }
}
