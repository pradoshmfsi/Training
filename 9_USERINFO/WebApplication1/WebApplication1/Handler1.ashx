<%@ WebHandler Language="C#" Class="Handler1" %>
using System;  
using System.Web;  
  
public class Handler1 : IHttpHandler   
{  
    public void ProcessRequest (HttpContext context)    
    {  
        HttpResponse r = context.Response;  
        r.ContentType = "image/png";  
        string file = context.Request.QueryString["file"];  
        if (file == "Arrow")  
        {  
            r.WriteFile("upload/gettyimages-1092658864-612x612.jpg");  
        }  
        else  
        {  
            r.WriteFile("Image.gif");  
        }  
    }  
  
    public bool IsReusable   
    {  
        get  
        {  
            return false;  
        }  
    } 
}