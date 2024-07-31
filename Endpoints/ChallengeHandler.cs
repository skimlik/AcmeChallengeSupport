using System.Linq;
using AcmeChallengeSupport.Host.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace AcmeChallengeSupport.Host.Endpoints;

public static class ChallengeHandler
{
    public static RouteHandlerBuilder AddChallengeHandler(this WebApplication app) =>
        app.MapGet("/.well-known/acme-challenge/{id}", ([FromRoute] string id, [FromServices] DbUnit data) => {
                var challenge = data.ChallengeMaps.FirstOrDefault(e => e.Key == id);
                if (challenge is not null)
                {
                    return challenge?.Value;
                }

                return null;
            })
            .WithName("AcmeChallenge")
            .WithOpenApi();
}