using FluentValidation;
using UserPtoject.Models;

namespace UserPtoject.Validations
{
    public class CreateUserValidation : AbstractValidator<User>
    {
        public CreateUserValidation() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Kısmı Boş Bırakılamaz");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyisim Kısmı Boş Bırakılamaz");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("En Az 3 Karakterli isim Girmelisiniz");
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage("En Az 2 Karakterli Soyisim Girmelisiniz");
        }

    }
}
