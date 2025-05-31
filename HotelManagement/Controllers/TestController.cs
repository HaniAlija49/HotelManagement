using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("divide-by-zero")]
    public IActionResult DivideByZero()
    {
        int divisor = 0;
        var result = 1 / divisor; // This will throw DivideByZeroException at runtime
        return Ok(result);
    }

    [HttpGet("null-reference")]
    public IActionResult NullReference()
    {
        string? text = null;
        return Ok(text!.Length); // This will throw NullReferenceException
    }

    [HttpGet("custom-exception")]
    public IActionResult CustomException()
    {
        throw new ApplicationException("This is a custom exception for testing");
    }

    [HttpGet("not-found")]
    public IActionResult NotFound()
    {
        throw new KeyNotFoundException("The requested resource was not found");
    }
} 