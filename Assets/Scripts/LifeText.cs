using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeText : MonoBehaviour
{
    TextMeshProUGUI lifeText;

    void Start()
    {
        lifeText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateHealthText(int health)
    {
        lifeText.text = health.ToString();
    }
}
