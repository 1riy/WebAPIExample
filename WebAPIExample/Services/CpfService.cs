public class CpfService
{
    public CpfService(int seed)
    {
        this.rand = new Random(seed);
    }
    Random rand;

    public bool Validate(string cpf)
    {
        string cpf9digits = cpf.Substring(0, 9);
        string cpfValidation = cpf.Substring(9, 2);
        string realValidation = getValidationDigits(cpf9digits);
        return cpfValidation == realValidation;
    }

    public string Generate()
    {
        int cpfValue = rand.Next(1000000000);
        string cpf = cpfValue.ToString("000\\.000\\.000");
        string cpf9digits = cpfValue.ToString("0000000000");
        
        string validation = getValidationDigits(cpf9digits);
        return cpf + '-' + validation;
    }

    // detalhes em: https://www.calculadorafacil.com.br/computacao/validar-cpf
    private string getValidationDigits(string cpf9digits)
    {
        int digit0 = 0, digit1 = 0;
        for (int i = 0; i < cpf9digits.Length; i++)
        {
            var digit = cpf9digits[i] - '0';
            digit0 += digit * (i + 1);
            digit1 += digit * i;
        }
        digit0 %= 11;
        digit0 %= 10;
        
        digit1 += 9 * digit0;
        digit1 %= 11;
        digit1 %= 10;

        return digit0.ToString() + digit1.ToString();
    }
}