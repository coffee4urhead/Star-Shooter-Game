using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void LoadShop()
    {
        DontDestroyOnLoad(gameObject);

        GameObject livesDispl = GameObject.Find("Lives_Display_img");
        livesDispl.SetActive(false);
        GameObject gotoShopBtn = GameObject.Find("GoToShop_Menu");
        gotoShopBtn.SetActive(false);
        GameObject challengesImg = GameObject.Find("Challenges_img");
        challengesImg.SetActive(false);

        SceneManager.LoadScene(2);
    }
}
