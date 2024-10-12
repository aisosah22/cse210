using System;
using System.Collections.Generic;

// Class representing a Comment
class Comment
{
    private string _commenterName;
    private string _commentText;

    // Constructor for Comment
    public Comment(string commenterName, string commentText)
    {
        _commenterName = commenterName;
        _commentText = commentText;
    }

    // Method to get the commenter's name
    public string GetCommenterName()
    {
        return _commenterName;
    }


    public string GetCommentText()
    {
        return _commentText;
    }
}

// Class representing a Video
class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments;

    // Constructor for Video
    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    // Method to add a comment to the video
    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    // Method to get the number of comments on the video
    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    // Method to display video information and its comments
    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_lengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");

        foreach (Comment comment in _comments)
        {
            Console.WriteLine($"- {comment.GetCommenterName()}: {comment.GetCommentText()}");
        }

        Console.WriteLine();
    }
}

// Main program demonstrating abstraction
class Program
{
    static void Main()
    {
        // Create 3-4 videos
        Video video1 = new Video("C# Programming Tutorial", "John Doe", 600);
        Video video2 = new Video("Introduction to Design Patterns", "Jane Smith", 1200);
        Video video3 = new Video("Learn Algorithms with Python", "Alice Johnson", 1800);
        Video video4 = new Video("Web Development with JavaScript", "Bob Williams", 900);

        // Add comments to the first video
        video1.AddComment(new Comment("Mike", "Great tutorial!"));
        video1.AddComment(new Comment("Sarah", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Kevin", "Well explained, keep it up!"));

        // Add comments to the second video
        video2.AddComment(new Comment("Rachel", "This was a bit difficult to follow."));
        video2.AddComment(new Comment("Tom", "I enjoyed the part on Singleton Pattern."));
        video2.AddComment(new Comment("Linda", "Please make more videos on this topic!"));

        // Add comments to the third video
        video3.AddComment(new Comment("Steve", "Fantastic explanation of algorithms!"));
        video3.AddComment(new Comment("Emily", "Helped me understand sorting algorithms."));
        video3.AddComment(new Comment("Patrick", "Looking forward to the next one!"));

        // Add comments to the fourth video
        video4.AddComment(new Comment("Jessica", "Clear and concise."));
        video4.AddComment(new Comment("Chris", "Perfect for beginners."));
        video4.AddComment(new Comment("Karen", "Could you cover more advanced topics in future videos?"));

        // Add videos to a list
        List<Video> videoList = new List<Video> { video1, video2, video3, video4 };

        // Iterate through the list of videos and display their information
        foreach (var video in videoList)
        {
            video.DisplayVideoInfo();
        }
    }
}
