﻿using System.Text;
using Markdown.Tags;

namespace Markdown
{
    public abstract class HtmlConverter : Converter
    {
        private static readonly Dictionary<TagType, string> startTags = new()
        {
            { TagType.Bold, "<strong>" },
            { TagType.Italic, "<em>" },
            { TagType.Header, "<h>" }
        };

        private static readonly Dictionary<TagType, string> endTags = new()
        {
            { TagType.Bold, "</strong>" },
            { TagType.Italic, "</em>" },
            { TagType.Header, "</h>" }
        };

        public static string InsertTags(ParsedText parsedText)
        {
            var sb = new StringBuilder();
            var prevTagPos = 0;
            foreach (var tag in parsedText.tags)
            {
                sb.Append(parsedText.paragraph.AsSpan(prevTagPos, tag.Position - prevTagPos));
                sb.Append(tag.IsEndTag ? endTags[tag.Type] : startTags[tag.Type]);
                prevTagPos = tag.Position;
            }

            sb.Append(parsedText.paragraph.AsSpan(prevTagPos, parsedText.paragraph.Length - prevTagPos));
            return sb.ToString();
        }
    }
}