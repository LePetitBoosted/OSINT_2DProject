using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Conversation : MonoBehaviour
{
    MessageManager messageManager;

    public TMP_Text previewSenderText;
    public TMP_Text previewTimeText;
    public TMP_Text previewContentText;

    public GameObject conversationPannel;

    public GameObject messagePrefab;

    List<Request> requests = new List<Request>();
    public Request lastRequest;

    bool read = false;
    public bool answered = false;

    private void Start()
    {
        SetPreviewText();

        messageManager = FindObjectOfType<MessageManager>().GetComponent<MessageManager>();
    }

    private void SetPreviewText()
    {
        previewSenderText.text = lastRequest.requestSender;
        previewTimeText.text = lastRequest.timeWindow.ToString(); //Need conversion
        previewContentText.text = lastRequest.requestContent;
    }

    public void MakeCurrentConversation()
    {
        if (!read)
        {
            read = true;

            messageManager.unreadConversationCount--;
            messageManager.UpdateUnread();

            previewSenderText.fontStyle = FontStyles.Normal;
            previewTimeText.fontStyle = FontStyles.Normal;
        }

        messageManager.SetCurrentConversation(this);
    }

    public void AddRequest(Request newRequest) 
    {
        requests.Add(newRequest);
        lastRequest = newRequest;
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
