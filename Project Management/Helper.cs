using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management
{
    public class Helper
    {
        public String Ellipsis(String input, int length)
        {
            if (length < 0) 
            {
                throw new ArgumentException(nameof(length));
            }
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return input[..(length - 3)] + "...";
        }

        public String CombineUrl(params String[] parts)
        {
            StringBuilder sb = new();
            bool wasNull = false;

            if (parts.All(part => part is null))
            {
                throw new ArgumentException("All arguments are null!");
            }
            foreach (String part in parts)
            {
                if (wasNull && part is null)
                {
                    throw new ArgumentException("Non null arg after null one");
                }
                if (part is null)
                {
                    wasNull = true;
                    continue;
                }

                wasNull = false;

                String p = part;
                if (!p.StartsWith('/')) p = '/' + p;
                if (p.EndsWith("/")) p = p[..^1];
                sb.Append(p);
            }
            return sb.ToString();
        }

        public String Finalize(String text)
        {
            if (text != "")
            {
                return text.Trim()[text.Length - 1] == '.' ? text : text + '.';
            }

            return ".";
        }
    }
}
