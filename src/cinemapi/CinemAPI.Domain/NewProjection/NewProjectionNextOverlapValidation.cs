﻿using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionNextOverlapValidation : INewProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly IMovieRepository movieRepo;
        private readonly INewProjection newProj;

        public NewProjectionNextOverlapValidation(IProjectionRepository projectRepo, IMovieRepository movieRepo, INewProjection newProj)
        {
            this.projectRepo = projectRepo;
            this.movieRepo = movieRepo;
            this.newProj = newProj;
        }

        public async Task<NewProjectionSummary> NewAsync(IProjectionCreation proj)
        {
            IEnumerable<IProjection> movieProjectionsInRoom = await projectRepo.GetActiveProjectionsAsync(proj.RoomId);

            IProjection nextProjection = movieProjectionsInRoom.Where(x => x.StartDate > proj.StartDate)
                                                                       .OrderBy(x => x.StartDate)
                                                                       .FirstOrDefault();

            if (nextProjection != null)
            {
                IMovie curMovie = await movieRepo.GetByIdAsync(proj.MovieId);
                IMovie nextProjectionMovie = await movieRepo.GetByIdAsync(nextProjection.MovieId);

                DateTime curProjectionEndTime = proj.StartDate.AddMinutes(curMovie.DurationMinutes);

                if (curProjectionEndTime >= nextProjection.StartDate)
                {
                    return new NewProjectionSummary(false, $"Projection overlaps with next one: {nextProjectionMovie.Name} at {nextProjection.StartDate}");
                }
            }

            return await newProj.NewAsync(proj);
        }
    }
}