namespace Progress_Tracking.Domain.Entities
{

    public class WeightHistory
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Weight { get; private set; }
        public DateTime Date { get; private set; }
        public string? Notes { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private WeightHistory() { }

        private WeightHistory(Guid userId, decimal weight, DateTime date, string? notes)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Weight = weight;
            Date = date;
            Notes = notes;
            CreatedAt = DateTime.UtcNow;
        }

        public static WeightHistory Create(Guid userId, decimal weight, DateTime date, string? notes)
            => new(userId, weight, date, notes);
    }

}
