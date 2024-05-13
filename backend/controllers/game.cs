using backend.components.middleware;
using backend.components.pagination;
using backend.models;
using backend.services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace backend.controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService service) =>
        _gameService = service;

    [HttpGet]
    public Pagination<Game> Get([FromQuery] GameRequest req) =>
        _gameService.GetList(req);

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Game>> Get(string id)
    {
        var game = await _gameService.GetAsync(id);

        if (game is null)
        {
            return NotFound();
        }

        return game;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    [Route("studios")]
    public OkObjectResult Studios()
    {
        return Ok(_gameService.GetStudios());
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
    [Authorization(UserType.Admin)]
    [HttpPost]
    public async Task<IActionResult> Post(CreateGame request)
    {
        Game newGame = await _gameService.CreateAsync(request);

        return CreatedAtAction(nameof(Get), new { id = newGame.Id }, newGame);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorization(UserType.Admin)]
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, UpdateGame updatedGame)
    {
        if (id.Length < 24)
        {
            return BadRequest();
        }

        await _gameService.UpdateAsync(id, updatedGame);
        var game = await _gameService.GetAsync(id);
        if (game == null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorization(UserType.Admin)]
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var game = await _gameService.GetAsync(id);

        if (game is null)
        {
            return NotFound();
        }

        await _gameService.RemoveAsync(id);

        return NoContent();
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    [Authorization(UserType.Admin)]
    [Route("bunch-insert")]
    public async Task<IActionResult> BunchInsert(IFormFile file)
    {
        string fileContents;
        using (var stream = file.OpenReadStream())
        using (var reader = new StreamReader(stream))
        {
            fileContents = await reader.ReadToEndAsync();
        }

        try
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            Game[] games = JsonConvert.DeserializeObject<Game[]>(fileContents, settings) ??
                           Array.Empty<Game>();
            _gameService.BunchInsert(games);
        }
        catch (FormatException fex)
        {
            Console.WriteLine(fex);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }


        return Ok();
    }
}