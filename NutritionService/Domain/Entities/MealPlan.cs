namespace NutritionService.Domain.Entities
{

    public class MealPlan
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int TargetCalorieMin { get; private set; }
        public int TargetCalorieMax { get; private set; }
        public List<MealPlanItem> Items { get; private set; } = new();

        private MealPlan() { }

        public MealPlan(string name, int targetCalorieMin, int targetCalorieMax)
        {
            Id = Guid.NewGuid();
            Name = name; TargetCalorieMin = targetCalorieMin; TargetCalorieMax = targetCalorieMax;
        }
    }
}
