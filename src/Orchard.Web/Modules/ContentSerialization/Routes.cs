using System.Collections.Generic;
using System.Web.Http;
using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;

namespace ContentSerialization {
    public class HttpRoutes : IHttpRouteProvider {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes()) {
                routes.Add(routeDescriptor);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            var routeDescriptors = new List<RouteDescriptor> {
                new HttpRouteDescriptor {
                    Priority = 5,
                    RouteTemplate = "api/Contents/{controller}/{action}/{id}",
                    Defaults = new {
                        area = "ContentSerialization",
                        id = RouteParameter.Optional
                    }
                },
                new HttpRouteDescriptor {
                    Priority = 5,
                    RouteTemplate = "api/{id}",
                    Defaults = new {
                        controller = "Alias",
                        action = "Display",
                        area = "ContentSerialization",
                        IDictionary = RouteParameter.Optional
                    }
                }
            };

            return routeDescriptors;
        }
    }
}