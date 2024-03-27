using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChallengerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopTemplatePrefab;
    [SerializeField]
    private string[] _questions;
    private int _randomQuestionId;

    private void Start()
    {
        _randomQuestionId = UnityEngine.Random.Range(0, 3);
    }
    public void GenerateRandomQuestion()
    {
        string questionToBeGenerated = _questions[_randomQuestionId];

        GameObject chalBtn = GameObject.Find("Challenge_Btn");
        GameObject questionPanel = chalBtn.transform.GetChild(1).gameObject;

        questionPanel.SetActive(true);
        Text textForTheQuestion = GameObject.Find("Question").GetComponent<Text>();
        textForTheQuestion.text = questionToBeGenerated;
    }

    public void ValidateAnswer(string playerAnswer)
    {
        if (_randomQuestionId == 0)
        {
            if (playerAnswer == "London")
            {
                Debug.Log("You answered correct");
            }
            else
            {
                Debug.Log("Your answer is wrong");
            }
        }
        else if (_randomQuestionId == 1)
        {
            if (playerAnswer == "1914")
            {
                Debug.Log("You answered correct");
            }
            else
            {
                Debug.Log("Your answer is wrong");
            }
        }
        else if (_randomQuestionId == 2)
        {
            if (playerAnswer == "1939")
            {
                Debug.Log("You answered correct");
            }
            else
            {
                Debug.Log("Your answer is wrong");
            }
        }
    }
}
