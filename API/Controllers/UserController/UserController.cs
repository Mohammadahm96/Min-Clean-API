﻿using Application.Commands.Users.Login;
using Application.Commands.Users.RegisterUser;
using Application.Dtos;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserDto newUser)
    {
        try
        {
            var result = await _mediator.Send(new RegisterUserCommand(newUser));
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DuplicateUserException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserDto loginUser)
    {
        try
        {
            // Send the login command to MediatR
            var token = await _mediator.Send(new LoginUserCommand(loginUser));

            // Return the token or handle the login success accordingly
            return Ok(new { Token = token });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidCredentialsException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }
}
