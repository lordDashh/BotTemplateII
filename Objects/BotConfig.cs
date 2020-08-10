namespace BotTemplate.Objects
{
    /// <summary>
    ///   DataStructure for the config.json file
    /// </summary>
    public class BotConfig
    {
        public string Token { get; set; }
        public string[] Prefixes { get; set; }
    }
}
