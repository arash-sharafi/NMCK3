using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NMCK3.Application.Exams.Commands.Create
{
    internal class CreateExamCommandValidator:AbstractValidator<CreateExamCommand>
    {
        public CreateExamCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Capacity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ExamDate).NotEmpty().Length(8);
        }
    }
}
