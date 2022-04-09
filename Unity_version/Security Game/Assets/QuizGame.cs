using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;
public class Question
{
    public string question;
    public string[] answers;

    public int correct_answer_idx;

    public Question(string question, int correct_answer_idx, string[] answers)
    {
        this.question = question;
        this.answers = answers;
        this.correct_answer_idx = correct_answer_idx;
    }
}

public class QuizGame : MonoBehaviour
{
    private static Question[] questions = {
        new Question("Which cognitive bias is related to people being over-reliant on the first peice of information they hear", 0, new string[] {
            "Anchoring bias",
            "Confirmation bias",
            "Information bias",
            "Availability heuristic"
        }),
        new Question("Which cognitive bias is related to people being over-reliant on the first peice of information they hear", 0, new string[] {
            "Anchoring bias",
            "Confirmation bias",
            "Information bias",
            "Availability heuristic"
        }),
        new Question("Which cognitive bias is related to people overestimating the importance of information that is available to them", 3, new string[] {
            "Anchoring bias",
            "Confirmation bias",
            "Information bias",
            "Availability heuristic"
        }),
        new Question("Which cognitive bias is related to people only listening to information that confirms their preconceptions", 1, new string[] {
            "Anchoring bias",
            "Confirmation bias",
            "Information bias",
            "Availability heuristic"
        }),
        new Question("Which of the attacks involves: Brute force attack which involves entering every word in a dictionary, common phrases or common/leaked passwords as a password", 0, new string[] {
            "Dictionary attack",
            "Chosen-plaintext attack",
            "Birthday attack",
            "Rainbow/lookup table"
        }),
        new Question("Which of the attacks involves: Finding two different messages such that their hash output is the same", 2, new string[] {
            "Dictionary attack",
            "Chosen-plaintext attack",
            "Birthday attack",
            "Rainbow/lookup table"
        }),
        new Question("Which has the definition: Hard to find two inputs with the same hash output", 0, new string[] {
            "Collision resistance",
            "Preimage resistance",
            "Avalanche effect",
            "Second preimage resistance"
        }),
        new Question("Which has the definition: Given same input, the function will return the same output", 1, new string[] {
            "Collision resistance",
            "Deterministic",
            "Avalanche effect",
            "Second preimage resistance"
        }),
        new Question("Which has the definition: A cipher where a small change in the plain text results in a large change (random) in the ciphertext", 2, new string[] {
            "Collision resistance",
            "Deterministic",
            "Avalanche effect",
            "Second preimage resistance"
        })
    };

    // Start is called before the first frame update
    private GameObject PauseMenuCanvas;

    private GameObject ClipboardText;
    private ClickableText[] answers;
    private int curr_question_idx;

    private bool clicked = false;

    private int correct_answers = 0;
    private System.Action<string> CreateOnClick(int curr_idx, int correct_idx, ClickableText textComp)
    {
        return async (string inp) =>
            {
                if (clicked == true)
                {
                    // If already clicked, do nothing
                    return;
                }
                clicked = true;

                if (curr_idx == correct_idx)
                {
                    Debug.Log("Correct Input");
                    textComp.Text.color = new Color(0, 255, 0, 1);
                    correct_answers++;
                }
                else
                {
                    Debug.Log("Incorrect Input");
                    textComp.Text.color = new Color(255, 0, 0, 1);
                }
                Debug.Log(curr_question_idx);
                if (curr_question_idx + 1 == questions.Length)
                {
                    // End game
                    GameMaster.otherScores = new Dictionary<string, string> {
                        { "score", correct_answers.ToString() + " / " + questions.Length.ToString() }
                    };
                    GameMaster.timeElapsed = (int)Time.timeSinceLevelLoad;
                    SceneManager.LoadScene("Victory");
                    return;
                }

                await Task.Delay(2000);
                textComp.Text.color = new Color(0, 0, 0, 1);
                clicked = false;
                SetupQuestion(++curr_question_idx);
            };
    }

    void SetupQuestion(int question_num)
    {
        var selectedQuestion = questions[question_num];
        var questionTextComp = ClipboardText.GetComponent<TextMeshPro>();
        questionTextComp.SetText(selectedQuestion.question);

        answers = questionTextComp.GetComponentsInChildren<ClickableText>();

        string letters = "ABCD";

        for (int i = 0; i < selectedQuestion.answers.Length; ++i)
        {
            answers[i].SetText(letters[i] + ") " + selectedQuestion.answers[i]);
            answers[i].OnClick = CreateOnClick(i, selectedQuestion.correct_answer_idx, answers[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuCanvas = GameObject.Find("PauseMenuCanvas");
        PauseMenuCanvas.SetActive(false);

        ClipboardText = GameObject.Find("ClipboardText");
        SetupQuestion(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuCanvas.SetActive(!PauseMenuCanvas.activeSelf);
        }
    }
}
