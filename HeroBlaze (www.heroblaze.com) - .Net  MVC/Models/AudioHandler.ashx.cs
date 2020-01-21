using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    /// <summary>
    /// Summary description for AudioHandler
    /// </summary>
    public class AudioHandler : IHttpHandler
    {
        AudioRepository ar = new AudioRepository();
        public void ProcessRequest(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request.QueryString["Id"]);
            byte[] sound = null;
            sound = ar.OneRow(id).AudioByte;
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.ContentType = "audio/mpeg";
            context.Response.BinaryWrite(sound);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}