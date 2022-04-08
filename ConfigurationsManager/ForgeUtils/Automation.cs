using Autodesk.Forge.Core;
using Autodesk.Forge.DesignAutomation;
using Autodesk.Forge.DesignAutomation.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ConfigurationsManager.ForgeUtils
{
    public class Automation
    {
		static readonly string PackageName = "MyTestPackage";
		static readonly string ActivityName = "MyTestActivity";
		static readonly string Owner = "AppNickName_1"; //e.g. MyTestApp (it must be *globally* unique)
		//static readonly string UploadUrl = "https://developer.api.autodesk.com/oss/v2/signedresources/60646052-59ea-49a8-8487-6d420c77652e?region=US";
		static readonly string Label = "prod";
		static readonly string TargetEngine = "Autodesk.AutoCAD+23";

		DesignAutomationClient api = new DesignAutomationClient();

        public async Task SubmitWorkItemAsync(string myActivity, string inputUrl, string outputUrl)
        {
            var workItemStatus = await api.CreateWorkItemAsync(new WorkItem()
            {
                ActivityId = myActivity,
                Arguments = new Dictionary<string, IArgument>()
                {
                    { "input", new XrefTreeArgument() { Url = inputUrl/*"http://download.autodesk.com/us/samplefiles/acad/blocks_and_tables_-_imperial.dwg"*/ } },
                    { "params", new XrefTreeArgument() { Url = $"data:application/json, {{\"ExtractBlockNames\":\"true\", \"ExtractLayerNames\":\"true\"}}" } },
                    //{ "params", new XrefTreeArgument() { Url = $"data:application/json, {JsonConvert.SerializeObject(new CrxApp.Parameters { ExtractBlockNames = true, ExtractLayerNames = true })}" } },
                    //TODO: replace it with your own URL
                    { "result", new XrefTreeArgument() { Verb=Verb.Put, Url = outputUrl } }
                }
            });

            while (!workItemStatus.Status.IsDone())
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                workItemStatus = await api.GetWorkitemStatusAsync(workItemStatus.Id);
            }
            //if (workItemStatus.Status == Status.Success)
            var fname = await DownloadToDocsAsync(workItemStatus.ReportUrl, "Das-report.txt");
        }

        public async Task<string> SetupActivityAsync(string myApp)
        {
            var myActivity = $"{Owner}.{ActivityName}+{Label}";
            var actResponse = await this.api.ActivitiesApi.GetActivityAsync(myActivity, throwOnError: false);
            var activity = new Activity()
            {
                Appbundles = new List<string>()
                    {
                        myApp
                    },
                CommandLine = new List<string>()
                    {
                        $"$(engine.path)\\accoreconsole.exe /i \"$(args[input].path)\" /al \"$(appbundles[{PackageName}].path)\" /s $(settings[script].path)"
                    },
                Engine = TargetEngine,
                Settings = new Dictionary<string, ISetting>()
                    {
                        { "script", new StringSetting() { Value = "_test params.json outputs\n" } }
                    },
                Parameters = new Dictionary<string, Parameter>()
                    {
                        { "input", new Parameter() { Verb= Verb.Get, LocalName = "$(HostDwg)",  Required = true } },
                        { "params", new Parameter() { Verb= Verb.Get, LocalName = "params.json", Required = true} },
                        { "result", new Parameter() { Verb= Verb.Put, Zip= true, LocalName = "outputs", Required= true} }
                    },
                Id = ActivityName
            };
            if (actResponse.HttpResponse.StatusCode == HttpStatusCode.NotFound)
            {
                await api.CreateActivityAsync(activity, Label);
                return myActivity;
            }
            await actResponse.HttpResponse.EnsureSuccessStatusCodeAsync();
            if (!Equals(activity, actResponse.Content))
            {
                await api.UpdateActivityAsync(activity, Label);
            }
            return myActivity;

            bool Equals(Activity a, Activity b)
            {
                //ignore id and version
                b.Id = a.Id;
                b.Version = a.Version;
                var res = a.ToString() == b.ToString();
                return res;
            }
        }

        public async Task<string> SetupAppBundleAsync()
        {
            var myApp = $"{Owner}.{PackageName}+{Label}";
            var appResponse = await this.api.AppBundlesApi.GetAppBundleAsync(myApp, throwOnError: false);
            var app = new AppBundle()
            {
                Engine = TargetEngine,
                Id = PackageName
            };
            var package = CreateZip(@"d:\package");
            if (appResponse.HttpResponse.StatusCode == HttpStatusCode.NotFound)
            {
                await api.CreateAppBundleAsync(app, Label, package);
                return myApp;
            }
            await appResponse.HttpResponse.EnsureSuccessStatusCodeAsync();
            if (!await EqualsAsync(package, appResponse.Content.Package))
            {
                await api.UpdateAppBundleAsync(app, Label, package);
            }
            return myApp;

            async Task<bool> EqualsAsync(string a, string b)
            {
                using (var aStream = File.OpenRead(a))
                {
                    var bLocal = await DownloadToDocsAsync(b, "das-appbundle.zip");
                    using (var bStream = File.OpenRead(bLocal))
                    {
                        using (var hasher = SHA256.Create())
                        {
                            var res = hasher.ComputeHash(aStream).SequenceEqual(hasher.ComputeHash(bStream));
                            return res;
                        }
                    }
                }
            }
        }

        public async Task<bool> SetupOwnerAsync()
        {
            var nickname = await api.GetNicknameAsync("me");
            if (nickname == Owner)
            {
                //tNo nickname for this clientId yet. Attempting to create one.
                var resp = await api.ForgeAppsApi.CreateNicknameAsync("me", new NicknameRecord() { Nickname = Owner }, throwOnError: false);
                if (resp.StatusCode == HttpStatusCode.Conflict)
                {
                    //There are already resources associated with this clientId or nickname is in use
                    return false;
                }
                await resp.EnsureSuccessStatusCodeAsync();
            }
            return true;
        }
        static string CreateZip(string folderPath)
        {
            string zip = @"d:\package.zip";
            if (File.Exists(zip))
                File.Delete(zip);
            using (var archive = ZipFile.Open(zip, ZipArchiveMode.Create))
            {
                string bundle = PackageName + ".bundle";
                string name = "PackageContents.xml";
                archive.CreateEntryFromFile(folderPath + "\\" + name, Path.Combine(bundle, name));
                name = "CrxApp.dll";
                archive.CreateEntryFromFile(folderPath + "\\" + name, Path.Combine(bundle, "Contents", name));
                name = "Newtonsoft.Json.dll";
                archive.CreateEntryFromFile(folderPath + "\\" + name, Path.Combine(bundle, "Contents", name));
            }
            return zip;

        }

        public async Task<string> DownloadToDocsAsync(string url, string localFile)
        {
            var fname = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), localFile);
            using (var client = new HttpClient())
            {
                var content = (await client.GetAsync(url)).Content;
                using (var output = File.Create(fname))
                {
                    (await content.ReadAsStreamAsync()).CopyTo(output);
                    output.Close();
                }
            }
            return fname;
        }
    }
}
