using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string technique = "";
    public string dataFile = "GameData.csv";
    private int score = 0;
    public TextMeshProUGUI TMP_coin;

    public void init()
    {
        if (Instance == null)
        {
            GameMain.GameStartedAt = DateTime.UtcNow;
            GameMain.DataFileLocation = dataFile;
            CSVHelper.initCSV(new Data(), dataFile);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        init();
    }

    private void Start()
    {

    }


}
