namespace DeepChecks.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DeepChecks.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DeepChecks.Data.ApplicationDbContext";
        }

        protected override void Seed(DeepChecks.Data.ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(x => x.CategoryId,
                new Entities.Category() { CategoryId = 1, CategoryTitle = "Things I'm Doing Well", CategoryDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." },
                new Entities.Category() { CategoryId = 2, CategoryTitle = "Things They Are Doing Well", CategoryDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." },
                new Entities.Category() { CategoryId = 3, CategoryTitle = "Things I Need More Of", CategoryDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." }
                );
        }
    }
}
