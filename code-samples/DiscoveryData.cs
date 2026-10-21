/// <summary>
/// Maintains persistent fish-discovery state across game sessions.
/// Uses a HashSet to prevent duplicate discoveries and Unity PlayerPrefs
/// to save and restore discovered fish identifiers.
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public static class DiscoveryData
{
    private const string SaveKey = "DiscoveredFishIds";

    public static HashSet<string> DiscoveredFishIds = new HashSet<string>();

    static DiscoveryData()
    {
        Load();
    }

    public static bool IsDiscovered(string fishId)
    {
        return DiscoveredFishIds.Contains(fishId);
    }

    public static void MarkDiscovered(string fishId)
    {
        if (string.IsNullOrEmpty(fishId))
        {
            return;
        }

        // Add() returns true only if this fish was not already discovered.
        if (DiscoveredFishIds.Add(fishId))
        {
            Save();
        }
    }

    private static void Save()
    {
        string savedData = string.Join(",", DiscoveredFishIds);

        PlayerPrefs.SetString(SaveKey, savedData);
        PlayerPrefs.Save();
    }

    private static void Load()
    {
        DiscoveredFishIds.Clear();

        string savedData = PlayerPrefs.GetString(SaveKey, "");

        if (string.IsNullOrEmpty(savedData))
        {
            return;
        }

        string[] fishIds = savedData.Split(',');

        foreach (string fishId in fishIds)
        {
            if (!string.IsNullOrEmpty(fishId))
            {
                DiscoveredFishIds.Add(fishId);
            }
        }
    }
}
