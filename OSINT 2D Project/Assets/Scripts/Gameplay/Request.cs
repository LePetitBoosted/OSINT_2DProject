using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Request", menuName = "Request")]
public class Request : ScriptableObject
{
    public string requestSender;
    public string requestObject;
    public string requestContent;

    public float timeWindow;

    public string[] correctAnswers;

    public string playerAnswer;
}
