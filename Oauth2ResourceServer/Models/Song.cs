using System;

namespace Oauth2ResourceServer.Models
{
    public class Song
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Singer { get; set; }
        public string Author { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
        public long MemberId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public SongStatus Status { get; set; }

        public Song()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = SongStatus.Available;
        }
    }

    public enum SongStatus
    {
        Available = 1,
        Deleted = 0
    }
}
