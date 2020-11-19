using System;
using System.Collections.Generic;
using System.Text;

namespace FacebookAPI.post.senddata
{
    public class ReactionSendData
    {
        public int count { get; set; }
        public string cursor { get; set; }
        public string feedbackTargetID { get; set; }
        public string reactionType { get; set; }
        public int scale { get; set; }
        public string id { get; set; }

        public static ReactionSendData Build(int count,string cursor, string reactionType, string id, string feedbackTargetID)
        {
            ReactionSendData sendData = new ReactionSendData();
            sendData.count = count;
            sendData.cursor = cursor;
            sendData.reactionType = reactionType;
            sendData.feedbackTargetID = feedbackTargetID;
            sendData.scale = 1;
            sendData.id = id;
            return sendData;
        }
    }
}
