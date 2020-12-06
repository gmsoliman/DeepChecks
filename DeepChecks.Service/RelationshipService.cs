using DeepChecks.Data;
using DeepChecks.Data.Entities;
using DeepChecks.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Service
{
    public class RelationshipService
    {
        private readonly Guid _userId;

        public RelationshipService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRelationship(RelationshipCreate model)
        {
            var entity =
                new Relationship()
                {
                    OwnerId = _userId,
                    RelationshipName = model.RelationshipName
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Relationships.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //public bool AddParticipantToRelationship(int participantId, int relationshipId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var relationship = ctx.Relationships.Single(e => e.RelationshipId == relationshipId);
        //        var participant = ctx.Participants.Single(e => e.ParticipantId == participantId);

        //        relationship.Participants.Add(participant);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        public IEnumerable<RelationshipListItem> GetRelationships()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Relationships
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new RelationshipListItem
                                {
                                    RelationshipId = e.RelationshipId,
                                    RelationshipName = e.RelationshipName
                                });
                return query.ToArray();
            }
        }

        public RelationshipListItem GetRelationshipById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Relationships
                        .Single(e => e.RelationshipId == id && e.OwnerId == _userId);
                return
                    new RelationshipListItem
                    {
                        RelationshipId = entity.RelationshipId,
                        RelationshipName = entity.RelationshipName,
                    };
            }
        }

        public bool UpdateRelationship(RelationshipListItem model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Relationships
                        .Single(e => e.RelationshipId == model.RelationshipId && e.OwnerId == _userId);

                entity.RelationshipName = model.RelationshipName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRelationship(int relationshipId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Relationships
                        .Single(e => e.RelationshipId == relationshipId && e.OwnerId == _userId);

                ctx.Relationships.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

