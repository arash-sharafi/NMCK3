using Mapster;
using NMCK3.Application.ExamReservations.Commands.Add;
using NMCK3.Shared.Contracts.ExamReservations;

namespace NMCK3.Api.Common.Mapping
{
    public class ExamMappingConfig:IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config
                .NewConfig<(string UserId, AddExamReservationRequest Request), AddExamReservationCommand>()
                .Map(dest => dest.ParticipantId, src => src.UserId)
                .Map(dest => dest, src => src.Request);
        }
    }
}
