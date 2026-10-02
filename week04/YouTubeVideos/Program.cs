using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Unboxing the New Trail Runner Shoes", "RunWithMia", 540);
        video1.AddComment(new Comment("Sipho", "Great review, very helpful!"));
        video1.AddComment(new Comment("Anna", "Those shoes look so comfortable."));
        video1.AddComment(new Comment("Thabo", "Where can I buy them?"));
        videos.Add(video1);

        Video video2 = new Video("Top 5 Budget Smartphones", "TechTalkZA", 785);
        video2.AddComment(new Comment("Lerato", "I bought number 3 and love it."));
        video2.AddComment(new Comment("James", "Battery life comparison would be nice."));
        video2.AddComment(new Comment("Zinhle", "Subscribed for more reviews!"));
        video2.AddComment(new Comment("Pieter", "Number 1 is overpriced."));
        videos.Add(video2);

        Video video3 = new Video("Easy Pap and Chakalaka Recipe", "KitchenWithNomsa", 420);
        video3.AddComment(new Comment("Bongani", "Made this tonight, it was delicious."));
        video3.AddComment(new Comment("Karen", "Love the spice level."));
        video3.AddComment(new Comment("Ayanda", "My family asked for seconds!"));
        videos.Add(video3);

        Video video4 = new Video("Learning C# in 10 Minutes", "CodeBasics", 610);
        video4.AddComment(new Comment("Olwethu", "Clear explanation of classes."));
        video4.AddComment(new Comment("David", "Please do one on inheritance."));
        video4.AddComment(new Comment("Naledi", "This helped with my homework."));
        videos.Add(video4);

        foreach (Video video in videos)
        {
            video.DisplayVideo();
        }
    }
}
