﻿using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionMovieValidation : INewProjection
    {
        private readonly IMovieRepository movieRepo;
        private readonly INewProjection newProj;

        public NewProjectionMovieValidation(IMovieRepository movieRepo, INewProjection newProj)
        {
            this.movieRepo = movieRepo;
            this.newProj = newProj;
        }

        public async Task<NewProjectionSummary> NewAsync(IProjectionCreation projection)
        {
            IMovie movie = await movieRepo.GetByIdAsync(projection.MovieId);

            if (movie == null)
            {
                return new NewProjectionSummary(false, $"Movie with id {projection.MovieId} does not exist");
            }

            return await newProj.NewAsync(projection);
        }
    }
}