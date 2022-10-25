using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class Conversation : MonoBehaviour
{
    MessageManager messageManager;

    public TMP_Text previewSenderText;
    public TMP_Text previewTimeText;
    public TMP_Text previewContentText;

    [ReadOnly] public GameObject conversationPannel;
    public GameObject convsersationPannelPrefab;
    Transform conversationPannelParent;

    public GameObject messagePrefab;
    Transform messageParent;

    List<Request> requests = new List<Request>();
    [ReadOnly] public Request lastRequest;

    bool read = false;
    [ReadOnly] public bool answered = false;

    private void Start()
    {
        messageManager = FindObjectOfType<MessageManager>().GetComponent<MessageManager>();

        SetPreviewText();
        CreateConversationPannel();
    }

    private void SetPreviewText()
    {
        previewSenderText.text = lastRequest.requestSender;
        previewTimeText.text = lastRequest.timeWindow.ToString(); //Need conversion
        previewContentText.text = lastRequest.requestContent;
    }

    private void CreateConversationPannel() 
    {
        conversationPannelParent = transform.parent.parent.parent.parent.Find("ConversationPannels"); //So dirty
        conversationPannel = Instantiate(convsersationPannelPrefab, conversationPannelParent);
        conversationPannel.name = lastRequest.requestSender + " Pannel";

        messageParent = conversationPannel.transform.Find("Content"); //Dirty
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

        //GameObject newMessage = Instantiate(messagePrefab, messageParent); WIIP
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
