using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        [HttpPost("Validate")]
        public ActionResult<bool> ValidateCreditCard(string creditCardNumber)
        {
            try
            {
                creditCardNumber = creditCardNumber.Replace(" ", "").Replace("-", "");

                int checksum = int.Parse(creditCardNumber[creditCardNumber.Length - 1].ToString());
                int total = 0;

                for (int i = creditCardNumber.Length - 2; i >= 0; i--)
                {
                    int sum = 0;
                    int digit = int.Parse(creditCardNumber[i].ToString());
                    if (i % 2 == creditCardNumber.Length % 2) // right to left every odd digit
                    {
                        digit *= 2;
                    }

                    sum = digit / 10 + digit % 10;
                    total += sum;
                }

                return total % 10 != 0 ? 10 - total % 10 == checksum : checksum == 0;

            } catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while validating the credit card number.");
            }
        }
    }
}
