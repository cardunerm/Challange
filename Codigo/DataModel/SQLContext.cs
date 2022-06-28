using Microsoft.EntityFrameworkCore;

namespace DesafioLaNacion.DataModel
{
    public class SQLContext : DbContext
    {
        public SQLContext() : base()
        {
        }

        public SQLContext(DbContextOptions<SQLContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=ChallengeLaNacion;user=root;password=root", ServerVersion.AutoDetect("server=localhost;port=3306;database=ChallengeLaNacion;user=root;password=root"));
        }
        public virtual DbSet<ContactRecord> ContactRecords { get; set; }



    }


}

