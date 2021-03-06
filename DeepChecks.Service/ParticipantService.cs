﻿using DeepChecks.Data;
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
                    CheckId = model.CheckId
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
                                    ParticipantId = e.ParticipantId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    CheckId = e.CheckId
                                });
                return query.ToArray();
            }
        }

        public ParticipantListItem GetParticipantById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Participants
                        .Single(e => e.ParticipantId == id && e.OwnerId == _userId);
                    return
                        new ParticipantListItem
                        {
                            ParticipantId = entity.ParticipantId,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            CheckId = entity.CheckId
                        };
            }
        }

        public IEnumerable<ParticipantListItem> GetParticipantByCheck(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Participants
                        .Where(e => e.CheckId == id && e.OwnerId == _userId)
                        .Select(
                            e => 
                                new ParticipantListItem
                                {
                                    ParticipantId = e.ParticipantId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    CheckId = e.CheckId
                                });
                return query.ToArray();
            }
        }

        public bool UpdateParticipant(ParticipantListItem model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Participants
                        .Single(e => e.ParticipantId == model.ParticipantId && e.OwnerId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.CheckId = model.CheckId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteParticipant(int participantId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Participants
                        .Single(e => e.ParticipantId == participantId && e.OwnerId == _userId);

                ctx.Participants.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
