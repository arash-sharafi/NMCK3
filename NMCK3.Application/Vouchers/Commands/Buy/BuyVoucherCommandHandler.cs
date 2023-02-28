using NMCK3.Application.Abstractions.Messaging;
using NMCK3.Application.Common.Services;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Common;
using NMCK3.Domain.Entities;
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
        ICommandHandler<BuyVoucherCommand, ReadOnlyCollection<Voucher>>
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

        public async Task<Result<ReadOnlyCollection<Voucher>>> Handle(BuyVoucherCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _userRepository.GetUserById(request.UserId, cancellationToken);

            var purchaseDate = VoucherPurchaseDate.Create(
                PersianDate.Today(_dateTimeProvider.Now).Value,
                _dateTimeProvider.Now);

            if (purchaseDate.IsFailure)
            {
                return Result.Fail<ReadOnlyCollection<Voucher>>(purchaseDate.Error);
            }

            var vouchers = await GenerateVouchers(
                request.Count, buyer, purchaseDate.Value, cancellationToken);

            if (vouchers.IsFailure)
            {
                return Result.Fail<ReadOnlyCollection<Voucher>>(vouchers.Error);
            }

            return vouchers.Value.AsReadOnly();
        }

        private Result<Voucher> GetNewVoucher(User buyer, VoucherPurchaseDate purchaseDate)
        {
            var voucherCode = VoucherCode.Create(GenerateNewVoucher());

            var voucherResult = Voucher.Create(voucherCode.Value, buyer, purchaseDate, _dateTimeProvider.Now);

            return voucherResult;
        }

        private async Task<Result<List<Voucher>>> GenerateVouchers(
            int count, User buyer, VoucherPurchaseDate purchaseDate,
            CancellationToken cancellationToken)
        {
            var vouchers = new List<Voucher>();

            var counter = 1;
            while (counter <= count)
            {
                var voucherResult = GetNewVoucher(buyer, purchaseDate);

                var voucherAlreadyExist =
                    await _voucherRepository.GetVoucherByCode(
                        voucherResult.Value.VoucherCode.Value,
                        cancellationToken);

                if (voucherAlreadyExist is not null)
                    continue;

                if (voucherResult.IsFailure)
                {
                    return Result.Fail<List<Voucher>>(voucherResult.Error);
                }
                vouchers.Add(voucherResult.Value);

                _voucherRepository.Add(voucherResult.Value);
                await _unitOfWork.CompleteAsync(cancellationToken);

                counter++;
            }

            return vouchers;
        }

        private string GenerateNewVoucher()
        {
            var voucher = new StringBuilder();
            var random = new Random();

            while (voucher.Length < 16)
            {
                voucher.Append(random.Next(10).ToString());
            }

            return voucher.ToString();
        }
    }
}
