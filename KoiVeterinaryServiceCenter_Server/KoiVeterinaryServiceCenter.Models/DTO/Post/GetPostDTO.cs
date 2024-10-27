namespace KoiVeterinaryServiceCenter.Models.DTO.Post
{
    public class GetPostDTO
    {
       
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PostUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
    
}
}
