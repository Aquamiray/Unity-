using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private TMP_Text coinText;

    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private int gemCount = 0;
    private bool isGameOver = false;
    private Vector3 playerPosition;

    

    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text leveCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;





    private int totalCoins = 0;




    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        UIManager.instance.fadeFromBlack = true;
        playerPosition = playerController.transform.position;

        FindTotalPickups();
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
    }
    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString();

    }

    public void Death()
    {
        if (!isGameOver)
        {
            
            UIManager.instance.DisableMobileControls();
           
            UIManager.instance.fadeToBlack = true;

           
            playerController.gameObject.SetActive(false);

            
            StartCoroutine(DeathCoroutine());

           
            isGameOver = true;

           
            Debug.Log("Died");
        }
    }

    public void FindTotalPickups()
    {

        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();

        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
            {
                totalCoins += 1;
            }

        }



    }
    public void LevelComplete()
    {



        levelCompletePanel.SetActive(true);
        leveCompletePanelTitle.text = "LEVEL COMPLETE";



        levelCompleteCoins.text = "COINS COLLECTED: " + coinCount.ToString() + " / " + totalCoins.ToString();

    }

    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        playerController.transform.position = playerPosition;

        
        yield return new WaitForSeconds(1f);

        
        if (isGameOver)
        {
            SceneManager.LoadScene(2);


        }
    }

}

