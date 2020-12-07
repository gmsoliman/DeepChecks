using DeepChecks.Data;
using DeepChecks.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Service
{
    public class CategoryService
    {
        private readonly Guid _userId;
        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Select(
                            e => 
                                new CategoryListItem
                                {
                                    CategoryId = e.CategoryId,
                                    CategoryTitle = e.CategoryTitle
                                });

                return query.ToArray();
            }
        }
        public CategoryDetail GetCategoryById(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.SingleOrDefault(e => e.CategoryId == categoryId);

                return
                    new CategoryDetail
                    {
                        CategoryId = entity.CategoryId,
                        CategoryTitle = entity.CategoryTitle,
                        CategoryDescription = entity.CategoryDescription
                    };
            }
        }
    }
}
