using DeepChecks.Data;
using DeepChecks.Data.Entities;
using DeepChecks.Models.Participant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Service
{
    public class ParticipantService
    {
        private readonly Guid _userId;

        public ParticipantService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateParticipant(ParticipantCreate model)
        {
            var entity =
                new Participant()
                {
                    OwnerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Participants.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ParticipantListItem> GetParticipants()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Participants
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ParticipantListItem
                                {
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    Email = e.Email
                                });
                return query.ToArray();
            }
        }
    }
}
