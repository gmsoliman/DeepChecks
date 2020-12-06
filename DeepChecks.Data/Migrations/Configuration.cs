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
                new Entities.Category() { CategoryId = 1, CategoryTitle = "Things I'm Doing Well", CategoryDescription = "For this category, ask yourself, what are some things I'm doing well in this relationship? How am I making an effort to meet the needs of the other person? Have I taken feedback from previous checks and worked to meet those asks? You add things that you're doing well that may not directly affect the relationship, such as excelling at work or advocating for yourself more, but also try to include a few things that are pertinent to the relationship."},
                new Entities.Category() { CategoryId = 2, CategoryTitle = "Things You're Doing Well", CategoryDescription = "For this category, think of things that the other person is doing well in the relationship. How have they worked to meet your needs? Have they made an effort to improve themselves or how they show up in the relationship? Maybe they've always done this thing really well, and you just want to acknowledge it again. This category is just as important as the others; it's an opportunity to validate the efforts that both of you have made to be better for each other!"},
                new Entities.Category() { CategoryId = 3, CategoryTitle = "Things I Need More Of", CategoryDescription = "This category doesn't just have to be a list of things the other person isn't doing. In fact, it shouldn't be! Take this time to point out things that the other person has been doing well in the relationship that you would love to continue seeing. Or, you can take this time to ask for things that you know you'll need in the future. Maybe, you plan on starting a new job, and you'd like the other person to check in with you to see how things are going once you embark on that journey. If there are things that you've asked for in the past, and the other person hasn't been able to provide that for your, this can be an opportunity to bring it up again. But, do so softly. This is not a moment for you to be calling each other out, rather you're calling each other in and bringing attention to your needs. Once the other person has told you their asks, make sure to reflect it back to them. Confirm what they need from you by trying to adhere as closely as possible to their words without adding in or spinning it with some of your own."}
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
