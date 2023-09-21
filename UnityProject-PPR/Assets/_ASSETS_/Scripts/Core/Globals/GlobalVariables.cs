/// <summary>
/// Locale Name 관련
/// </summary>
public static class LocaleNames
{
    public static readonly string Korean = "ko";
    public static readonly string English = "en";
}

/// <summary>
/// Scene Name 관련
/// </summary>
public static class SceneNames
{
    public static readonly string Lobby = "Lobby";
    public static readonly string Stage = "Stage";
    public static readonly string Battle = "Battle";
    public static readonly string Mystery = "Mystery";
    public static readonly string RestSite = "RestSite";
    public static readonly string Treasure = "Treasure";
    public static readonly string Shop = "Shop";
}

public static class AudioNames
{
    public static readonly string BGM = "BGM Volume";
    public static readonly string SFX = "SFX Volume";
}

/// <summary>
/// 스테이지 타입 별 이미지 Naming
/// </summary>
public static class StageImageNames
{
    public static readonly string Case_NotAccess = "Sprites_21";
    public static readonly string Case_Access = "Sprites_17";
    public static readonly string Case_Clear = "Sprites_22";

    public static readonly string Battle = "All-Icons_82";
    public static readonly string Boss = "All-Icons_18";
    public static readonly string Event = "All-Icons_4";
    public static readonly string Shop = "All-Icons_97";

    public static readonly string Clear_Battle = "All-Icons_90";
    public static readonly string Clear_Boss = "All-Icons_26";
    public static readonly string Clear_Event = "All-Icons_10";
    public static readonly string Clear_Shop = "All-Icons_105";
}

public static class DUR
{
    public static readonly float CANVAS_FADE_TIME = 0.2f;
    public static readonly float TYPING_SPEED = 0.03f;
    public static readonly float SELECTION_FADE_TIME = 0.6f;
}

/// <summary>
/// 가격 관련
/// </summary>
public static class CASH
{
    public static readonly int REWARD_MIN_MINOR = 30;
    public static readonly int REWARD_MAX_MINOR = 42;
    public static readonly int REWARD_MIN_ELITE = 60;
    public static readonly int REWARD_MAX_ELITE = 72;
    public static readonly int REWARD_MIN_BOSS = 100;
    public static readonly int REWARD_MAX_BOSS = 122;

    public static readonly int PRICE_MIN_RELIC_COMMON = 143;
    public static readonly int PRICE_MAX_RELIC_COMMON = 157;
    public static readonly int PRICE_MIN_RELIC_UNCOMMON = 238;
    public static readonly int PRICE_MAX_RELIC_UNCOMMON = 262;
    public static readonly int PRICE_MIN_RELIC_RARE = 285;
    public static readonly int PRICE_MAX_RELIC_RARE = 315;

    public static readonly int PRICE_MIN_POTION_COMMON = 48;
    public static readonly int PRICE_MAX_POTION_COMMON = 52;
    public static readonly int PRICE_MIN_POTION_UNCOMMON = 72;
    public static readonly int PRICE_MAX_POTION_UNCOMMON = 78;
    public static readonly int PRICE_MIN_POTION_RARE = 95;
    public static readonly int PRICE_MAX_POTION_RARE = 105;

    public static readonly int PRICE_MIN_CARD_COMMON = 45;
    public static readonly int PRICE_MAX_CARD_COMMON = 55;
    public static readonly int PRICE_MIN_CARD_UNCOMMON = 68;
    public static readonly int PRICE_MAX_CARD_UNCOMMON = 82;
    public static readonly int PRICE_MIN_CARD_RARE = 135;
    public static readonly int PRICE_MAX_CARD_RARE = 165;

    public static readonly float PRICE_SALE_BIG = 0.5f;
}

public static class DataKey
{
    public static class Settings
    {
        public static readonly string BGM = "SETTINGS_BGM";
        public static readonly string SFX = "SETTINGS_SFX";
        public static readonly string FPS = "SETTINGS_FPS";
        public static readonly string LANGUAGE = "SETTINGS_LANGUAGE";
    }
}