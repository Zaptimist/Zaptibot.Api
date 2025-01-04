using Microsoft.AspNetCore.Mvc;
using Zaptibot.Api.Sounds.Handlers;
using Zaptibot.Api.Sounds.Models.Entity;

namespace Zaptibot.Api.Sounds;

[ApiController]
[Route("[controller]")]
public class SoundsController(
    IGetSoundHandler getSoundHandler, 
    IAddSoundHandler addSoundHandler) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        Sound? result = await getSoundHandler.GetAsync(id);
    
        return Ok(result);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await getSoundHandler.GetAllAsync());
    }
    
    
    // TODO: Implement real file add 
    [HttpPost]
    public async Task<IActionResult> AddSoundAsync(string name)
    {
        await addSoundHandler.HandleAsync(name);

        return Ok();
    }
    //
    // [HttpDelete("{id:int}")]
    // public async Task<IActionResult> DeleteSoundAsync(int id)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [HttpPut("{name}")]
    // public async Task<IActionResult> UpdateSoundAsync(string name)
    // {
    //     throw new NotImplementedException();
    // }
}