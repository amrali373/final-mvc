using api.models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    #region Mongodb
    private readonly IMongoCollection<AppUser> _collection;

    // Dependency Injection
    public UserController(IMongoClient client, Settings.IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<AppUser>("users");
    }
    #endregion

    [HttpPost("register")]
    public ActionResult<AppUser> Register(AppUser userInput)
    {
        if (userInput.Password != userInput.ConfirmPassword)
            return BadRequest("Passwords do not match");

        AppUser? user = _collection.Find(appUser => appUser.Email == userInput.Email).FirstOrDefault();

        if (user is not null)
            return BadRequest("Email already exists");

        _collection.InsertOne(userInput);

        return userInput;
    }

    [HttpGet("get-all")]
    public ActionResult<List<AppUser>> GetAll()
    {
        List<AppUser> users = _collection.Find(new BsonDocument()).ToList();

        if (users.Count == 0)
            return NoContent();

        return users;
    }

    [HttpGet("get-by-username/{userInput}")]
    public ActionResult<AppUser> GetByUserName(string userInput)
    {
        AppUser? user = _collection.Find(appUser => appUser.UserName == userInput).FirstOrDefault();

        if (user is null)
            return NotFound("User not found");

        return user;
    }

    [HttpPut("update-by-id/{userId}")]
    public ActionResult<AppUser> UpdateById(string userId, AppUser user)
    {
        AppUser? userToUpdate = _collection.Find(appUser => appUser.Id == userId).FirstOrDefault();

        if (userToUpdate is null)
            return NotFound("User not found");

        UpdateDefinition<AppUser> updatedUser = Builders<AppUser>.Update
            .Set(doc => doc.Email, user.Email);

        _collection.UpdateOne(doc => doc.Id == userId, updatedUser);

        AppUser newUser = _collection.Find(appUser => appUser.Id == userId).FirstOrDefault();

        return newUser;
    }

        [HttpDelete("delete-by-id/{userId}")]
    public ActionResult<DeleteResult> DeleteById(string userId)
    {
        AppUser? user = _collection.Find(appUser => appUser.Id == userId).FirstOrDefault();
        
        if (user is null)
            return NotFound("User not found");

        return _collection.DeleteOne(doc => doc.Id == userId);
    }
}
