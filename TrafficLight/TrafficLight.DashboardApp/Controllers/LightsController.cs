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
        public LightsStatus GetAll()
        {
            return this.service.ToStatus();
        }

        [HttpGet, Route("status/{channel}")]
        public void Get(string channel, bool state)
        {
            this.service[channel] = state;
        }
        
        [HttpPost, Route("status")]
        public void PostAll(LightsStatus status)
        {
            this.service["red"] = status.Red;
            this.service["yellow"] = status.Yellow;
            this.service["green"] = status.Green;
        }

        [HttpPost, Route("status/{channel}")]
        public void Post(string channel, bool state)
        {
            this.service[channel] = state;
        }
    }
}
