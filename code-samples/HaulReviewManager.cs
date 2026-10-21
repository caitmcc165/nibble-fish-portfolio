/// <summary>
/// Processes the player's completed fishing haul, identifies newly discovered
/// fish, calculates rewards, updates persistent discovery data, and controls
/// the transition from individual fish reveals to the haul summary.
/// </summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HaulReviewManager : MonoBehaviour
{
    public Image fishImage;

    public TMP_Text fishName;
    public TMP_Text variantName;
    public TMP_Text rarityText;
    public TMP_Text pointsText;
    public TMP_Text biomeText;
    public TMP_Text newBadge;
    public GameObject cardPanel;
    public GameObject haulSummaryPanel;
    public TMP_Text totalPointsText;

    private List<FishData> newFish = new List<FishData>();
    private int currentFishIndex = 0;
    private int totalPoints = 0;

    void Start()
    {
        HashSet<string> seenThisHaul = new HashSet<string>();

        foreach (FishData fish in RunData.CaughtFish)
        {
            // Every fish contributes points, even duplicates.
            totalPoints += fish.pointValue;

            // Only add genuinely new fish once to the reveal list.
            if (!DiscoveryData.IsDiscovered(fish.fishId) &&
                seenThisHaul.Add(fish.fishId))
            {
                newFish.Add(fish);
            }
        }

        if (newFish.Count > 0)
        {
            ShowCurrentFish();
        }
        else
        {
            ShowSummary();
        }
    }

    void ShowCurrentFish()
    {
        FishData fish = newFish[currentFishIndex];

        ShowFish(fish);
        DiscoveryData.MarkDiscovered(fish.fishId);
    }

    void ShowFish(FishData fish)
    {
        fishImage.sprite = fish.sprite;
        fishImage.preserveAspect = true;

        fishName.text = fish.fishName;
        variantName.text = fish.variantName;
        rarityText.text = fish.rarity.ToString();
        pointsText.text = "+" + fish.pointValue + " Points";
        biomeText.text = fish.biome.ToString();

        newBadge.text = "NEW!";
    }

    public void ShowNextFish()
    {
        currentFishIndex++;

        if (currentFishIndex < newFish.Count)
        {
            ShowCurrentFish();
        }
        else
        {
            ShowSummary();
        }
    }

    void ShowSummary()
    {
        PlayerEconomy.AddCoins(totalPoints);

        cardPanel.SetActive(false);
        haulSummaryPanel.SetActive(true);
        totalPointsText.text = "Coins Earned: " + totalPoints;
    }

    public void ContinueFishing()
    {
        SceneManager.LoadScene("PondScene");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
