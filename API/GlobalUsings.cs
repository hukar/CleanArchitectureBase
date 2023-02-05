global using static Microsoft.AspNetCore.Http.Results;

global using API.Endpoints;
global using API.Filters;

global using Infrastructure.Persistence.Repositories;

global using Application;
global using Application.Dtos;
global using Application.Interfaces.Repositories;
global using Application.Robots.Queries.GetAllRobots;
global using Application.Robots.Queries.GetRobotById;
global using Application.Robots.Commands.CreateRobot;
global using Application.Robots.Commands.UpdateRobot;

global using MediatR;
global using FluentValidation;
