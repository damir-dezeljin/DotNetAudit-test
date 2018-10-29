using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace si.dezo.test.DotNetAudit.Misc {
    public class CustomErrorResource : ObjectResult {
        public CustomErrorResource (HttpStatusCode code, string errorMessage) : base (new { msg = errorMessage }) {
            this.StatusCode = (int) code;
        }
    }
}