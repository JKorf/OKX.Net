namespace OKX.Net.Objects.Public;
/// <summary>
/// Announcement type
/// </summary>
[SerializationModel]
public record OKXAnnouncementType
{
    /// <summary>
    /// ["<c>annType</c>"] Announcement type
    /// </summary>
    [JsonPropertyName("annType")]
    public string AnnouncementType { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>annTypeDesc</c>"] Announcement type description
    /// </summary>
    [JsonPropertyName("annTypeDesc")]
    public string Description { get; set; } = string.Empty;
}


