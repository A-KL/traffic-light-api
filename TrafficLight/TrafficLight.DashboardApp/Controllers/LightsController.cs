namespace TraficLight.DashboardApp.Controllers
{
    using System.Web.Http;
    using TraficLight.DashboardApp.Domain;

    [RoutePrefix("api/{controller}")]
    public class LightsController : ApiController
    {
        private readonly ITrafficLightService service;

        public LightsController(ITrafficLightService service)
        {
            this.service = service;
        }

        [HttpGet, Route("status")]
        public LightsStatus Get()
        {
            return this.service.ToStatus();
        }

        [HttpPost, Route("status")]
        public void Post(LightsStatus status)
        {
            this.service["red"] = status.Red;
            this.service["yellow"] = status.Yellow;
            this.service["green"] = status.Green;
        }
    }
}
