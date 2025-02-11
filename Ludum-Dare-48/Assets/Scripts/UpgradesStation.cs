﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesStation : MonoBehaviour
{
    // Singleton
    private static UpgradesStation _instance;
    public static UpgradesStation Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UpgradesStation>();
            }

            return _instance;
        }

    }

    public TextMeshProUGUI StationsText;
    public GameObject UpgradesPanelGO;
    private bool isInside = false;
    private bool isShopOpened = false;

    private void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.F) && !GameManager.GetCurrentPlayer().GetComponent<Inventory>().IsOpen())
        {
            AudioSource.PlayClipAtPoint(GameManager.Instance.ClickSound, GameManager.GetCurrentPlayer().transform.position);
            SetShopState(true);
        }
    }

    public static void SetShopState(bool state)
    {
        Instance.isShopOpened = state;
        Instance.UpgradesPanelGO.SetActive(state);
    }

    public static bool IsShopOpen()
    {
        return Instance.isShopOpened;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isInside = true;
            AudioSource.PlayClipAtPoint(GameManager.Instance.ClickSound, collision.transform.position);
            StationsText.SetText("PRESS F TO ENTER SHOP");
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
}
