namespace Core.Models.ModelBase
{
    public class Model : IModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public long CreatedById { get; set; } = 0;
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; } = null;
        public long? DeletedById { get; set; }
        public string? DeletedByName { get; set; }
    }
}
