using FluentValidation;


namespace API.Areas.Admin.ViewModels;

public class DestinationValidator : AbstractValidator<DestinationViewModel>
{
    public DestinationValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Vui lòng nhập tên");
        RuleFor(x => x.Email).NotEmpty()
            .WithMessage("Vui lòng nhập email");
        RuleFor(x => x.Address).NotEmpty()
            .WithMessage("Vui lòng nhập địa chỉ");
        RuleFor(x => x.Amenities).NotEmpty()
            .WithMessage("Vui lòng nhập tiện ích");
        RuleFor(x => x.PhoneNumber).NotEmpty()
            .WithMessage("Vui lòng nhập số điện thoại");
        RuleFor(x => x.CommuneWardId).NotEmpty()
            .WithMessage("Vui lòng chọn phường");
        RuleFor(x => x.Description).NotEmpty()
            .WithMessage("Vui lòng nhập mô tả");
        RuleFor(x => x.DestinationCategoryId).NotEmpty()
            .WithMessage("Vui lòng chọn danh mục");
        RuleFor(x => x.Tags).NotEmpty()
            .WithMessage("Vui lòng nhập thẻ");
        RuleFor(x => x.ShortDescription).NotEmpty()
            .WithMessage("Vui lòng nhập mô tả tóm tắt");
    }
}
