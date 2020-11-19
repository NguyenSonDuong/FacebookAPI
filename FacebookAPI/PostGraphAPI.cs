using FacebookAPI.post;
using System;
using System.Collections.Generic;
using System.Text;
using FacebookAPI.delegatefolder;
using FacebookAPI.post.senddata;
using Newtonsoft.Json;

namespace FacebookAPI
{
    public class PostGraphAPI : GraphAPIFacebook
    {
        private String id;
        private String feedID;


        private static String REACTION_TYPE_ALL = "NODE";
        private static String REACTION_TYPE_HAHA = "HAHA";
        private static String REACTION_TYPE_SAD = "SORRY";
        private static String REACTION_TYPE_ANGRY = "ANGRY";
        private static String REACTION_TYPE_LIKE = "LIKE";
        private static String REACTION_TYPE_SUPPORT = "SUPPORT";
        private static String REACTION_TYPE_WOW = "WOW";

        private List<ReactionType.UserReaction> AllReaction;
        private List<ReactionType.UserReaction> HahaReaction;
        private List<ReactionType.UserReaction> SadReaction;
        private List<ReactionType.UserReaction> AngryReaction;
        private List<ReactionType.UserReaction> WowReaction;
        private List<ReactionType.UserReaction> LikeReaction;

        public string Id { get => id; set => id = value; }
        public string FeedID { get => feedID; set => feedID = value; }

        private event ProcessLoading processEvent;
        private event ErrorLoading errorEvent;
        private event SuccessLoading successEvent;

        public event ProcessLoading ProcessEvent
        {
            add { this.processEvent += value; }
            remove { this.processEvent -= value; }
        }
        public event ErrorLoading ErrorEvent
        {
            add { this.errorEvent += value; }
            remove { this.errorEvent -= value; }
        }
        public event SuccessLoading SuccessEvent
        {
            add { this.successEvent += value; }
            remove { this.successEvent -= value; }
        }

        public PostGraphAPI()
        {
            AllReaction = new List<ReactionType.UserReaction>();
            HahaReaction = new List<ReactionType.UserReaction>();
            SadReaction = new List<ReactionType.UserReaction>();
            AngryReaction = new List<ReactionType.UserReaction>();
            WowReaction = new List<ReactionType.UserReaction>();
            LikeReaction = new List<ReactionType.UserReaction>();
        }

        public PostGraphAPI(String id, String feedID)
        {
            this.Id = id;
            this.FeedID = feedID;
            AllReaction = new List<ReactionType.UserReaction>();
            HahaReaction = new List<ReactionType.UserReaction>();
            SadReaction = new List<ReactionType.UserReaction>();
            AngryReaction = new List<ReactionType.UserReaction>();
            WowReaction = new List<ReactionType.UserReaction>();
            LikeReaction = new List<ReactionType.UserReaction>();
        }

        public void GetAllReaction(String cursor, int process)
        {
            try
            {
                ReactionSendData reactionSend = ReactionSendData.Build(20, cursor, REACTION_TYPE_ALL, id, id);
                String dataPost = $"__user={base.Id}&__a=1&fb_dtsg={base.Fb_dtsg}&variables={reactionSend}&doc_id=3785345958187365";
                String json = base.RequestPost("", dataPost, API.CONTENT_TYPE_URLENCODED);
                ReactionType.Rootobject reaction = JsonConvert.DeserializeObject<ReactionType.Rootobject>(json);
                foreach(ReactionType.Edge item in reaction.data.node.reactors.edges)
                {
                    processEvent(item, process++);
                }
                if (reaction.data.node.reactors.page_info.has_next_page)
                {
                    GetAllReaction(reaction.data.node.reactors.page_info.end_cursor, process);
                }
                else
                {
                    successEvent("THÀNH CÔNG", process);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void GetHahaReaction(String cursor, int process)
        {
            try
            {
                ReactionSendData reactionSend = ReactionSendData.Build(20, cursor, REACTION_TYPE_ALL, id, id);
                String dataPost = $"__user={base.Id}&__a=1&fb_dtsg={base.Fb_dtsg}&variables={reactionSend}&doc_id=3785345958187365";
                String json = base.RequestPost("", dataPost, API.CONTENT_TYPE_URLENCODED);
                ReactionType.Rootobject reaction = JsonConvert.DeserializeObject<ReactionType.Rootobject>(json);
                foreach (ReactionType.Edge item in reaction.data.node.reactors.edges)
                {
                    processEvent(item, process++);
                }
                if (reaction.data.node.reactors.page_info.has_next_page)
                {
                    GetAllReaction(reaction.data.node.reactors.page_info.end_cursor, process);
                }
                else
                {
                    successEvent("THÀNH CÔNG", process);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSadReaction(String cursor, int process)
        {

        }
        public void GetAngryReaction(String cursor, int process)
        {

        }
        public void GetWowReaction(String cursor, int process)
        {

        }
        public void GetLikeReaction(String cursor, int process)
        {

        }
    }
}
