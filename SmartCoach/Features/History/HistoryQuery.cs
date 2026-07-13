using MediatR;

namespace SmartCoach.Features.History
{

    public record HistoryQuery(Guid? SessionId, int Page = 1, int PageSize = 20) : IRequest<object>;
}
