using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace StarChartTests
{
    public class ConfigureMiddlewareTests
    {
        [Fact(DisplayName = "Call AddControllers In ConfigureServices @call-addmvc-in-configureservices")]
        public void CallAddControllersInConfigureServicesTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Startup.cs";
            Assert.True(File.Exists(filePath), "`Startup.cs` was not found. Did you rename, move, or delete it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"services\s*?[.]AddControllers";
            var rgx = new Regex(pattern);

            Assert.True(rgx.IsMatch(file), "`Startup.ConfigureServices` didn't contain a call for `AddControllers` on `services`");
        }

        [Fact(DisplayName = "Call AddDbContext In ConfigureServices @call-adddbcontext-in-configureservices")]
        public void CallAddApplicationDbContextInConfigureServicesTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Startup.cs";
            Assert.True(File.Exists(filePath), "`Startup.cs` was not found. Did you rename, move, or delete it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"services\s*?[.]AddDbContext\s*?[<]\s*?ApplicationDbContext\s*?[>]";
            var rgx = new Regex(pattern);

            Assert.True(rgx.IsMatch(file), "`Startup.ConfigureServices` didn't contain a call for `AddDbContext` on `services` with the type argument `ApplicationDbContext`");
            //This doesn't technically verify UseInMemoryDatabase was used as an argument for AddDbContext, however; this will allow for other acceptable ways to handle this task
            Assert.True(file.Contains("UseInMemoryDatabase"), @"`Startup.ConfigureServices` didn't provide `options => options.UseInMemoryDatabase(""StarChart"")` as an argument for `AddDbContext<ApplicationDbContext>`");
        }

        [Fact(DisplayName = "Call UseRouting In Configure @call-usemvc-in-configure")]
        public void CallUseRoutingInConfigureTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Startup.cs";
            Assert.True(File.Exists(filePath), "`Startup.cs` was not found. Did you rename, move, or delete it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"app\s*?[.]UseRouting";
            var rgx = new Regex(pattern);

            Assert.True(rgx.IsMatch(file), "`Startup.Configure` didn't contain a call for `UseRouting` on `app`");
        }

        [Fact(DisplayName = "Call UseEndpoints In Configure @call-useendpoints-in-configure")]
        public void CallUseEndPointsInConfigureTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Startup.cs";
            Assert.True(File.Exists(filePath), "`Startup.cs` was not found. Did you rename, move, or delete it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"app\s*?[.]UseRouting\s*?[(]\s*?[)]\s*?;\s*?app\s*?[.]\s*?UseEndpoints\s*?[(]\s*?endpoints\s*?[=][>]\s*?[{]\s*?endpoints\s*?[.]\s*?MapControllers\s*?[(]\s*?[)]\s*?;\s*?[}]\s*?[)]\s*?;";
            var rgx = new Regex(pattern);

            Assert.True(rgx.IsMatch(file), "`Startup.Configure` didn't contain a call for `UseEndpoints` on `app` after the `UseRouting` call, with an argument of `endpoints => { endpoints.MapControllers(); }`");
        }
    }
}
