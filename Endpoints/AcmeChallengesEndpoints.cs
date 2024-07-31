using System.Linq;
using AcmeChallengeSupport.Host.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace AcmeChallengeSupport.Host.Endpoints;

public static class AcmeChallengesEndpoints
{
    public static RouteHandlerBuilder AddChallengeMapsGet(this WebApplication app) =>
        app.MapGet("/acme-challenges", ([FromServices] DbUnit data, [FromQuery] int? top, [FromQuery] int? skip) => 
            {
                var take = top ?? 100;
                var after = skip ?? 0;

                return data.ChallengeMaps.OrderBy(k => k.Key).Skip(after).Take(take);
            })
            .WithName("AcmeChallengesList")
            .WithOpenApi();
    
    public static RouteHandlerBuilder AddChallengeMapsPost(this WebApplication app) =>
        app.MapPost("/acme-challenges/{key}", ([FromServices] DbUnit data, [FromRoute] string key, [FromBody] string value) =>
            {
                var entity = new ChallengeMap
                {
                    Key = key,
                    Value = value
                };

                data.Add(entity);
                data.SaveChanges();
                return entity;
            })
            .WithName("AcmeChallengeCreate")
            .WithOpenApi();
    
    public static RouteHandlerBuilder AddChallengeMapsDelete(this WebApplication app) =>
        app.MapDelete("/acme-challenges/{key}", ([FromServices] DbUnit data, [FromRoute] string key) =>
            {
                var map = data.ChallengeMaps.FirstOrDefault(e => e.Key == key);
                if (map is null)
                {
                    return null;
                }

                data.Remove(map);
                data.SaveChanges();
                return map;
            })
            .WithName("AcmeChallengeDelete")
            .WithOpenApi();
}