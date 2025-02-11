﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellingStation : MonoBehaviour
{
    public TextMeshProUGUI StationsText;
    private GameObject PlayerGO;
    private MoneyManager playerMoney;
    private Inventory playerInventory;
    private bool isInside = false;

    private void Start()
    {
        playerMoney = GameManager.Instance.MoneyManagerSC;
    }

    private void Update()
    {
        if (isInside)
            StationsText.SetText("PRESS F TO SELL ALL ORES\nEARNINGS: $" + GetEarningsOfInventory());

        if (PlayerGO == null)
        {
            PlayerGO = GameManager.GetCurrentPlayer();
            if (PlayerGO == null)
                return;
            playerInventory = PlayerGO.GetComponent<Inventory>();
        }

        if (isInside && Input.GetKeyDown(KeyCode.F))
        {
            int totalEarnings = GetEarningsOfInventory();
            if (totalEarnings > 0)
            {
                AudioSource.PlayClipAtPoint(GameManager.Instance.CoinPickupSound, PlayerGO.transform.position);
                playerMoney.AddMoney(totalEarnings);
                playerInventory.RemoveAllOres();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isInside = true;
            AudioSource.PlayClipAtPoint(GameManager.Instance.ClickSound, collision.transform.position);
            StationsText.SetText("PRESS F TO SELL ALL ORES\nEARNINGS: $0");
            StationsText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isInside = false;
            AudioSource.PlayClipAtPoint(GameManager.Instance.ClickSound, collision.transform.position);
            StationsText.gameObject.SetActive(false);
        }
    }

    private int GetEarningsOfInventory()
    {
        int silverOres = playerInventory.GetNumberOfOresWithName("Iron");
        int goldOres = playerInventory.GetNumberOfOresWithName("Gold");
        int emeraldOres = playerInventory.GetNumberOfOresWithName("Emerald");
        int redIronOres = playerInventory.GetNumberOfOresWithName("Red Iron");
        int lapisOres = playerInventory.GetNumberOfOresWithName("Lapis");
        int fossileOres = playerInventory.GetNumberOfOresWithName("Fossile");
        int totalEarnings = 5 * silverOres + 20 * goldOres + 50 * emeraldOres + 75 * redIronOres + 100 * lapisOres + 200 * fossileOres;
        return totalEarnings;
    }
}
