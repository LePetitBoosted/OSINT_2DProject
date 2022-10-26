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

    List<GameObject> messages = new List<GameObject>();
    public GameObject messagePrefab;
    Transform messageParent;

    List<Request> requests = new List<Request>();
    [ReadOnly] public Request lastRequest;

    bool read = false;
    [ReadOnly] public bool answered = false;

    private void Start()
    {
        messageManager = FindObjectOfType<MessageManager>().GetComponent<MessageManager>();
    }

    private void SetPreviewText()
    {
        previewSenderText.text = lastRequest.requestSender;
        previewTimeText.text = lastRequest.timeWindow.ToString(); //Need conversion
        previewContentText.text = lastRequest.requestContent;
    }

    public void CreateConversationPannel() 
    {
        conversationPannelParent = transform.parent.parent.parent.parent.Find("ConversationPannels"); //So dirty
        conversationPannel = Instantiate(convsersationPannelPrefab, conversationPannelParent);
        conversationPannel.name = gameObject.name + " Pannel";

        messageParent = conversationPannel.transform.Find("Conversation Scroll View/Viewport/Content"); //Dirty
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

        SetPreviewText();

        foreach (GameObject message in messages)
        {
            Vector3 newPosition = new Vector3(message.GetComponent<RectTransform>().anchoredPosition.x, message.GetComponent<RectTransform>().anchoredPosition.y + 205f, 0);
            message.GetComponent<RectTransform>().anchoredPosition = newPosition;
        }

        messageParent.GetComponent<RectTransform>().sizeDelta = new Vector2(messageParent.GetComponent<RectTransform>().sizeDelta.x, messageParent.GetComponent<RectTransform>().sizeDelta.y + 205f);

        GameObject newMessage = Instantiate(messagePrefab, messageParent);
        newMessage.GetComponent<RectTransform>().anchoredPosition = new Vector3(350, 0, 0);
        messages.Add(newMessage);

        newMessage.GetComponentInChildren<TMP_Text>().text = newRequest.requestContent;

        //Reset InputField & Button
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
