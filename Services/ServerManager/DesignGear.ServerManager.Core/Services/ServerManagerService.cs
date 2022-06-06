using Autodesk.Forge.DesignAutomation.Model;
using DesignGear.Contracts.Enums;
using DesignGear.Contracts.Models.ServerManager.Derivative;
using DesignGear.ServerManager.Core.ForgeUtils;
using DesignGear.ServerManager.Core.Helpers;
using DesignGear.ServerManager.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DesignGear.ServerManager.Core.Services
{
    public class ServerManagerService : IServerManagerService
    {
        private readonly ForgeSettings _forgeSettings;
        static readonly string bucketName = "test_bucket_tv237s4155";
        static readonly string outputPath = @"d:\result";

        public ServerManagerService(IOptions<ForgeSettings> forgeSettings)
        {
            _forgeSettings = forgeSettings.Value;
        }

        public async Task<string> TranslateSvfAsync(IFormFile packageFile, string rootFileName)
        {
            var api = new Derivative(_forgeSettings);
            await api.Authenticate();
            var bucketKey = await api.CreateBucket(bucketName);
            var objInfo = await api.UploadZip(bucketKey, packageFile);
            return await api.Translate(Base64(objInfo.ObjectId), rootFileName);
        }

        public async Task<SvfStatus> CheckStatusJobAsync(string urn)
        {
            var api = new Derivative(_forgeSettings);
            await api.Authenticate();
            var manifest = await api.CheckStatusJob(urn);
            if (manifest.progress == "complete")
            {
                var status = manifest.derivatives?[0].status;
                if (status == "success")
                {
                    return SvfStatus.Ready;
                }

                return SvfStatus.IncorrectRequestError;
            }
            return SvfStatus.InProcess;
        }

        public async Task<byte[]> DownloadSvfAsync(string urn)
        {
            var api = new Derivative(_forgeSettings);
            await api.Authenticate();
            return await api.DownloadSvfAsync(urn);
        }

        public async Task<VmWorkItem> ProcessModelAsync(IFormFile appBundleFile, IFormFile packageFile)
        {
            var inventor = new Automation();
            await inventor.SetupOwnerAsync();
            //var myApp = await inventor.SetupAppBundleAsync();
            var myApp = await inventor.SetupAppBundleAsync(appBundleFile);
            var myActivity = await inventor.SetupActivityAsync(myApp);
            //create bucket
            var api = new Derivative(_forgeSettings);
            await api.Authenticate();
            var bucketKey = await api.CreateBucket(bucketName);
            //upload input file
            var objInfo = await api.UploadZip(bucketKey, packageFile);
            //create input file URL
            var inputFile = await api.CreateSignedResource(bucketKey, objInfo.ObjectKey);
            //create output file temp URL
            var outputFile = await api.CreateSignedResource(bucketKey, objInfo.ObjectKey + "output");
            var workItemId = await inventor.SubmitWorkItemAsync(myActivity, inputFile.SignedUrl, outputFile.SignedUrl);
            //download from temp URL
            //await inventor.DownloadToDocsAsync(outputFile.SignedUrl, outputPath);
            return new VmWorkItem()
            {
                Id = workItemId,
                Url = outputFile.SignedUrl
            };
        }

        public async Task<Status> CheckStatusAsync(string id)
        {
            var inventor = new Automation();
            return await inventor.CheckStatusAsync(id);
        }

        private static string Base64(string input)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
