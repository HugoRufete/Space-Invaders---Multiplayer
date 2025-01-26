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

    public Alien_Instantiator alien_Instantiator;
    public TurretPlacer turretPlacer;
    public Enemy_Ship_Placer enemy_Ship_Placer;
    public Ship_Shotting ship_Shotting;
    public Ship_Controller ship_Controller;
    private void Start()
    {
        youWin_Text.SetActive(false);

        //Desactivar Código
        alien_Instantiator.enabled = false;
        turretPlacer.enabled = false;
        enemy_Ship_Placer.enabled = false;
        ship_Shotting.enabled = false;
        ship_Controller.enabled = false;
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

    public void StartGame()
    {
        alien_Instantiator.enabled = true;
        turretPlacer.enabled = true;
        enemy_Ship_Placer.enabled = true;
        ship_Shotting.enabled = true;
        ship_Controller.enabled = true;
    }
}
