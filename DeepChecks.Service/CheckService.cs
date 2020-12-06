using DeepChecks.Data;
using DeepChecks.Data.Entities;
using DeepChecks.Models.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Service
{
    public class CheckService
    {
        private readonly Guid _userId;

        public CheckService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCheck(CheckCreate model)
        {
            var entity =
                new Check()
                {
                    OwnerId = _userId,
                    CheckTitle = model.CheckTitle,
                    CheckDate = model.CheckDate,
                    RelationshipId = model.RelationshipId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Checks.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CheckListItem> GetChecks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Checks
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CheckListItem
                                {
                                    CheckId = e.CheckId,
                                    CheckTitle = e.CheckTitle,
                                    CheckDate = e.CheckDate,
                                    RelationshipId = e.RelationshipId
                                });
                return query.ToArray();
            }
        }

        public CheckListItem GetCheckById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Checks
                        .Single(e => e.CheckId == id && e.OwnerId == _userId);
                return
                    new CheckListItem
                    {
                        CheckId = entity.CheckId,
                        CheckTitle = entity.CheckTitle,
                        CheckDate = entity.CheckDate,
                        RelationshipId = entity.RelationshipId
                    };
            }
        }

        public IEnumerable<CheckListItem> GetCheckByRelationship (int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Checks
                        .Where(e => e.RelationshipId == id && e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CheckListItem
                                {
                                    CheckId = e.CheckId,
                                    CheckTitle = e.CheckTitle,
                                    CheckDate = e.CheckDate,
                                    RelationshipId = e.RelationshipId
                                });
                return query.ToArray();
            }
        }

        public bool UpdateCheck(CheckListItem model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Checks
                        .Single(e => e.CheckId == model.CheckId && e.OwnerId == _userId);

                entity.CheckTitle = model.CheckTitle;
                entity.CheckDate = model.CheckDate;
                entity.RelationshipId = model.RelationshipId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCheck(int checkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Checks
                        .Single(e => e.CheckId == checkId && e.OwnerId == _userId);

                ctx.Checks.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
