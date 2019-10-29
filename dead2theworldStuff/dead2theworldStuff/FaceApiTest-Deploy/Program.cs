using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.ProjectOxford.Common;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceApiTestDeploy
{
    internal class Program
    {
        private readonly IFaceClient faceClient = new FaceClient(
            new ApiKeyServiceClientCredentials("7a4c13f93842422392b8f472f127381b"),
            new System.Net.Http.DelegatingHandler[] { });

        private static async Task Main(string[] args)
        {
            //use this and replace person id's to add new users and train them to our person group
            List<string> imgUrls = new List<string>() { "https://i.imgur.com/Npk2a0U.jpg" };
            //"https://i.imgur.com/p9vFrlW.jpg",
            //"https://i.imgur.com/JFBTaUW.jpg",
            //"https://i.imgur.com/ktCHy9S.jpg",
            //"https://i.imgur.com/nCU12mD.jpg" };

            IFaceServiceClient faceServiceClient = new FaceServiceClient("7a4c13f93842422392b8f472f127381b",
                "https://southcentralus.api.cognitive.microsoft.com/face/v1.0");

            try
            {
                var test = await faceServiceClient.GetPersonGroupAsync("5");
                
                Console.Write("Got PersonGroup " + test.Name);
                var person = await faceServiceClient.CreatePersonAsync("5", "Craig, Dave", "Craig, Dave");
                foreach (string img in imgUrls)
                {
                    await faceServiceClient.AddPersonFaceAsync("5", person.PersonId, img);
                }
                Console.WriteLine("Training Person Group");
                await faceServiceClient.TrainPersonGroupAsync("5");
                Console.WriteLine("Finished TrainingPerson Group - feel free to test now");
            }
            catch (ClientException ex)
            {
                Console.WriteLine(ex.Error.Code);
                Console.WriteLine(ex.Error.Message);
            }
            Console.ReadKey();
        }
    }
}