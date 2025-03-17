using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Net.Objects.Public
{
    /// <summary>
    /// Announcements page
    /// </summary>
    [SerializationModel]
    public record OKXAnnouncementsPage
    {
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("details")]
        public OKXAnnouncement[] Details { get; set; } = Array.Empty<OKXAnnouncement>();
        /// <summary>
        /// Total number of pages
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
        /// Announcement type
        /// </summary>
        [JsonPropertyName("annType")]
        public string? AnnouncementType { get; set; }
        /// <summary>
        /// Publish time
        /// </summary>
        [JsonPropertyName("pTime")]
        public DateTime PublishTime { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }


}
