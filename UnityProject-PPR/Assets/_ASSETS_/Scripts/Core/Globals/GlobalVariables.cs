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

public static class Fade
{
    public static readonly float CANVAS_FADE_TIME = 0.2f;
    public static readonly float SELECTION_FADE_TIME = 0.6f;
}


/// <summary>
/// 아이템 가격 관련
/// </summary>
public static class ItemPrice
{
    public static readonly int PRICE_MIN_HELMET = 35;
    public static readonly int PRICE_MAX_HELMET = 46;

    public static readonly int PRICE_MIN_ARMOR = 44;
    public static readonly int PRICE_MAX_ARMOR = 57;

    public static readonly int PRICE_MIN_WEAPON = 53;
    public static readonly int PRICE_MAX_WEAPON = 68;

    public static readonly int PRICE_MIN_TRINKET = 60;
    public static readonly int PRICE_MAX_TRINKET = 81;

    public static readonly int PRICE_MIN_POTION = 21;
    public static readonly int PRICE_MAX_POTION = 31;

    public static readonly float PRICE_WEIGHT_UNCOMMON = 1.5f;
    public static readonly float PRICE_WEIGHT_RARE = 2.0f;

    public static readonly float PRICE_SALE_SMALL = 0.75f;
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