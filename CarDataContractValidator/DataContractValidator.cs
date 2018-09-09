using CarDataContract;
using FluentValidation;

namespace CarDataContractValidator
{
    public class DataContractValidator : AbstractValidator<CarContract>
    {
        public DataContractValidator()
        {
            RuleFor(c => c.Title).NotNull().NotEmpty().Length(5, 50);
            RuleFor(c => c.Fuel).IsInEnum().NotNull().NotEmpty();
            RuleFor(c => c.Price).NotNull().NotEmpty().GreaterThan(0).LessThanOrEqualTo(int.MaxValue);            
            RuleFor(c => c.Mileage).NotNull().NotEmpty().When(c => !c.IsNew);
            RuleFor(c => c.FirstRegistration).NotNull().NotEmpty().When(c => !c.IsNew);
            RuleFor(c => c.Mileage).Empty().When(c => c.IsNew).WithMessage("Mileage must not be passed for new car");
            RuleFor(c => c.FirstRegistration).Empty().When(c => c.IsNew).WithMessage("FistRegistration date must not be passed for new car");
        }
    }
    public class CarIdValidator : AbstractValidator<int>
    {
        public CarIdValidator()
        {
            RuleFor(c => c).GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
        }
    }
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().LessThanOrEqualTo(int.MaxValue).GreaterThan(0);
            RuleFor(c => c.Title).NotNull().NotEmpty().Length(5, 50);
            RuleFor(c => c.Fuel).IsInEnum().NotNull().NotEmpty();
            RuleFor(c => c.Price).NotNull().NotEmpty().GreaterThan(0).LessThanOrEqualTo(int.MaxValue);
            RuleFor(c => c.Mileage).NotNull().NotEmpty().When(c => !c.IsNew);
            RuleFor(c => c.FirstRegistration).NotNull().NotEmpty().When(c => !c.IsNew);
            RuleFor(c => c.Mileage).Empty().When(c => c.IsNew).WithMessage("Mileage must not be passed for new car");
            RuleFor(c => c.FirstRegistration).Empty().When(c => c.IsNew).WithMessage("FistRegistration date must not be passed for new car");
        }
    }
}
