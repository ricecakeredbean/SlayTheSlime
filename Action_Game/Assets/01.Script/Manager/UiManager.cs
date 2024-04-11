using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text[] texts;
    public Image[] images;
    public Button[] buttons;

    public Slider hpBar;

    private void Awake()
    {
        Instance = this;
    }

    public void UiUpdate()
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
        texts[0].text = string.Format("Time : {0}:{1}", m, Mathf.Floor(time));
        texts[1].text = $"{GameManager.Instance.Gold:000,000,000}";
        hpBar.value = Player.Instance.Hp;
        if (hpBar.value <= 0)
        {
            hpBar.fillRect.localScale = new Vector3(0, 0, 0);
        }
    }

    public void GameOverUi()
    {
        for (int i = 0; i < texts.Length - 2; i++)
        {
            texts[i].enabled = false;
        }
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = false;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
        texts[2].gameObject.SetActive(true);
    }

    public void WinUi()
    {
        for (int i = 0; i < texts.Length - 1; i++)
        {
            texts[i].enabled = false;
        }
        for (int i = 0; i < images.Length-1; i++)
        {
            images[i].enabled = false;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
        images[2].gameObject.SetActive(true);
        texts[3].gameObject.SetActive(true);
    }
}
