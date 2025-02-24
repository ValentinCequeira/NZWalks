using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRolId = "c0c045ba-b4d7-4afd-9363-dda31da3afea";
            var writerRolId = "e96befa4-3406-4089-8418-68e438ae6f56";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRolId,
                    ConcurrencyStamp = readerRolId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRolId,
                    ConcurrencyStamp = writerRolId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                }
            };

            //con el builder llevamos la info al constructor arriba
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
