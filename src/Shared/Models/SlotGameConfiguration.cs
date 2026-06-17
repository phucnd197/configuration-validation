namespace ConfigurationValidation.Shared.Models;

// Root Configuration Object
public record SlotConfig(
    GameMetadata GameMetadata,
    BetSettings BetSettings,
    ReelConfiguration ReelConfiguration,
    List<PayoutRule> PayoutTable,
    JackpotSettings JackpotSettings,
    FeatureFlags FeatureFlags,
    LiveOpsCampaign LiveOpsCampaign
)
{
    public void Validate(ref List<string> errors)
    {
        GameMetadata.Validate(ref errors);
        BetSettings.Validate(ref errors);
        ReelConfiguration.Validate(ref errors);
        foreach (var rule in PayoutTable)
        {
            rule.Validate(ref errors);
        }
        JackpotSettings.Validate(ref errors);
        FeatureFlags.Validate(ref errors);
        LiveOpsCampaign.Validate(ref errors);
    }
}

public record GameMetadata(
    string GameName,
    string SlotMachineId,
    string AppVersionTarget,
    List<string> BuildTargets
)
{
    public void Validate(ref List<string> errors)
    {
        if (string.IsNullOrEmpty(GameName))
        {
            errors.Add("Gamename is empty");
        }
        if (string.IsNullOrEmpty(SlotMachineId))
        {
            errors.Add("SlotmachineId is empty");
        }
        if (string.IsNullOrEmpty(AppVersionTarget))
        {
            errors.Add("AppVersion is missing");
        }
    }
}

public record BetSettings(
    string Currency,
    int DefaultBet,
    List<int> BetLevels
)
{
    public void Validate(ref List<string> errors)
    {
    }
}

public record ReelConfiguration(
    int ReelCount,
    int RowCount,
    List<SymbolDefinition> Symbols
)
{
    public void Validate(ref List<string> errors)
    {
    }
}

public record SymbolDefinition(
    string Id,
    string Name,
    string Type
)
{
    public void Validate(ref List<string> errors)
    {
    }
}

public record PayoutRule(
    string SymbolId,
    int MatchCount,
    int Multiplier
)
{
    public void Validate(ref List<string> errors)
    {
    }
}

public record JackpotSettings(
    bool JackpotEnabled,
    string JackpotType,
    long BasePoolValue,
    double ContributionRate,
    int MinBetToQualify
)
{
    public void Validate(ref List<string> errors)
    {
    }
}

public record FeatureFlags(
    bool FreeSpinsEnabled,
    bool GambleMinigameEnabled,
    bool BuyFeatureAllowed
)
{
    public void Validate(ref List<string> errors)
    {
    }

}

public record LiveOpsCampaign(
    string CampaignId,
    string CampaignName,
    List<string> PlayerSegments,
    CampaignSchedule Schedule,
    CampaignRewards Rewards
)
{
    public void Validate(ref List<string> errors)
    {
    }
}

public record CampaignSchedule(
    DateTime StartTimeUtc,
    DateTime EndTimeUtc
)
{
    public void Validate(ref List<string> errors)
    {
    }
}

public record CampaignRewards(
    int EntryBonusChips,
    double XpMultiplier
)
{
    public void Validate(ref List<string> errors)
    {
    }
}
