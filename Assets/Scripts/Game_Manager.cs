using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timeRemaining = 60f; 
    private bool isTimerRunning = true;

    public GameObject youWin_Text;

    private void Start()
    {
        youWin_Text.SetActive(false);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                UpdateTimerDisplay();
            }
            else
            {
                isTimerRunning = false;
                timeRemaining = 0; 
                UpdateTimerDisplay();
                Debug.Log("Time's up!");
                youWin_Text.SetActive(true);
            }
        }
    }

    void UpdateTimerDisplay()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void CallReloadCoroutine()
    {
        StartCoroutine(ReloadSceneWithDelay());
    }
    IEnumerator ReloadSceneWithDelay()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("SampleScene");
    }
}
