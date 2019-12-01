using System.Collections.Generic;

namespace ClientServer
{
    class ChatRoom
    {
        private List<ChatParticipant> Participants = new List<ChatParticipant>();

        //join a new participant in the chat room
        public void Join(ChatParticipant participant)
        {
            participant.OnLeave += Leave;
            participant.OnMessage += SendMessage;
            if (!Participants.Contains(participant))
                Participants.Add(participant);
        }

        //remove a participant from chatRoom if leave
        public void Leave(ChatParticipant participant)
        {
            Participants.Remove(participant);
            participant.OnLeave -= Leave;
            participant.OnMessage -= SendMessage;
        }
        
        //sent message to all participants in the chat room
        public void SendMessage(Message message)
        {
            foreach (var client in Participants)
                client.Send(message);
        }

    }
}