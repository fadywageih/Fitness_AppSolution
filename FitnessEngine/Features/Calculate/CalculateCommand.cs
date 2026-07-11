using MediatR;

namespace FitnessEngine.Features.Calculate
{

    public record CalculateCommand : IRequest<CalculateResponse?>;

    public record CalculateResponse(decimal Bmr, decimal Tdee, decimal CalorieTarget, string Status);
}
