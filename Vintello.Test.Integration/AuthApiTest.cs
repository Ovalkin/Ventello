using Microsoft.EntityFrameworkCore;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration;

public class AuthApiTest
{
    private readonly HttpClient _client;
    private readonly VintelloContext _context;

    public AuthApiTest()
    {
    }
}