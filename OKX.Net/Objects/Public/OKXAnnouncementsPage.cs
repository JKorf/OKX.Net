namespace OKX.Net.Objects.Public
{
    /// <summary>
    /// Announcements page
    /// </summary>
    [SerializationModel]
    public record OKXAnnouncementsPage
    {
        /// <summary>
        /// ["<c>details</c>"] Details
        /// </summary>
        [JsonPropertyName("details")]
        public OKXAnnouncement[] Details { get; set; } = Array.Empty<OKXAnnouncement>();
        /// <summary>
        /// ["<c>totalPage</c>"] Total number of pages
        /// </summary>
        [JsonPropertyName("totalPage")]
        public decimal TotalPage { get; set; }
    }

    /// <summary>
    /// Announcement
    /// </summary>
    [SerializationModel]
    public record OKXAnnouncement
    {
        /// <summary>
        /// ["<c>annType</c>"] Announcement type
        /// </summary>
        [JsonPropertyName("annType")]
        public string? AnnouncementType { get; set; }
        /// <summary>
        /// ["<c>pTime</c>"] Publish time
        /// </summary>
        [JsonPropertyName("pTime")]
        public DateTime PublishTime { get; set; }
        /// <summary>
        /// ["<c>title</c>"] Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>url</c>"] Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }


}
