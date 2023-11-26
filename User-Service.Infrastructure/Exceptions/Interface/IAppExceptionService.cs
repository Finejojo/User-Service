using FluentValidation.Results;

public interface IAppExceptionService
   {
      AppException GetValidationExceptionResult(ValidationResult validationResult);
      
   }