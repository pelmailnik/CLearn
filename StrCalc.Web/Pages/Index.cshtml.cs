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
        private readonly ICalculator _calculator;

        public string expressoin { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ICalculator calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }

        public void OnPost(string exp)
        {

            if (!String.IsNullOrEmpty(exp))
            {
                expressoin = _calculator.Calculate(exp).ToString();
            }
        }
    }
}
