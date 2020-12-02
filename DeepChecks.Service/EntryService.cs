using DeepChecks.Data;
using DeepChecks.Data.Entities;
using DeepChecks.Models.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Service
{
    public class EntryService
    {
        private readonly Guid _userId;

        public EntryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateEntry(EntryCreate model)
        {
            var entity =
                new Entry()
                {
                    OwnerId = _userId,
                    EntryContent = model.EntryContent,
                    CategoryId = model.CategoryId,
                    ParticipantId = model.ParticipantId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Entries.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<EntryListItem> GetEntries()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Entries
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new EntryListItem
                                {
                                    EntryId = e.EntryId,
                                    EntryContent = e.EntryContent,
                                });
                return query.ToArray();
            }
        }

        public EntryDetail GetEntryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Entries
                        .Single(e => e.EntryId == id && e.OwnerId == _userId);
                return
                    new EntryDetail
                    {
                        EntryId = entity.EntryId,
                        EntryContent = entity.EntryContent,
                        CategoryId = entity.CategoryId,
                        ParticipantId = entity.ParticipantId
                    };
            }
        }

        public bool UpdateEntry(EntryDetail model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Entries
                        .Single(e => e.EntryId == model.EntryId && e.OwnerId == _userId);

                entity.EntryContent = model.EntryContent;
                entity.CategoryId = model.CategoryId;
                entity.ParticipantId = model.ParticipantId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteEntry(int entryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Entries
                        .Single(e => e.EntryId == entryId && e.OwnerId == _userId);

                ctx.Entries.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
