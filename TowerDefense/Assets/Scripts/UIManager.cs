using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject shop;
    public GameObject close;

    public TMP_Text goldText;
    public TextMeshProUGUI livesText;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void OpenShop()
    {
        shop.SetActive(true);
        close.SetActive(true);
    }

    public void CloseShop()
    {
        shop.SetActive(false);
        close.SetActive(false);

        buildManager.SelectTurretToBuild(null);
    }

    private void Update()
    {
        goldText.text = PlayerStats.Money.ToString();
        livesText.text = "Lives : " + PlayerStats.lives.ToString();

        if(PlayerStats.lives<=2)
        {
            livesText.color= Color.red;
        }
    }
}
