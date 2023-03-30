using Application.Robots.Queries.IsWeaponExists;

namespace API.Endpoints;

public static class RobotEndpointsExtension
{
    public static WebApplication MapRobot(this WebApplication app)
    {
        var route = app.MapGroup("/robots");
                        
        route.MapGet("/", async (ISender sender) => await sender.Send(new GetAllRobotsQuery()));

        route.MapGet("/withweapons", async (ISender sender) 
            => await sender.Send(new GetAllRobotsWithWeaponsQuery())
        );

        route.MapGet("/{id:int}", async (int id, ISender sender) 
            => (await sender.Send(new GetRobotByIdQuery(id))) is GetRobotDto robot ? Ok(robot) : NotFound()
        );

        route.MapGet("/{id:int}/withweapons", async (int id, ISender sender) 
            => (await sender.Send(new GetRobotWithWeaponsByIdQuery(id))) is GetRobotWithWeaponsDto robot ? Ok(robot) : NotFound()
        );

        route.MapGet("/weaponslist", async (ISender sender) 
            => await sender.Send(new GetAllWeaponsQuery())
        );

        route.MapGet("/isweaponexists", async (ISender sender, int id , string name)
            => await sender.Send(new IsWeaponExistsQuery(new GetWeaponDto(id, name)))
        );

        route.MapPost("/", async (CreateUpdateRobotDto robotToCreate, ISender sender) => {
            var robotCreated = await sender.Send(new CreateRobotCommand(robotToCreate));

            return Created($"/robots/{robotCreated.Id}", robotCreated);
        })
            .AddEndpointFilter<ValidationFilter<CreateUpdateRobotDto>>();

        route.MapPut("/{id:int}", async (int id, CreateUpdateRobotDto robotToUpdate, ISender sender) => {
            var rowsAffected = await sender.Send(new UpdateRobotCommand(id, robotToUpdate));

            if(rowsAffected == 0) return NotFound();

            return NoContent();
        })
            .AddEndpointFilter<ValidationFilter<CreateUpdateRobotDto>>();

        route.MapDelete("/{id:int}", async (int id, ISender sender) => {
            var rowsAffected = await sender.Send(new DeleteRobotCommand(id));

            if(rowsAffected == 0) return NotFound();

            return NoContent();
        });

        return app;
    }
}
