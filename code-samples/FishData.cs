using UnityEngine;

/// <summary>
/// Defines reusable configuration data for each fish in Nibble Fish.
/// Fish content is stored as ScriptableObject assets so gameplay systems
/// can reference data without hard-coding individual fish behavior.
/// </summary>

public enum FishRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public enum FishBiome
{
    Pond,
    Lake,
    River,
    Coast,
    CoralReef,
    Shipwreck,
    Ocean
}

public enum FishSizeClass
{
    Tiny,
    Small,
    Medium,
    Large,
    Huge,
    Giant
}

[CreateAssetMenu(fileName = "NewFishData", menuName = "Fish/Fish Data")]
public class FishData : ScriptableObject
{
    [Header("Identity")]
    public string fishId;
    public string fishName;
    public string variantName = "Standard";

    [Header("Collection Data")]
    public FishRarity rarity = FishRarity.Common;
    public int pointValue = 100;
    public FishBiome biome = FishBiome.Pond;

    [Header("Gameplay")]
    public FishSizeClass sizeClass = FishSizeClass.Medium;
    public int capacityCost = 1;

    [Header("Visual")]
    public Sprite sprite;
}
