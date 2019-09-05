using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace AWSRekognitionTest
{
    public class RekognitionHelper
    {
        public static DetectLabelsResponse GetInfo(string filePath)
        {
            String photo = filePath;

            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();
            try
            {
                using (FileStream fs = new FileStream(photo, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    image.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load file " + photo);
                return null;
            }

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient();

            DetectLabelsRequest detectlabelsRequest = new DetectLabelsRequest()
            {
                Image = image,
                MaxLabels = 10,
                MinConfidence = 77F
            };

            try
            {
                DetectLabelsResponse detectLabelsResponse = rekognitionClient.DetectLabels(detectlabelsRequest);
                
                Console.WriteLine("Detected labels for " + photo);
                foreach (Label label in detectLabelsResponse.Labels)
                    Console.WriteLine("{0}: {1}", label.Name, label.Confidence);

                return detectLabelsResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
