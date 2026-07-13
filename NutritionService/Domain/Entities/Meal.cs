namespace NutritionService.Domain.Entities
{

    public class Meal
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Type { get; private set; } = string.Empty;
        public int Calories { get; private set; }
        public int Protein { get; private set; }
        public int Carbs { get; private set; }
        public int Fat { get; private set; }
        public string Ingredients { get; private set; } = string.Empty;
        public string Instructions { get; private set; } = string.Empty;
        public string? Allergens { get; private set; }
        public string? Tags { get; private set; }

        private Meal() { }

        public Meal(string name, string type, int calories, int protein, int carbs, int fat, string ingredients, string instructions, string? allergens = null, string? tags = null)
        {
            Id = Guid.NewGuid();
            Name = name; Type = type; Calories = calories; Protein = protein; Carbs = carbs; Fat = fat;
            Ingredients = ingredients; Instructions = instructions; Allergens = allergens; Tags = tags;
        }
    }
}
