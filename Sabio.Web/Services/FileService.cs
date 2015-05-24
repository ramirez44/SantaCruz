using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon;

namespace Sabio.Web.Services
{
    public class FileService 
 {
        static IAmazonS3 client; //api controller will be doing the heavy lifting - keep this as generic as possible to be used by others

        public static void uploadFile(string localPath, string remotePath, string fileType) //keep these - do not change this
        {
            using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey, RegionEndpoint.USWest2))
            {
                Console.WriteLine("uploading an object");

                var uploadRequest = new TransferUtilityUploadRequest
              {
                  //this looks like an AJAX call - recognize this as an ajax call - regardless of client AMAZON or something else
                  FilePath = localPath,
                   BucketName = bucketName,
                  CannedACL = S3CannedACL.PublicRead,
                  Key = keyName + remotePath,//unique name 
                  ContentType = fileType
                }; 
                
                var fileTransferUtility = new TransferUtility(client);
                fileTransferUtility.Upload(uploadRequest);
                //this is like jquery function telling the file to UPLOAD

            }
                }




    }
}
