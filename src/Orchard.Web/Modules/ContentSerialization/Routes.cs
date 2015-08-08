using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContentSerialization {
    public class HttpRoutes : IHttpRouteProvider {

        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes()) {
                routes.Add(routeDescriptor);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new HttpRouteDescriptor {
                    Priority = 5,
                    RouteTemplate = "api/Contents/{controller}/{action}/{id}",
                    Defaults = new {
                        area = "ContentSerialization",
                        id = RouteParameter.Optional
                    }
                }
            };
        }
    }
}