using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MailManager mailManager;
    public MessageManager messageManager;

    public Request[] requests;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            mailManager.ReceiveMail(requests[0]);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            messageManager.ReceiveMessage(requests[0]);
        }

        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            messageManager.ReceiveMessage(requests[1]);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            messageManager.ReceiveMessage(requests[2]);
        }
    }
}
