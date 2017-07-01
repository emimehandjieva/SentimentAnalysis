using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SentimentAnalysis.VisualisationModule.Startup))]
namespace SentimentAnalysis.VisualisationModule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
