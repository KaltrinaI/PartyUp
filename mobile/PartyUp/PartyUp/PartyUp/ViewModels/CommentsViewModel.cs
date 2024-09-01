using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;
using PartyUp.Models;
using PartyUp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PartyUp.ViewModels
{
    public class CommentsViewModel : BaseViewModel
    {
        private ObservableCollection<Comment> _comments;
        public ObservableCollection<Comment> Comments { get => _comments;
            set => SetProperty(ref _comments, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
        
        public User User { get; set; }
        
        public ICommand PostCommentCommand { get; }
        
        public CommentsViewModel(Event partyEvent, ICommentsService commentsService)
        {
            User = JsonConvert.DeserializeObject<User>(Preferences.Get("user", null));
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                var comments = await commentsService.GetCommentsPerEvent(partyEvent.EventId).ConfigureAwait(false);
                Comments = new ObservableCollection<Comment>(comments.OrderByDescending(x => x.TimeOfComment));
            });
            
            PostCommentCommand = new Command(async () =>
            {
                var comment = new Comment
                {
                    Text = Text,
                    UserId = User.UserId,
                    EventId = partyEvent.EventId,
                    TimeOfComment = DateTime.UtcNow
                };

                await commentsService.PostComment(comment);
                comment.User = User;
                Comments.Insert(0,comment);
            });
            
        }
        
    }
}