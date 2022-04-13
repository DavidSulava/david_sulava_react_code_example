using System.Threading.Tasks;
using ConfigurationsManager.ForgeUtils;

namespace ConfigurationsManager
{
	public class ServerManager
	{
		static readonly string bucketName = "test_bucket_tv237s4155";
		static readonly string svfPath = @"d:\Suspension\";
		static readonly string outputPath = @"d:\result";
		public async Task<string> GetSvfAsync(string filePath)
		{
			var api = new Derivative();
			await api.Authenticate();
			var bucketKey = await api.CreateBucket(bucketName);
			var objInfo = await api.UploadZip(bucketKey, filePath);
			var urn = await api.Translate(Utils.Base64(objInfo.ObjectId));
			await api.DownloadSvf(urn, svfPath);
			return urn;
		}

		public async Task<string> ProcessModelAsync(string filePath)
		{
			var inventor = new Automation();
			await inventor.SetupOwnerAsync();
			var myApp = await inventor.SetupAppBundleAsync();
			var myActivity = await inventor.SetupActivityAsync(myApp);
			//create bucket
			var api = new Derivative();
			await api.Authenticate();
			var bucketKey = await api.CreateBucket(bucketName);
			//upload input file
			var objInfo = await api.UploadZip(bucketKey, filePath);
			//create input file URL
			var inputFile = await api.CreateSignedResource(bucketKey, objInfo.ObjectKey);
			//create output file temp URL
			var outputFile = await api.CreateSignedResource(bucketKey, objInfo.ObjectKey + "output");
			await inventor.SubmitWorkItemAsync(myActivity, inputFile.SignedUrl, outputFile.SignedUrl);
			//download from temp URL
			await inventor.DownloadToDocsAsync(outputFile.SignedUrl, outputPath);
			return outputFile.SignedUrl;
		}
	}
}
