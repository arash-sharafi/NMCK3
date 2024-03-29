﻿using NMCK3.Application.Abstractions.Messaging;
using NMCK3.Application.Common.Services;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using NMCK3.Domain.Shared;
using NMCK3.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Exams.Commands.Create
{
    internal sealed class CreateExamCommandHandler : ICommandHandler<CreateExamCommand, Guid>
    {
        private readonly IExamRepository _examRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateExamCommandHandler(IExamRepository examRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            _examRepository = examRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var examDateResult = ExamDate.Create(examDate: request.ExamDate,
                currentDate: _dateTimeProvider.Now);

            if (examDateResult.IsFailure)
            {
                return Result.Fail<Guid>(examDateResult.Error);
            }

            var result = Exam.Create(request.Name, examDateResult.Value, request.Description, request.Capacity);


            if (result.IsFailure)
            {
                return Result.Fail<Guid>(result.Error);
            }

            _examRepository.Add(result.Value);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return result.Value.Id;
        }
    }
}