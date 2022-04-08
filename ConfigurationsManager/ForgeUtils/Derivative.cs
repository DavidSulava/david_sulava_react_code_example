using Autodesk.Forge;
using Autodesk.Forge.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RestSharp;

namespace ConfigurationsManager.ForgeUtils
{
    public class Derivative
    {
		static readonly string rootFileName = "Suspension.iam";
		private string _accessToken;
		private DateTime _expiresAt;

		public async Task<Bearer> Authenticate()
		{
			TwoLeggedApi oAuth = new TwoLeggedApi();
			Bearer token = (await oAuth.AuthenticateAsync(
				Configuration.ForgeClientId,
				Configuration.ForgeClientSecret,
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
					bucketKey = Configuration.ForgeClientId.ToLower() + DateTime.Now.Ticks.ToString();
				PostBucketsPayload bucketPayload = new PostBucketsPayload(bucketKey.ToLower(), null, PostBucketsPayload.PolicyKeyEnum.Transient);
				Bucket bucket = (await buckets.CreateBucketAsync(bucketPayload, Configuration.ForgeRegion)).ToObject<Bucket>();
				return bucket.BucketKey;
			}
			catch
			{
				return bucketKey;
			}
		}

		public async Task<ObjectDetails> UploadZip(string bucketKey, string filePath)
		{
			ObjectsApi objects = new ObjectsApi();
			objects.Configuration.AccessToken = _accessToken;
			string objectKey = Path.GetFileName(filePath);
			ObjectDetails uploadedObj;

			using (StreamReader streamReader = new StreamReader(filePath))
			{
				uploadedObj = (await objects.UploadObjectAsync(bucketKey,
					   objectKey, (int)streamReader.BaseStream.Length, streamReader.BaseStream,
					   "application/octet-stream")).ToObject<ObjectDetails>();
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

		public async Task<dynamic> Translate(string urn)
		{
			//var urn = bucket.BucketKey;

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

			var progress = string.Empty;
			dynamic manifest = null;
			while (progress != "complete")
			{
				manifest = await derivative.GetManifestAsync(urn);
				progress = manifest.progress;
			}
			var status = manifest.derivatives?[0].status;
			if (status == "failed")
				return null;
			return manifest.urn;
		}

		public async Task<bool> DownloadSvf(string urn, string localPath)
		{
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
					return false;
				}
				else
				{
					// combine with selected local path
					string pathToSave = Path.Combine(localPath, resource.LocalPath);
					// ensure local dir exists
					Directory.CreateDirectory(Path.GetDirectoryName(pathToSave));
					// save file
					File.WriteAllBytes(pathToSave, response.RawBytes);
				}
			}

			return true;
		}
	}
}
