namespace SIPI_PRESENTEEISM.Core.Integrations.Azure
{
    using Microsoft.Azure.CognitiveServices.Vision.Face;
    using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
    using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;
    using System;

    public class AzureFaceRecognition : IFaceRecognition
    {
        private IFaceClient client;

        private readonly string PersonGroupId;

        public AzureFaceRecognition(IConfiguration configuration)
        {
            var key = configuration.GetValue<string>("Azure:FaceAPI:SUBSCRIPTION_KEY");
            var endpoint = configuration.GetValue<string>("Azure:FaceAPI:ENDPOINT");
            PersonGroupId = configuration.GetValue<string>("Azure:FaceAPI:PersonGroupId");

            client = new FaceClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
        }

        public async Task AddPerson(Guid userId, List<Stream> images)
        {
            var personGroup = await client.PersonGroup.GetAsync(PersonGroupId);
            if (personGroup != null)
            {
                await client.PersonGroup.CreateAsync(PersonGroupId, PersonGroupId, recognitionModel: RecognitionModel.Recognition04);
                personGroup = await client.PersonGroup.GetAsync(PersonGroupId);
            }

            if (personGroup is null)
                throw new Exception("Person Group not exists");

            foreach (var image in images)
            {
                var detectedFaces = await client.Face.DetectWithStreamAsync(image,
                    recognitionModel: RecognitionModel.Recognition04,
                    detectionModel: DetectionModel.Detection03,
                    returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.QualityForRecognition });

                if (detectedFaces.Count == 0)
                    throw new Exception("Some image not have a less one face");

                if (detectedFaces.Count > 1)
                    throw new Exception("Some image have two or more faces");

                var faceQuality = detectedFaces.First().FaceAttributes.QualityForRecognition.Value;
                if (faceQuality == QualityForRecognition.Low)
                    throw new Exception("Some image have low quality");
            }

            foreach (var image in images)
            {
                await client.PersonGroupPerson.AddFaceFromStreamAsync(personGroup.PersonGroupId, userId, image);
            }

            await client.PersonGroup.TrainAsync(personGroup.PersonGroupId);
        }

        public async Task<bool> Identify(Guid userId, Stream image)
        {
            var detectedFaces = await client.Face.DetectWithStreamAsync(image,
                recognitionModel: RecognitionModel.Recognition04,
                detectionModel: DetectionModel.Detection03,
                returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.QualityForRecognition });

            if (detectedFaces.Count == 0)
                throw new Exception("Some image not have a less one face");

            if (detectedFaces.Count > 1)
                throw new Exception("Some image have two or more faces");

            var face = detectedFaces.First();
            if (face.FaceAttributes.QualityForRecognition.Value == QualityForRecognition.Low)
                throw new Exception("Some image have low quality");

            var identifyResults = await client.Face.IdentifyAsync(new List<Guid>() { face.FaceId.Value }, PersonGroupId);

            foreach (var identifyResult in identifyResults)
            {
                if (identifyResult.Candidates.Count == 0)
                    continue;

                var person = await client.PersonGroupPerson.GetAsync(PersonGroupId, identifyResult.Candidates[0].PersonId);
                if (identifyResult.Candidates.Any(c => c.PersonId == userId && c.Confidence >= 50))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
