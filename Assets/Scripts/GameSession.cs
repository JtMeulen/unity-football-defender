using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int totalScore = 0;
    [SerializeField] float scoreTimerSeconds = 1f;

    Coroutine addTimeScoreCoroutine;

    private void Awake()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        addTimeScoreCoroutine = StartCoroutine(AddTimeScore());
    }

    IEnumerator AddTimeScore()
    {
        while (true)
        {
            totalScore++;
            yield return new WaitForSeconds(scoreTimerSeconds);
        }
    }

    public int GetScore()
    {
        return totalScore;
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }

    public void StopScore()
    {
        StopCoroutine(addTimeScoreCoroutine);
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
