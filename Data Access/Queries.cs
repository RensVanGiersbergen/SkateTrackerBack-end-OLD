﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.data;
using Data_Access.models;
using Microsoft.EntityFrameworkCore;
using Interface_Layer;
using Interface_Layer.dto;
using Data_Access;


namespace Data_Access
{
    public class Queries : IPositionCollectionDAL, IJourneyCollectionDAL
    {

        SkateTrackerContext context;
        public Queries()
        {
            context = new SkateTrackerContext(new DbContextOptionsBuilder<SkateTrackerContext>().UseSqlServer(Configuration.ConnectionString).Options);
        }

        public Queries(string name)
        {
            context = new SkateTrackerContext(new DbContextOptionsBuilder<SkateTrackerContext>().UseInMemoryDatabase<SkateTrackerContext>(databaseName: name).Options);
        }

        public void AddPosition(DTOPosition dto)
        {
            Position position = new Position()
            {
                JourneyID = dto.JourneyID,
                Latitude = dto.Latitude,
                Longtitude = dto.Longitude,
                Speed = dto.Speed,
                TimeStamp = dto.TimeStamp,
            };
            context.Add(position);
            context.SaveChanges();
        }

        public int AddJourney(DTOJourney dto)
        {
            Journey journey = new Journey()
            {
                Id = dto.Id,
                Name = dto.Name,
                StartTime = dto.StartTime,
                MaxSpeed = dto.MaxSpeed,
                TotalTime = dto.TotalTime,
                RideTime = dto.RideTime,
                PauseTime = dto.PauseTime
            };
            context.Add(journey);
            context.SaveChanges();

            return journey.Id;
        }

        public List<DTOPosition> GetPositionsByJourney(int ID)
        {
            List<DTOPosition> positions = new List<DTOPosition>();
            foreach (Position position in context.Position.ToList().Where(x => x.JourneyID == ID))
            {
                positions.Add(new DTOPosition()
                {
                    JourneyID = position.JourneyID,
                    Latitude = position.Latitude,
                    Longitude = position.Longtitude,
                    Speed = position.Speed,
                    TimeStamp = position.TimeStamp
                });
            }
            return positions;
        }

        public List<DTOJourney> GetJourneysBySkater(int SkaterID)
        {
            List<DTOJourney> journeys = new List<DTOJourney>();
            foreach (Journey journey in context.Journey.ToList().Where(x => x.SkaterId == SkaterID))
            {
                journeys.Add(new DTOJourney()
                {
                    Id = journey.Id,
                    Name = journey.Name,
                    MaxSpeed = journey.MaxSpeed,
                    StartTime = journey.StartTime,
                    PauseTime = journey.PauseTime,
                    RideTime = journey.RideTime,
                    TotalTime = journey.TotalTime
                });
            }
            return journeys;
        }
    }
}
