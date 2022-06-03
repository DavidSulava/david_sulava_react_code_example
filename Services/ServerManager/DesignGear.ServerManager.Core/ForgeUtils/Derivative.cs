using Autodesk.Forge;
using Autodesk.Forge.Model;
using RestSharp;
using DesignGear.ServerManager.Core.Helpers;
using Microsoft.AspNetCore.Http;
using DesignGear.Contracts.Dto;
using System.IO.Compression;

namespace DesignGear.ServerManager.Core.ForgeUtils
{
    public class Derivative
    {
        private string _accessToken;
        private DateTime _expiresAt;
        private readonly ForgeSettings _forgeSettings;

        public Derivative(ForgeSettings forgeSettings)
        {
            _forgeSettings = forgeSettings;
        }

        public async Task<Bearer> Authenticate()
        {
            TwoLeggedApi oAuth = new TwoLeggedApi();
            Bearer token = (await oAuth.AuthenticateAsync(
                _forgeSettings.ClientId,
                _forgeSettings.ClientSecret,
                oAuthConstants.CLIENT_CREDENTIALS,
                new Scope[] { Scope.BucketRead, Scope.BucketCreate, Scope.DataRead, Scope.DataWrite })).ToObject<Bearer>();
            _accessToken = token.AccessToken;
            _expiresAt = DateTime.Now.AddSeconds(token.ExpiresIn.Value);
            return token;
        }

        public async Task<string> CreateBucket()
        {
            return await CreateBucket(null);
        }
        public async Task<string> CreateBucket(string bucketKey)
        {
            try
            {
                BucketsApi buckets = new BucketsApi();
                buckets.Configuration.AccessToken = _accessToken;
                if (string.IsNullOrEmpty(bucketKey))
                    bucketKey = _forgeSettings.ClientId.ToLower() + DateTime.Now.Ticks.ToString();
                PostBucketsPayload bucketPayload = new PostBucketsPayload(bucketKey.ToLower(), null, PostBucketsPayload.PolicyKeyEnum.Transient);
                Bucket bucket = (await buckets.CreateBucketAsync(bucketPayload, _forgeSettings.Region)).ToObject<Bucket>();
                return bucket.BucketKey;
            }
            catch
            {
                return bucketKey;
            }
        }

        public async Task<ObjectDetails> UploadZip(string bucketKey, IFormFile filePackage)
        {
            ObjectsApi objects = new ObjectsApi();
            objects.Configuration.AccessToken = _accessToken;
            string objectKey = filePackage.FileName;
            ObjectDetails uploadedObj;
            using (var stream = new MemoryStream())
            {
                filePackage.CopyTo(stream);
                stream.Seek(0, SeekOrigin.Begin);

                using (StreamReader streamReader = new StreamReader(stream))
                {
                    uploadedObj = (await objects.UploadObjectAsync(bucketKey,
                           objectKey, (int)streamReader.BaseStream.Length, streamReader.BaseStream,
                           "application/octet-stream")).ToObject<ObjectDetails>();
                }
            }

            return uploadedObj;
        }

        public async Task<PostObjectSigned> CreateSignedResource(string bucketKey, string objectKey)
        {
            ObjectsApi objects = new ObjectsApi();
            objects.Configuration.AccessToken = _accessToken;
            var pbs = new PostBucketsSigned(10);
            var result = (await objects.CreateSignedResourceAsync(bucketKey, objectKey, pbs, "readwrite")).ToObject<PostObjectSigned>();
            return result;
        }

        public async Task<dynamic> Translate(string urn, string rootFileName)
        {
            // prepare a SVF translation
            List<JobPayloadItem> outputs = new List<JobPayloadItem>()
            {
                new JobPayloadItem(
                    JobPayloadItem.TypeEnum.Svf,
                    new List<JobPayloadItem.ViewsEnum>()
                    {
                        JobPayloadItem.ViewsEnum._2d,
                        JobPayloadItem.ViewsEnum._3d
                    })
            };
            var job = new JobPayload(new JobPayloadInput(urn, true, rootFileName), new JobPayloadOutput(outputs));


            // start translation job
            DerivativesApi derivative = new DerivativesApi();
            derivative.Configuration.AccessToken = _accessToken;
            dynamic jobPosted = await derivative.TranslateAsync(job, true);
            return jobPosted.urn;

            //var progress = string.Empty;
            //dynamic manifest = null;
            //while (progress != "complete")
            //{
            //    manifest = await derivative.GetManifestAsync(urn);
            //    progress = manifest.progress;
            //}
            //var status = manifest.derivatives?[0].status;
            //if (status == "failed")
            //    return null;
            //return manifest.urn;
        }

        public async Task<dynamic> CheckStatusJob(string urn)
        {
            DerivativesApi derivative = new DerivativesApi();
            derivative.Configuration.AccessToken = _accessToken;
            return await derivative.GetManifestAsync(urn);
        }

        public async Task<IEnumerable<FileStreamDto>> DownloadSvf(string urn, string localPath)
        {

            var result = new List<FileStreamDto>();
            // get the list of resources to download
            var resourcesToDownload = await ExtractSvf.ExtractSVFAsync(urn, _accessToken);

            var client = new RestClient("https://developer.api.autodesk.com/");
            foreach (ExtractSvf.Resource resource in resourcesToDownload)
            {
                // prepare the GET to download the file
                RestRequest request = new RestRequest(resource.RemotePath, Method.GET);
                request.AddHeader("Authorization", "Bearer " + _accessToken);
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                else
                {
                    //// combine with selected local path
                    //string pathToSave = Path.Combine(localPath, resource.LocalPath);
                    //// ensure local dir exists
                    //Directory.CreateDirectory(Path.GetDirectoryName(pathToSave));
                    //// save file
                    //File.WriteAllBytes(pathToSave, response.RawBytes);

                    result.Add(new FileStreamDto()
                    {
                        FileName = resource.LocalPath,
                        Content = new MemoryStream(response.RawBytes),
                        Length = response.ContentLength,
                        ContentType = response.ContentType
                    });
                }
            }

            return result;
        }

        public async Task<byte[]> DownloadSvfAsync(string urn)
        {
            // get the list of resources to download
            var resourcesToDownload = await ExtractSvf.ExtractSVFAsync(urn, _accessToken);

            var client = new RestClient("https://developer.api.autodesk.com/");

            var filelist = new List<FileStreamDto>();

            foreach (ExtractSvf.Resource resource in resourcesToDownload)
            {
                // prepare the GET to download the file
                RestRequest request = new RestRequest(resource.RemotePath, Method.GET);
                request.AddHeader("Authorization", "Bearer " + _accessToken);
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                else
                {
                    filelist.Add(new FileStreamDto()
                    {
                        Content = new MemoryStream(response.RawBytes),
                        FileName = resource.LocalPath,
                        ContentType = response.ContentType,
                        Length = response.ContentLength
                    });
                }
            }


            using (var compressedFileStream = new MemoryStream())
            {
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    foreach (var caseAttachmentModel in filelist)
                    {
                        //Create a zip entry for each attachment
                        var zipEntry = zipArchive.CreateEntry(caseAttachmentModel.FileName);

                        //Get the stream of the attachment
                        //using (var originalFileStream = new MemoryStream(caseAttachmentModel.Content))
                        using (var zipEntryStream = zipEntry.Open())
                        {
                            //Copy the attachment stream to the zip entry stream
                            caseAttachmentModel.Content.CopyTo(zipEntryStream);
                        }
                    }
                }
                return compressedFileStream.ToArray();
            }
        }
    }
}
