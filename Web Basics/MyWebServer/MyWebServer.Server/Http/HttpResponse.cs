﻿using MyWebServer.Server.Common;
using System;
using System.Text;

namespace MyWebServer.Server.Http
{
    public abstract class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Headers.Add(HttpHeader.Server, "My  Web Server");
            this.Headers.Add(HttpHeader.Date, $"{DateTime.UtcNow.ToString("R")}");
        }
        public HttpStatusCode StatusCode { get; protected set; }
        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();
        public string Content { get; protected set; }
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");
            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }
            if (!string.IsNullOrEmpty(this.Content))
            {
                result.AppendLine();
                result.Append(this.Content);
            }
            
            return result.ToString();
        }
        protected void PrepareContent(string content, string contentType)
        {
            Guard.AgainstNull(content, nameof(content));
            Guard.AgainstNull(contentType, nameof(contentType));

            var contentLength = Encoding.UTF8.GetByteCount(content).ToString();

            this.Headers.Add(HttpHeader.ContentType, contentType);
            this.Headers.Add(HttpHeader.ContentLength, contentLength);
            this.Content = content;
        }
    }
}
