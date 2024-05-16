using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject idleUI;

    public GameObject gameEndUI;

    public GameObject gameOverBG;

    public Text gameEndText;

    public Text timeText;
    public Text coinText;

    public Slider hpBar;

    public void UIUpdate()
    {
        float time;
        int m = 0;
        time = GameManager.Instance.LimitTime;
        for (; time > 60;)
        {
            if (time > 60)
            {
                time -= 60;
                m++;
            }
        }
        timeText.text = $"Time : {m:00}:{time:00.00}";
        coinText.text = $"{GameManager.Instance.Gold:000,000,000}";
        hpBar.value = Player.Instance.Hp;
        if (hpBar.value <= 0)
        {
            hpBar.fillRect.localScale = new Vector3(0, 0, 0);
        }
    }

    public void GameOverUI()
    {
        idleUI.SetActive(false);
        gameEndUI.SetActive(true);
        gameEndText.text = "Game Over";
        gameEndText.color = Color.red;
        gameOverBG.SetActive(true);
    }

    public void WinUi()
    {
        idleUI.SetActive(false);
        gameEndUI.SetActive(true);
    }
}
