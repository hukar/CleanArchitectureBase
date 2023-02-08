global using static Microsoft.AspNetCore.Http.Results;
global using System.Net;


global using API.Endpoints;
global using API.Filters;
global using API.Middlewares;


global using Infrastructure.Persistence.Repositories;
global using Infrastructure.Persistence.Database;

global using Application;
global using Application.Dtos;
global using Application.Interfaces.Repositories;
global using Application.Robots.Queries.GetAllRobots;
global using Application.Robots.Queries.GetRobotById;
global using Application.Robots.Commands.CreateRobot;
global using Application.Robots.Commands.UpdateRobot;
global using Application.Robots.Commands.DeleteRobot;


global using MediatR;
global using FluentValidation;
