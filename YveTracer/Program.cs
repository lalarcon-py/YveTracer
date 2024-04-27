using System;
using System.Collections.Generic;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace YouTubeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize YouTube service
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAsTBWj2_TZgkEMFx0M0z8P_2lViMA03SI",
                ApplicationName = "YveTracer"
            });

            // Track autoplayed videos
            List<string> autoplayedVideoIds = new List<string>();

            // Track video statistics
            List<string> commonPhrases = new List<string>();

            // Fetch video details
            var videoRequest = youtubeService.Videos.List("snippet,statistics");
            videoRequest.Id = "VIDEO_ID"; // Replace with actual video ID
            var videoResponse = videoRequest.Execute();

            if (videoResponse.Items.Count > 0)
            {
                var video = videoResponse.Items[0];
                
                // Extract video information
                string title = video.Snippet.Title;
                ulong likes = video.Statistics.LikeCount.GetValueOrDefault();
                ulong views = video.Statistics.ViewCount.GetValueOrDefault();
                string creator = video.Snippet.ChannelTitle;
                ulong comments = video.Statistics.CommentCount.GetValueOrDefault();

                // Tracks autoplayed video
                autoplayedVideoIds.Add(video.Id);

                // Analyzes title for common phrases
                string[] titleWords = title.Split(' '); // Split title into words
                foreach (var word in titleWords)
                {
                    if (word.Length > 2) // Exclude short words
                    {
                        commonPhrases.Add(word);
                    }
                }
            }

            // Display statistics
            Console.WriteLine($"Number of Autoplayed Videos: {autoplayedVideoIds.Count}");
            Console.WriteLine("Common Phrases in Titles:");
            foreach (var phrase in commonPhrases)
            {
                Console.WriteLine(phrase);
            }
        }
    }
}