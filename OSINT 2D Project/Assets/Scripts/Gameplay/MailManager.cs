using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MailManager : MonoBehaviour
{
    public GameObject mailPreviewPrefab;

    public Transform mailPreviewsParent;
    List<GameObject> mailPreviews = new List<GameObject>();

    public GameObject currentMailtParent;
    public TMP_Text currentSender;
    public TMP_Text currentObject;
    public TMP_Text currentContent;
    public TMP_InputField inputField;
    public Button sendButton;

    public int unreadMailCount;
    public GameObject[] notifications;

    Mail currentMail;

    public void ReceiveMail(Request request) 
    {
        unreadMailCount++;
        UpdateUnread();

        foreach(GameObject mailPreview in mailPreviews) 
        {
            Vector3 newPosition = new Vector3(0, mailPreview.GetComponent<RectTransform>().anchoredPosition.y - 205f, 0);
            mailPreview.GetComponent<RectTransform>().anchoredPosition = newPosition;
        }

        mailPreviewsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(mailPreviewsParent.GetComponent<RectTransform>().sizeDelta.x, mailPreviewsParent.GetComponent<RectTransform>().sizeDelta.y + 205f);

        GameObject newMailPreview = Instantiate(mailPreviewPrefab, mailPreviewsParent);
        newMailPreview.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -5, 0);
        mailPreviews.Add(newMailPreview);

        newMailPreview.GetComponent<Mail>().request = request;
        newMailPreview.name = request.name;
    }

    public void SetCurrentMail(Mail mail) 
    {
        if (!currentMailtParent.activeSelf) 
        {
            currentMailtParent.SetActive(true);
        }

        //Reset previous current mail color
        if (currentMail != null) 
        {
           if (currentMail.answered) currentMail.gameObject.GetComponentInChildren<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
           else currentMail.gameObject.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        }

        currentMail = mail;

        currentSender.text = mail.request.requestSender;
        currentObject.text = mail.request.requestObject;
        currentContent.text = mail.request.requestContent;

        currentMail.gameObject.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.8f);

        if (currentMail.answered) 
        {
            inputField.interactable = false;
            inputField.text = mail.request.playerAnswer;
            sendButton.interactable = false;
            sendButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Sent";
        }
        else 
        {
            inputField.interactable = true;
            inputField.text = "";
            sendButton.interactable = true;
            sendButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Send";
        }
    }

    public void CheckAnswer(string playerAnswer) 
    {
        currentMail.answered = true;
        currentMail.gameObject.GetComponentInChildren<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);

        playerAnswer = FormatString(playerAnswer);

        currentMail.request.playerAnswer = playerAnswer;

        foreach (string correctAnswer in currentMail.request.correctAnswers) 
        {
            if (playerAnswer == FormatString(correctAnswer)) 
            {
                currentMail.AnsweredCorrectly();
                return;
            }
        }

        currentMail.AnsweredWrongly();
    }

    public void UpdateUnread() 
    {
        if (unreadMailCount < 0) 
        {
            unreadMailCount = 0;
        }

        if (unreadMailCount == 0) 
        {
            foreach (GameObject notification in notifications)
            {
                notification.SetActive(false);
            }
        }

        if (unreadMailCount == 1) 
        {
            foreach(GameObject notification in notifications) 
            {
                notification.SetActive(true);
            }
        }
        
        if (unreadMailCount > 0) 
        {
            foreach (GameObject notification in notifications)
            {
                if (unreadMailCount < 10)
                {
                    notification.GetComponentInChildren<TMP_Text>().text = unreadMailCount.ToString();
                }
                else 
                {
                    notification.GetComponentInChildren<TMP_Text>().text = "9+";
                }
            }
        }
    }

    string FormatString(string stringToFormat)
    {
        string newString = stringToFormat.Replace(" ", "");
        newString = newString.ToLower();

        return newString;
    }
}
