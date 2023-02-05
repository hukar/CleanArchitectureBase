namespace API.Endpoints;

public static class RobotEndpointExtension
{
    public static WebApplication MapRobot(this WebApplication app)
    {
        var route = app.MapGroup("/robots");
                        
        route.MapGet("/", async (ISender sender) => await sender.Send(new GetAllRobotsQuery()));

        route.MapGet("/{id:int}", async (int id, ISender sender) =>
        {
            var robot = await sender.Send(new GetRobotByIdQuery(id));

            if (robot is null) return NotFound();

            return Ok(robot);
        });

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
