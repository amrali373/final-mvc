using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.models;

public record AppUser(
        [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
        [EmailAddress, MaxLength(50)]
        string Email,
        [ MaxLength(50)]
        string UserName,
        [Length(8,16)]
        string Password,
        string ConfirmPassword
);
