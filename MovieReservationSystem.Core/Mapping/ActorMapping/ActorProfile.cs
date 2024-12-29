using AutoMapper;

namespace MovieReservationSystem.Core.Mapping.ActorMapping
{
    public partial class ActorProfile : Profile
    {
        public ActorProfile()
        {
            GetAllActorsMapping();
            GetActorByIdMapping();
            CreateActorMapping();
            EditActorMapping();
        }
    }
}
