using MediatR;

namespace Progress_Tracking.Features.LogWeight
{

    public record LogWeightCommand(decimal Weight, DateTime Date, string? Notes) : IRequest<LogWeightResponse?>;

    public record LogWeightResponse(Guid Id, decimal Weight, decimal Bmi, decimal? DifferenceFromPrevious);

}
