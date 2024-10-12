using Microsoft.AspNetCore.Builder;

namespace Pooshit.AspNetCore.Services.Extensions {
        /// <summary>
        /// Extensions for Cross-Origin-Ressource-Sharing
        /// </summary>
        public static class CorsExtensions {

            /// <summary>
            /// Custom middleware for adding "Access-Control-Allow-Origin" headers. Allowed origins are defined under "security:corsOrigins" in the config
            /// </summary>
            /// <param name="app">application builder to extend</param>        
            /// <param name="origins">allowed web-origins to call the Api</param>
            /// <param name="methods">allowed HTTP-Methods in requests</param>
            /// <param name="headers">allowed HTTP-Headers in requests</param>
            /// <returns>the application builder for fluent behavior</returns>
            public static IApplicationBuilder UseCors(this IApplicationBuilder app, string[] origins, string[] methods, string[] headers) {
                return app.UseCors(builder => {
                    if(origins != null)
                        builder.WithOrigins(origins);

                    if(methods != null)
                        builder.WithMethods(methods);

                    if(headers != null)
                        builder.WithHeaders(headers);

                    builder.WithExposedHeaders("Content-Disposition");
                });
            }

            /// <summary>
            /// Custom middleware for adding "Access-Control-Allow-Origin" headers. Allowed origins are defined under "security:corsOrigins" in the config
            /// </summary>
            /// <param name="app"> IApplicationBuilder-Object </param>        
            /// <returns> IApplicationBuilder-Object extended with new middleware </returns>
            public static IApplicationBuilder UseDefaultCors(this IApplicationBuilder app) {
                return app.UseCors(new []{"*"}, new []{ "GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS", "BULK" }, new []{"*"});
            }
        }
}