using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Akh.Breed.Friendships.Dto;

namespace Akh.Breed.Chat.Dto
{
    public class GetUserChatFriendsWithSettingsOutput
    {
        public DateTime ServerTime { get; set; }
        
        public List<FriendDto> Friends { get; set; }

        public GetUserChatFriendsWithSettingsOutput()
        {
            Friends = new EditableList<FriendDto>();
        }
    }
}
