using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance = null;

    public bool isGameOver=false;

    public GameObject retryButton;

    [SerializeField]
    private TextMeshProUGUI textGoal;

    [SerializeField]
    private int goal;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }
    void Start()
    {
        textGoal.SetText(goal.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseGoal()
    { 
        goal = goal - 1;  
        if(goal <= 0)
        {
            SetGameOver(true);
        }
        textGoal.text = goal.ToString();
    }

    public void SetGameOver(bool success)
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Camera.main.backgroundColor=success? Color.green : Color.red;
        }
        Invoke("ShowRetryButton", 1f);
        


    }

    void ShowRetryButton(){
        retryButton.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
