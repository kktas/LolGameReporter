namespace Core.Models.ModelBase
{
    public interface IModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? DeletedById { get; set; }
        public string? DeletedByName { get; set; }
    }
}
