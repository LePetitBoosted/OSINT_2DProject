using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public GameObject conversationPreviewPrefab;

    public Transform conversationPreviewsParent;
    List<GameObject> conversationPreviews = new List<GameObject>();

    public GameObject conversationPrefab;

    public int unreadConversationCount;
    public GameObject[] notifications;

    Conversation currentConversation;

    public void ReceiveMessage(Request request) 
    {
      
    }

    public void SetCurrentConversation(Conversation conversation) 
    {
        if (currentConversation.conversationPannel != null) 
        {
            currentConversation.conversationPannel.SetActive(false);
        }

        conversation.conversationPannel.SetActive(true);
        currentConversation = conversation;
    }

    public void CheckAnswer(string playerAnswer) 
    {
        currentConversation.answered = true;
        currentConversation.gameObject.GetComponentInChildren<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);

        playerAnswer = FormatString(playerAnswer);

        currentConversation.lastRequest.playerAnswer = playerAnswer;

        foreach (string correctAnswer in currentConversation.lastRequest.correctAnswers) 
        {
            if (playerAnswer == FormatString(correctAnswer)) 
            {
                currentConversation.AnsweredCorrectly();
                return;
            }
        }

        currentConversation.AnsweredWrongly();
    }

    public void UpdateUnread() 
    {
        if (unreadConversationCount < 0) 
        {
            unreadConversationCount = 0;
        }

        if (unreadConversationCount == 0) 
        {
            foreach (GameObject notification in notifications)
            {
                notification.SetActive(false);
            }
        }

        if (unreadConversationCount == 1) 
        {
            foreach(GameObject notification in notifications) 
            {
                notification.SetActive(true);
            }
        }
        
        if (unreadConversationCount > 0) 
        {
            foreach (GameObject notification in notifications)
            {
                if (unreadConversationCount < 10)
                {
                    notification.GetComponentInChildren<TMP_Text>().text = unreadConversationCount.ToString();
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
