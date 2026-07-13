namespace NutritionService.Domain.Entities
{

    public class MealPlanItem
    {
        public Guid Id { get; private set; }
        public Guid MealPlanId { get; private set; }
        public Guid MealId { get; private set; }
        public string DayOfWeek { get; private set; } = string.Empty;
        public string MealTime { get; private set; } = string.Empty;

        public MealPlan MealPlan { get; private set; } = null!;
        public Meal Meal { get; private set; } = null!;

        private MealPlanItem() { }

        public MealPlanItem(Guid mealPlanId, Guid mealId, string dayOfWeek, string mealTime)
        {
            Id = Guid.NewGuid();
            MealPlanId = mealPlanId; MealId = mealId; DayOfWeek = dayOfWeek; MealTime = mealTime;
        }
    }
}
