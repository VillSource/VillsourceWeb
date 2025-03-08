using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Villsource.Server.Controllers;

[Route("api")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet("name")]
    public IActionResult GetName()
    {
        return Ok(new
        {
            value = "Villsource"
        });
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
        return Ok(new
        {
            value = "3.0.0"
        });
    }

    [HttpGet("list")]
    public IActionResult GetList()
    {
        using var db = new AppDbContext();
        db.Database.EnsureCreated();
        var list = db.Lists.ToList();
        return Ok(list);
    }

    [HttpPost("list")]
    public IActionResult PostList(string txt)
    {
        using var db = new AppDbContext();
        db.Database.EnsureCreated();
        long id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
        db.Lists.Add(new() { Id=id, Content = txt});
        var ack = db.SaveChanges();
        return Ok(ack);
    }

    [HttpDelete("list")]
    public IActionResult DeleteList(string id)
    {
        using var db = new AppDbContext();
        db.Database.EnsureCreated();

        if (id == "**")
        {
            db.Lists.ExecuteDelete();
            db.SaveChanges();
            return NoContent();
        }

        if (!long.TryParse(id, out var idl))
            return BadRequest();

        var list = db.Lists.Find(idl);
        if (list is null)
            return NotFound();

        db.Lists.Remove(list);
        db.SaveChanges();
        return NoContent();
    }
}
