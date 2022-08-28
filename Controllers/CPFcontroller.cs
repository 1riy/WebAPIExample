using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("cpf/")]
public class CpfController : ControllerBase
{
    [HttpGet("validate/{cpf}")]
    public object Validate(
        [FromServices]CpfService cpfService,
        string cpf
    )
    {
        try
        {
            if (cpf == null)
            {
                return new {
                    Status = "Fail",
                    Message = "Missing data 'cpf'"
                };
            }

            cpf = cpf
                .Replace(".", "")
                .Replace("-", "");
            cpf = cpf.Trim();
            
            bool hasOnlyNumbers = cpf
                .All(c => '0' <= c && c <= '9');
            if (cpf.Length != 11 || !hasOnlyNumbers)
            {
                return new {
                    Status = "Fail",
                    Message = "Invalid cpf data"
                };
            }
            
            return new {
                Status = "Success",
                Message = "The CPF was validated",
                Data = cpfService.Validate(cpf)
            };
        }
        catch
        {
            return new {
                Status = "Fail",
                Message = "Unknow error"
            };
        }
    }
    [HttpGet("generate")]
    public object Generate(
        [FromServices]CpfService cpfService
    )
    {
        return new{
            Status = "Sucess",
            Cpf = cpfService.Generate()
        };
    }
}