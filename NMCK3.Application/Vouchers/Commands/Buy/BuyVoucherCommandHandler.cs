using NMCK3.Application.Abstractions.Messaging;
using NMCK3.Application.Common.Services;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using NMCK3.Domain.Shared;
using NMCK3.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Vouchers.Commands.Buy
{
    internal sealed class BuyVoucherCommandHandler :
        ICommandHandler<BuyVoucherCommand, ReadOnlyCollection<string>>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BuyVoucherCommandHandler(
            IDateTimeProvider dateTimeProvider,
            IVoucherRepository voucherRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _dateTimeProvider = dateTimeProvider;
            _voucherRepository = voucherRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ReadOnlyCollection<string>>> Handle(BuyVoucherCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _userRepository.GetUserById(request.BuyerId, cancellationToken);

            var purchaseDate = VoucherPurchaseDate.Create(
                PersianDate.Today(_dateTimeProvider.Now).Value,
                _dateTimeProvider.Now);

            if (purchaseDate.IsFailure)
            {
                return Result.Fail<ReadOnlyCollection<string>>(purchaseDate.Error);
            }

            var vouchers = await GenerateVouchers(
                request.Count, buyer, purchaseDate.Value, cancellationToken);

            if (vouchers.IsFailure)
                return Result.Fail<ReadOnlyCollection<string>>(vouchers.Error);

            return vouchers.Value.AsReadOnly();
        }

        private async Task<Result<List<string>>> GenerateVouchers(
            int count, User buyer, VoucherPurchaseDate purchaseDate,
            CancellationToken cancellationToken)
        {
            var vouchers = new List<string>();

            var counter = 1;
            while (counter <= count)
            {
                var voucherResult = GetNewVoucher(buyer, purchaseDate);

                var voucherAlreadyExist =
                    await _voucherRepository.GetVoucherByCode(
                        voucherResult.Value.VoucherCode,
                        cancellationToken);

                if (voucherAlreadyExist is not null)
                    continue;

                if (voucherResult.IsFailure)
                    return Result.Fail<List<string>>(voucherResult.Error);

                vouchers.Add(voucherResult.Value.VoucherCode.Value);

                _voucherRepository.Add(voucherResult.Value);

                counter++;
            }
            await _unitOfWork.CompleteAsync(cancellationToken);
            return vouchers;
        }

        private Result<Voucher> GetNewVoucher(User buyer, VoucherPurchaseDate purchaseDate)
        {
            var voucherCode = VoucherCode.Create(GenerateNewVoucher());

            var voucherResult = Voucher.Create(voucherCode.Value, buyer, purchaseDate, _dateTimeProvider.Now);

            return voucherResult;
        }

        private string GenerateNewVoucher()
        {
            var voucher = new StringBuilder();
            var random = new Random();

            while (voucher.Length < 16)
                voucher.Append(random.Next(10).ToString());

            return voucher.ToString();
        }
    }
}
