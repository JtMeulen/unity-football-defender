using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    GameSession gameSession;
    TextMeshProUGUI textComponent;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textComponent.text = gameSession.GetScore().ToString();
    }
}
