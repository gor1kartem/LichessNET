using LichessNET.Entities.Game;

namespace LichessNET.API.Converter;

public class GameConverters
{
    public static Game ConvertGame(dynamic game)
    {
        Game gameobject = new Game();

        gameobject.Id = game.id;
        gameobject.Rated = game.rated;
        gameobject.Variant = game.variant;
        gameobject.Speed = game.speed;
        gameobject.Perf = game.perf;
        gameobject.CreatedAt = game.createdAt;
        gameobject.LastMoveAt = game.lastMoveAt;
        gameobject.Moves = game.turns;
        gameobject.Status = game.status;
                
        gameobject.White = new Player();
        gameobject.Black = new Player();
        gameobject.Clock = new Clock();
        gameobject.Opening = new Opening();
                
        gameobject.White.Rating = game.players.white.rating;
        gameobject.White.RatingDiff = game.players.white.ratingDiff;
        gameobject.White.Id = game.players.white.userId;
        gameobject.White.Provisional = game.players.white.provisional;
                
        gameobject.Black.Rating = game.players.black.rating;
        gameobject.Black.RatingDiff = game.players.black.ratingDiff;
        gameobject.Black.Id = game.players.black.userId;
        gameobject.Black.Provisional = game.players.black.provisional;
        
        gameobject.Clock = game.clock.ToObject<Clock>();
        try
        {
            gameobject.Opening = game.opening.ToObject<Opening?>();
        }
        catch
        {

        }


        return gameobject;
    }
}