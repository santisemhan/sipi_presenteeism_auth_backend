namespace SIPI_PRESENTEEISM.Core.Integrations.ARSA
{
    using Newtonsoft.Json;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Cognitive;
    using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class ARSAFaceRecognition : IFaceRecognition
    {
        private readonly HttpClient _httpClient;

        public ARSAFaceRecognition()
        {
            _httpClient = new HttpClient(); 
        }

        public Task AddPerson(Guid userId, List<Stream> images)
        {
            throw new NotImplementedException();
        }

        public async Task AddPerson(Guid userId, List<string> imagesURL)
        {
            // TODO: Arreglar: sacamos 3 fotos y guardamos solo la primera y la ultima
            var content = JsonConvert.SerializeObject(new {
                face_image_url = imagesURL.First(),
                additional_face_image_0_url = imagesURL.Last()
            });

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://face-recognition18.p.rapidapi.com/register_face2"),
                Headers = {
                    { "x-face-uid", $"{userId}" },
                    { "X-RapidAPI-Key", "e1f2c16754msh49cc6faeffe7fddp135532jsnd263af113831" },
                    { "X-RapidAPI-Host", "face-recognition18.p.rapidapi.com" },
                },
                Content = new StringContent(content)
                { 
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<Guid?> Identify(string imageURL)
        {
            var content = JsonConvert.SerializeObject(new
            {
                image_input_url = imageURL,
            });

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://face-recognition18.p.rapidapi.com/recognize_face2"),
                Headers = {
                    { "X-RapidAPI-Key", "e1f2c16754msh49cc6faeffe7fddp135532jsnd263af113831" },
                    { "X-RapidAPI-Host", "face-recognition18.p.rapidapi.com" },
                },
                Content = new StringContent(content)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<ARSAIdentifyDTO>(responseContent);

            if (body == null || body.Status != "success")
                throw new Exception("Is not possible identify the person");

            if (body.Recognition_Result.Count > 1)
                throw new Exception("Multiple faces in the photo");

            //if (!body.Recognition_Result.First().Liveness.Is_Real_Face)
            //    throw new Exception("The face is not real");

            return Guid.Parse(body.Recognition_Result.First().Recognition_Uidresult);
        }
    }
}
