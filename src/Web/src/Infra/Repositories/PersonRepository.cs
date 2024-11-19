


//using Microsoft.EntityFrameworkCore;
//using VozAmiga.Api.Data.Database;
//using VozAmiga.Core.Data.Model;
//using VozAmiga.Core.Data.Repositories;

//namespace VozAmiga.Api.Infra.Repositories;


//public class PersonRepository : IPersonRepository
//{
//    private readonly IDbContext _context;

//    public PersonRepository(IDbContext context)
//    {
//        _context = context;
//    }

//    /// <inheritdoc />
//    public async Task<Person?> FindByEmail(string email, CancellationToken cancellationToken = default)
//    {
//        return await _context.Set<Person>()
//            .Where(p => p.Email == email)
//            .FirstOrDefaultAsync(cancellationToken);
//    }
//}
