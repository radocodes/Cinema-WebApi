using CinemAPI.Domain;
using CinemAPI.Domain.AvailableSeats;
using CinemAPI.Domain.CancelReservation;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.NewProjection;
using CinemAPI.Domain.NewReservation;
using CinemAPI.Domain.NewReservedTicket;
using CinemAPI.Domain.NewTicket;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer
{
    public class DomainPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<INewProjection, NewProjectionCreation>();
            container.RegisterDecorator<INewProjection, NewProjectionMovieValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionUniqueValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionRoomValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionPreviousOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionNextOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionAvailableSeatsValidation>();

            container.Register<IAvailableSeatsCount, AvailableSeatsCount>();
            container.RegisterDecorator<IAvailableSeatsCount, AvailableSeatsCountExistValidation>();
            container.RegisterDecorator<IAvailableSeatsCount, AvailableSeatsCountLateValidation>();

            container.Register<INewReservation, NewReservationCreation>();
            container.RegisterDecorator<INewReservation, NewReservationLateValidation>();
            container.RegisterDecorator<INewReservation, NewReservationNotExistSeatsValidation>();
            container.RegisterDecorator<INewReservation, NewReservationUniqueValidation>();

            container.Register<INewTicket, NewTicketCreation>();
            container.RegisterDecorator<INewTicket, NewTicketLateValidation>();
            container.RegisterDecorator<INewTicket, NewTicketSeatValidation>();

            container.Register<INewReservedTicket, NewReservedTicketCreation>();
            container.RegisterDecorator<INewReservedTicket, NewReservedTicketLateValidation>();
            container.RegisterDecorator<INewReservedTicket, NewReservedTicketNotExistValidation>();

            container.Register<ICancelReservations, CancelReservations>();
            
        }
    }
}