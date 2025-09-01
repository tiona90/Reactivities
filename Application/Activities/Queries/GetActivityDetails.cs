using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries;

public class GetActivityDetails
{
    public class Query : IRequest<Activity>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Activity>
    {
        public Task<Activity> Handle(Query request, CancellationToken cancellationToken)
        {
            return Handle(request, context, cancellationToken);
        }

        public async Task<Activity> Handle(Query request, AppDbContext context, CancellationToken cancellationToken)
        {
            var acivity = await context.Activities.FindAsync([request.Id], cancellationToken);
            if (acivity == null) throw new Exception("Activity not found");
            return acivity;
        }
    }
}