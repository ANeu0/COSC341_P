using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public static class GameMain
{
    public static DateTime GameStartedAt;
    public static DateTime LastCoinEvent;
    public static int coinCount = 0;

    public static string DataFileLocation { get; internal set; }
    public static string Technique { get; internal set; }
}
