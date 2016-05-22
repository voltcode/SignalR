using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using SignalR_bugApp.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

[assembly: OwinStartupAttribute(typeof(SignalR_bugApp.Startup))]
namespace SignalR_bugApp
{
    public class FilteredCamelCasePropertyNamesContractResolver : DefaultContractResolver
    {
        public FilteredCamelCasePropertyNamesContractResolver()
        {
            AssembliesToInclude = new HashSet<Assembly>();
            TypesToInclude = new HashSet<Type>();
        }
        // Identifies assemblies to include in camel-casing
        public HashSet<Assembly> AssembliesToInclude { get; set; }
        // Identifies types to include in camel-casing
        public HashSet<Type> TypesToInclude { get; set; }
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            Type declaringType = member.DeclaringType;
            if (
                TypesToInclude.Contains(declaringType)
                || AssembliesToInclude.Contains(declaringType.Assembly))
            {
                jsonProperty.PropertyName = ToCamelCase(jsonProperty.PropertyName);
            }
            return jsonProperty;
        }
        public string ToCamelCase(string text)
        {
            return char.ToLower(text[0]) + text.Substring(1);
        }
    }



        public partial class Startup
        {
            private static readonly Lazy<JsonSerializer> JsonSerializerFactory = new Lazy<JsonSerializer>(GetJsonSerializer);
            private static JsonSerializer GetJsonSerializer()
            {
                return new JsonSerializer
                {
                    ContractResolver = new FilteredCamelCasePropertyNamesContractResolver
                    {
                        // 1) Register all types in specified assemblies:
                        AssembliesToInclude =
                 {
                     typeof (Startup).Assembly
                 },
                        // 2) Register individual types:
                        //TypesToInclude =
                        //                {
                        //                    typeof(Hubs.Message),
                        //                }
                    }
                };
            }

            public void Configuration(IAppBuilder app)
            {
                ConfigureAuth(app);
                app.MapSignalR(new Microsoft.AspNet.SignalR.HubConfiguration() { EnableDetailedErrors = true, EnableJavaScriptProxies = true, EnableJSONP = true });

                GlobalHost.DependencyResolver.Register(typeof(MyHub), () => new MyHub());
                GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => JsonSerializerFactory.Value);
            }
    }
}
