/*
 * Copyright 2024 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// [START generativeaionvertexai_gemini_pdf]

using Google.Cloud.AIPlatform.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PdfInput
{
    public async Task<string> SummarizePdf(
        string projectId = "your-project-id",
        string location = "us-central1",
        string publisher = "google",
        string model = "gemini-1.5-pro-preview-0409")
    {

        var predictionServiceClient = new PredictionServiceClientBuilder
        {
            Endpoint = $"{location}-aiplatform.googleapis.com"
        }.Build();

        string prompt = @"You are a very professional document summarization specialist.
Please summarize the given document.";

        var content = new Content
        {
            Role = "USER"
        };
        content.Parts.AddRange(new List<Part>()
        {
            new()
            {
                Text = prompt
            },
            new()
            {
                FileData = new() {
                    MimeType = "application/pdf",
                    FileUri = "gs://cloud-samples-data/generative-ai/pdf/2403.05530.pdf"
                }
            }
        });

        var generateContentRequest = new GenerateContentRequest
        {
            Model = $"projects/{projectId}/locations/{location}/publishers/{publisher}/models/{model}"
        };
        generateContentRequest.Contents.Add(content);

        GenerateContentResponse response = await predictionServiceClient.GenerateContentAsync(generateContentRequest);

        string responseText = response.Candidates[0].Content.Parts[0].Text;
        Console.WriteLine(responseText);

        return responseText;
    }
}

// [END generativeaionvertexai_gemini_pdf]
