﻿using CinemAPI.Models.Contracts.Projection;
using System;

namespace CinemAPI.Models
{
    public class Projection : IProjection, IProjectionCreation
    {
        private int availableSeatsCount;

        public Projection()
        {
        }

        public Projection(int movieId, int roomId, DateTime startdate)
        {
            this.MovieId = movieId;
            this.RoomId = roomId;
            this.StartDate = startdate;
        }

        public Projection(int movieId, int roomId, DateTime startdate, int availableSeatsCount): this()
        {
            this.MovieId = movieId;
            this.RoomId = roomId;
            this.StartDate = startdate;
            this.AvailableSeatsCount = availableSeatsCount;
        }

        public long Id { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public DateTime StartDate { get; set; }

        public int AvailableSeatsCount 
        {
            get => availableSeatsCount;
            set
            {
                if (value >= 0)
                {
                    availableSeatsCount = value;
                }
                else
                {
                    availableSeatsCount = 0;

                    //or another way:
                    //throw new ArgumentOutOfRangeException("availableSeatsCount", "The property can not accept negative values!");
                }
            }
        }
    }
}