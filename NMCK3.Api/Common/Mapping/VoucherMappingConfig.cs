using Mapster;
using NMCK3.Application.Vouchers.Commands.Buy;
using NMCK3.Shared.Contracts.Vouchers;

namespace NMCK3.Api.Common.Mapping
{
    public class VoucherMappingConfig:IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config
                .NewConfig<(string UserId, BuyVoucherRequest Request), BuyVoucherCommand>()
                .Map(dest => dest.BuyerId, src => src.UserId)
                .Map(dest => dest, src => src.Request);
        }
    }
}
