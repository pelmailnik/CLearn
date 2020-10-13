using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace StrCalc.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string expressoin { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost(string exp)
        {
            var expressionValidator = new ExpressionValidator();
            ICalculator obj = new Calculator(expressionValidator);

            if (!String.IsNullOrEmpty(exp))
            {
                expressoin = obj.Calculate(exp).ToString();
            }
        }
    }
}
